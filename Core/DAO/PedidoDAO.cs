using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Npgsql;
using System.Data;
using Dominio.Cliente;
using Dominio.Venda;

namespace Core.DAO
{
    public class PedidoDAO : AbstractDAO
    {
        // construtor padrão
        public PedidoDAO() : base("tb_pedido", "id_pedido")
        {

        }

        // construtor para DAOs que também utilizarão o DAO de Pedido
        public PedidoDAO(NpgsqlConnection connection, bool ctrlTransaction) : base(connection, ctrlTransaction, "tb_pedido", "id_pedido")
        {

        }

        public override void Salvar(EntidadeDominio entidade)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            Pedido pedido = (Pedido)entidade;

            pst.CommandText = "INSERT INTO tb_pedido(username, total_pedido, status_pedido_fk, end_entrega_fk, frete, dt_cadastro_pedido) " +
                "VALUES (:1, :2, :3, :4, :5, :6) RETURNING id_pedido ";

            parameters = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("1", pedido.Usuario),
                    new NpgsqlParameter("2", pedido.Total),
                    new NpgsqlParameter("3", pedido.Status.ID),
                    new NpgsqlParameter("4", pedido.EnderecoEntrega.ID),
                    new NpgsqlParameter("5", pedido.Frete),
                    new NpgsqlParameter("6", pedido.DataCadastro)
                };

            pst.Parameters.Clear();
            pst.Parameters.AddRange(parameters);
            pst.Connection = connection;
            pst.CommandType = CommandType.Text;
            pedido.ID = entidade.ID = (int)pst.ExecuteScalar();
            // já executa o comando na linha anterior
            //pst.ExecuteNonQuery();

            // variáveis que conterão os valores do pedido (Subtotal) 
            // e também os valores dos cupons para tirar a diferença dos mesmo, 
            // se necessário criar outro cupom de troca
            decimal subtotal = 0;
            decimal valorCupons = 0;

            // salvando os itens do pedido
            PedidoDetalheDAO pedidoDetalheDAO = new PedidoDetalheDAO(connection, false);
            foreach (PedidoDetalhe detalhe in pedido.Detalhes)
            {
                detalhe.IdPedido = pedido.ID;
                pedidoDetalheDAO.Salvar(detalhe);

                // a cada iteração da lista pega a quantidade e valor do item que está sendo iterado
                subtotal += (decimal)(detalhe.Quantidade * detalhe.ValorUnit);
            }

            // salvando os cartões utilizados no pedido
            CCPedidoDAO ccPedidoDAO = new CCPedidoDAO(connection, false);
            foreach (CartaoCreditoPedido cc in pedido.CCs)
            {
                cc.IdPedido = pedido.ID;
                ccPedidoDAO.Salvar(cc);
            }

            // verifica se cupom promocional foi utilizada
            if(pedido.CupomPromocional.ID != 0)
            {
                ClientePF cliente = new ClientePF();
                // passa usuário que é dono do pedido para o e-mail do cliente
                cliente.Email = pedido.Usuario;

                // para trazer o ID do cliente que será utilizado 
                cliente = ((ClientePF)new ClientePFDAO().Consultar(cliente).ElementAt(0));

                // contem que o cliente já utilizou o cupom promocional
                PedidoXCupom clienteXCupom = new PedidoXCupom();
                clienteXCupom.ID = pedido.ID;
                clienteXCupom.Cupom.ID = pedido.CupomPromocional.ID;

                // preparando e salvando no BD
                PedidoXCupomDAO pedidoXCupomDAO = new PedidoXCupomDAO(connection, false);
                pedidoXCupomDAO.Salvar(clienteXCupom);

                // consulta em CupomDAO para pegar o valor do cupom e assim fazer a conta para valor de cupons
                Cupom cupom = new Cupom();
                cupom = ((Cupom)new CupomDAO().Consultar(pedido.CupomPromocional).ElementAt(0));
                
                // Operação é multiplicação devido ser porcentagem (%)
                valorCupons = subtotal * (decimal)cupom.ValorCupom;
            }

            // alterando status do cupom que o cliente utilizou
            foreach (Cupom cp in pedido.CuponsTroca)
            {
                Cupom cupom = new Cupom();
                cupom = ((Cupom)new CupomDAO().Consultar(cp).ElementAt(0));
                cupom.IdPedido = pedido.ID;
                cupom.Status = 'I';
                new CupomDAO(connection,false).Alterar(cupom);

                // adiciona o valor do cupom para variável
                valorCupons += (decimal)cupom.ValorCupom;
            }

            // verifica se o valor dos cupons usados superam o valor do subtotal
            // se sim, criará outro cupom de troca para o cliente poder estar usando
            if(valorCupons > subtotal)
            {
                ClientePF cliente = new ClientePF();
                // passa usuário que é dono do pedido para o e-mail do cliente
                cliente.Email = pedido.Usuario;

                // para trazer o ID do cliente que será utilizado 
                cliente = ((ClientePF)new ClientePFDAO().Consultar(cliente).ElementAt(0));

                Cupom cupom = new Cupom();
                cupom.IdCliente = cliente.ID;
                cupom.Tipo.ID = 1;      // cupom troca
                cupom.Status = 'A';
                cupom.ValorCupom = (float)(valorCupons - subtotal);
                cupom.CodigoCupom = "AUTOMATICO" + cupom.IdCliente + DateTime.Now.ToString("yyyyMMddHHmmss") + "$" + cupom.ValorCupom;
                new CupomDAO(connection, false).Alterar(cupom);
            }

            if (ctrlTransaction == true)
            {
                pst.CommandText = "COMMIT WORK";
                connection.Close();
            }

            return;
        }

        public override void Alterar(EntidadeDominio entidade)
        {
            throw new NotImplementedException();
        }

        public override List<EntidadeDominio> Consultar(EntidadeDominio entidade)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            Pedido pedido = (Pedido)entidade;
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT * FROM tb_pedido JOIN tb_status_pedido ON (tb_pedido.status_pedido_fk = tb_status_pedido.id_status_pedido) ");

            // WHERE sem efeito, usado apenas para poder diminuir o número de ifs da construção da query
            sql.Append("WHERE 1 = 1 ");

            if (pedido.ID != 0)
            {
                sql.Append("AND id_pedido = :1 ");
            }

            if (!String.IsNullOrEmpty(pedido.Usuario))
            {
                sql.Append("AND username = :2 ");
            }

            if (pedido.Total != 0.0)
            {
                sql.Append("AND total_pedido = :3 ");
            }

            if (pedido.Status.ID != 0)
            {
                sql.Append("AND status_pedido_fk = :4 ");
            }

            if (pedido.EnderecoEntrega.ID != 0)
            {
                sql.Append("AND end_entrega_fk = :5 ");
            }

            if (pedido.Frete != 0.0)
            {
                sql.Append("AND frete = :6 ");
            }

            if (pedido.DataCadastro != null)
            {
                sql.Append("AND dt_cadastro_pedido = :7 ");
            }

            sql.Append("ORDER BY tb_pedido.id_pedido,tb_pedido.status_pedido_fk ");

            pst.CommandText = sql.ToString();
            parameters = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("1", pedido.ID),
                    new NpgsqlParameter("2", pedido.Usuario),
                    new NpgsqlParameter("3", pedido.Total),
                    new NpgsqlParameter("4", pedido.Status.ID),
                    new NpgsqlParameter("5", pedido.EnderecoEntrega.ID),
                    new NpgsqlParameter("6", pedido.Frete)
                };

            if (pedido.DataCadastro != null)
            {
                NpgsqlParameter[] parametersAux = new NpgsqlParameter[]
                   {
                    new NpgsqlParameter("7", pedido.DataCadastro)
                   };

                parameters.Concat(parametersAux);
            }

            pst.Parameters.Clear();
            pst.Parameters.AddRange(parameters);
            pst.Connection = connection;
            pst.CommandType = CommandType.Text;

            reader = pst.ExecuteReader();

            // Lista de retorno da consulta do banco de dados, que conterá os cartões do cliente encontrados
            List<EntidadeDominio> pedidos = new List<EntidadeDominio>();
            while (reader.Read())
            {
                pedido = new Pedido();
                pedido.ID = Convert.ToInt32(reader["id_pedido"]);
                pedido.Usuario = reader["username"].ToString();
                pedido.Total = Convert.ToSingle(reader["total_pedido"]);

                pedido.Status.ID = Convert.ToInt32(reader["id_status_pedido"]);
                pedido.Status.Nome = reader["nome_status_pedido"].ToString();

                pedido.EnderecoEntrega.ID = Convert.ToInt32(reader["end_entrega_fk"]);

                pedido.Frete = Convert.ToSingle(reader["frete"]);

                pedido.DataCadastro = Convert.ToDateTime(reader["dt_cadastro_pedido"].ToString());

                pedidos.Add(pedido);
            }
            connection.Close();
            return pedidos;
        }
    }
}
