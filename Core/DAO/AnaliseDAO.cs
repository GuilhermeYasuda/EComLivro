using Dominio;
using Dominio.Analise;
using Dominio.Venda;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DAO
{
    public class AnaliseDAO : AbstractDAO
    {
        public AnaliseDAO() : base("tb_pedido", "id_pedido")
        {

        }

        public override void Salvar(EntidadeDominio entidade)
        {
            throw new NotImplementedException();
        }

        public override void Alterar(EntidadeDominio entidade)
        {
            throw new NotImplementedException();
        }

        public override List<EntidadeDominio> Consultar(EntidadeDominio entidade)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            Analise analise = (Analise)entidade;
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT * FROM tb_pedido JOIN tb_status_pedido ON (tb_pedido.status_pedido_fk = tb_status_pedido.id_status_pedido) ");

            // WHERE sem efeito, usado apenas para poder diminuir o número de ifs da construção da query
            sql.Append("WHERE 1 = 1 ");

            if (analise.DataCadastro != null)
            {
                sql.Append("AND dt_cadastro_pedido >= DATE('");
                sql.Append(analise.DataCadastro);
                sql.Append("') ");
            }

            if (analise.DataFim != null)
            {
                sql.Append("AND dt_cadastro_pedido <= DATE('");
                sql.Append(analise.DataFim);
                sql.Append("') ");
            }

            sql.Append("ORDER BY tb_pedido.dt_cadastro_pedido ");

            pst.CommandText = sql.ToString();
            parameters = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("0", analise.ID)
                };

            if (analise.DataCadastro != null)
            {
                NpgsqlParameter[] parametersAux = new NpgsqlParameter[]
                   {
                    new NpgsqlParameter("1", analise.DataCadastro)
                   };
                parameters.Concat(parametersAux);
            }

            if (analise.DataFim != null)
            {
                NpgsqlParameter[] parametersAux = new NpgsqlParameter[]
                   {
                    new NpgsqlParameter("2", analise.DataFim)
                   };

                parameters.Concat(parametersAux);
            }

            pst.Parameters.Clear();
            if(parameters != null)
            {
                pst.Parameters.AddRange(parameters);
            }
            pst.Connection = connection;
            pst.CommandType = CommandType.Text;

            reader = pst.ExecuteReader();

            // Lista de retorno da consulta do banco de dados, que conterá os pedidos encontrados
            List<EntidadeDominio> pedidos = new List<EntidadeDominio>();
            while (reader.Read())
            {
                Pedido pedido = new Pedido();
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
