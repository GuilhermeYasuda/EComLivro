using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using System.Data;
using Npgsql;
using Dominio.Cliente;
using Dominio.Venda;

namespace Core.DAO
{
    class StatusPedidoDAO : AbstractDAO
    {
        public StatusPedidoDAO() : base("tb_status_pedido", "id_status_pedido")
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

            StatusPedido status = (StatusPedido)entidade;
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT * FROM tb_status_pedido ");
            sql.Append(" WHERE 1 = 1 ");

            if (status.ID != 0)
            {
                sql.Append("AND id_status_pedido = :1 ");
            }

            if (!String.IsNullOrEmpty(status.Nome))
            {
                sql.Append("AND nome_status_pedido = :2 ");
            }

            sql.Append("ORDER BY nome_status_pedido");


            pst.CommandText = sql.ToString();
            parameters = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("1", status.ID),
                    new NpgsqlParameter("2", status.Nome)
                };
            
            pst.Parameters.Clear();
            pst.Parameters.AddRange(parameters);
            pst.Connection = connection;
            pst.CommandType = CommandType.Text;

            reader = pst.ExecuteReader();

            // Lista de retorno da consulta do banco de dados, que conterá os status encontrados
            List<EntidadeDominio> statuss = new List<EntidadeDominio>();
            while (reader.Read())
            {
                status = new StatusPedido();
                status.ID = Convert.ToInt32(reader["id_status_pedido"]);
                status.Nome = reader["nome_status_pedido"].ToString();

                statuss.Add(status);
            }
            connection.Close();
            return statuss;
        }
    }
}
