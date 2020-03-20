using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using System.Data;
using Npgsql;
using Dominio.Cliente;

namespace Core.DAO
{
    class TipoLogradouroDAO : AbstractDAO
    {
        public TipoLogradouroDAO() : base("tb_tipo_logradouro", "id_tipo_log")
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

            TipoLogradouro tipo = (TipoLogradouro)entidade;
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT * FROM tb_tipo_logradouro ");
            sql.Append(" WHERE 1 = 1 ");

            if (tipo.ID != 0)
            {
                sql.Append("AND id_tipo_log = :1 ");
            }

            if (!String.IsNullOrEmpty(tipo.Nome))
            {
                sql.Append("AND nome_tipo_log = :2 ");
            }

            sql.Append("ORDER BY nome_tipo_log");


            pst.CommandText = sql.ToString();
            parameters = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("1", tipo.ID),
                    new NpgsqlParameter("2", tipo.Nome)
                };
            
            pst.Parameters.Clear();
            pst.Parameters.AddRange(parameters);
            pst.Connection = connection;
            pst.CommandType = CommandType.Text;

            reader = pst.ExecuteReader();

            // Lista de retorno da consulta do banco de dados, que conterá os produtores encontrados
            List<EntidadeDominio> tipos = new List<EntidadeDominio>();
            while (reader.Read())
            {
                tipo = new TipoLogradouro();
                tipo.ID = Convert.ToInt32(reader["id_tipo_log"]);
                tipo.Nome = reader["nome_tipo_log"].ToString();

                tipos.Add(tipo);
            }
            connection.Close();
            return tipos;
        }
    }
}
