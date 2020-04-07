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
    public class PedidoDetalheDAO : AbstractDAO
    {
        // construtor padrão
        public PedidoDetalheDAO() : base("tb_pedido_detalhe", "id_pedido_detalhe")
        {

        }

        // construtor para DAOs que também utilizarão o DAO de PedidoDetalhe
        public PedidoDetalheDAO(NpgsqlConnection connection, bool ctrlTransaction) : base(connection, ctrlTransaction, "tb_pedido_detalhe", "id_pedido_detalhe")
        {

        }

        public override void Salvar(EntidadeDominio entidade)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            PedidoDetalhe pedidoDetalhe = (PedidoDetalhe)entidade;

            pst.CommandText = "INSERT INTO tb_pedido_detalhe(pedido_fk, livro_pedido_fk, qtde, valor_unit_livro) " +
                "VALUES (:1, :2, :3, :4)";

            parameters = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("1", pedidoDetalhe.IdPedido),
                    new NpgsqlParameter("2", pedidoDetalhe.Livro.ID),
                    new NpgsqlParameter("3", pedidoDetalhe.Quantidade),
                    new NpgsqlParameter("4", pedidoDetalhe.ValorUnit)
                };

            pst.Parameters.Clear();
            pst.Parameters.AddRange(parameters);
            pst.Connection = connection;
            pst.CommandType = CommandType.Text;
            pst.ExecuteNonQuery();

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
            PedidoDetalhe pedidoDetalhe = (PedidoDetalhe)entidade;
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT * FROM tb_pedido_detalhe JOIN tb_pedido ON tb_pedido.id_pedido = tb_pedido_detalhe.pedido_fk ");
            sql.Append("JOIN tb_livro ON tb_livro.id_livro = tb_pedido_detalhe.livro_pedido_fk ");

            // WHERE sem efeito, usado apenas para poder diminuir o número de ifs da construção da query
            sql.Append("WHERE 1 = 1 ");

            if (pedidoDetalhe.ID != 0)
            {
                sql.Append("AND id_pedido_detalhe = :1 ");
            }

            if (pedidoDetalhe.IdPedido != 0)
            {
                sql.Append("AND pedido_fk = :2 ");
            }

            if (pedidoDetalhe.Livro.ID != 0)
            {
                sql.Append("AND livro_pedido_fk = :3 ");
            }

            if (pedidoDetalhe.Quantidade != 0)
            {
                sql.Append("AND qtde = :4 ");
            }

            if (pedidoDetalhe.ValorUnit != 0.0)
            {
                sql.Append("AND valor_unit_livro = :5 ");
            }

            sql.Append("ORDER BY tb_pedido_detalhe.id_pedido_detalhe,tb_pedido_detalhe.pedido_fk,tb_pedido_detalhe.livro_pedido_fk ");

            pst.CommandText = sql.ToString();
            parameters = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("1", pedidoDetalhe.ID),
                    new NpgsqlParameter("2", pedidoDetalhe.IdPedido),
                    new NpgsqlParameter("3", pedidoDetalhe.Livro.ID),
                    new NpgsqlParameter("4", pedidoDetalhe.Quantidade),
                    new NpgsqlParameter("5", pedidoDetalhe.ValorUnit)
                };

            pst.Parameters.Clear();
            pst.Parameters.AddRange(parameters);
            pst.Connection = connection;
            pst.CommandType = CommandType.Text;

            reader = pst.ExecuteReader();

            // Lista de retorno da consulta do banco de dados, que conterá os cartões do cliente encontrados
            List<EntidadeDominio> pedidoDetalhes = new List<EntidadeDominio>();
            while (reader.Read())
            {
                pedidoDetalhe = new PedidoDetalhe();
                pedidoDetalhe.ID = Convert.ToInt32(reader["id_pedido_detalhe"]);
                pedidoDetalhe.IdPedido = Convert.ToInt32(reader["pedido_fk"]);

                pedidoDetalhe.Livro.ID = Convert.ToInt32(reader["id_livro"]);
                pedidoDetalhe.Livro.Titulo = reader["titulo_livro"].ToString();

                pedidoDetalhe.Quantidade = Convert.ToInt32(reader["qtde"]);
                pedidoDetalhe.ValorUnit = Convert.ToSingle(reader["valor_unit_livro"]);

                pedidoDetalhes.Add(pedidoDetalhe);
            }
            connection.Close();
            return pedidoDetalhes;
        }
    }
}
