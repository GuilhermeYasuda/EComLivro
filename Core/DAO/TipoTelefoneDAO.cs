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
    class TipoTelefoneDAO : AbstractDAO
    {
        public TipoTelefoneDAO() : base("tb_tipo_telefone", "id_tipo_tel")
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

            TipoTelefone tipo = (TipoTelefone)entidade;
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT * FROM tb_tipo_telefone ");
            sql.Append(" WHERE 1 = 1 ");

            if (tipo.ID != 0)
            {
                sql.Append("AND id_tipo_tel = :1 ");
            }

            if (!String.IsNullOrEmpty(tipo.Nome))
            {
                sql.Append("AND nome_tipo_tel = :2 ");
            }

            sql.Append("ORDER BY nome_tipo_tel");


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
                tipo = new TipoTelefone();
                tipo.ID = Convert.ToInt32(reader["id_tipo_tel"]);
                tipo.Nome = reader["nome_tipo_tel"].ToString();

                tipos.Add(tipo);
            }
            connection.Close();
            return tipos;
        }
    }
}
