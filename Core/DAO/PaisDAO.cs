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
    class PaisDAO : AbstractDAO
    {
        public PaisDAO() : base("tb_paises", "id_pais")
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

            Pais pais = (Pais)entidade;
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT * FROM tb_paises ");

            if (pais.ID != 0)
            {
                sql.Append("WHERE id_pais = :1 ");
            }

            sql.Append("ORDER BY nome_pais");


            pst.CommandText = sql.ToString();
            parameters = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("1", pais.ID)
                };
            
            pst.Parameters.Clear();
            pst.Parameters.AddRange(parameters);
            pst.Connection = connection;
            pst.CommandType = CommandType.Text;

            reader = pst.ExecuteReader();

            // Lista de retorno da consulta do banco de dados, que conterá os produtores encontrados
            List<EntidadeDominio> paises = new List<EntidadeDominio>();
            while (reader.Read())
            {
                pais = new Pais();
                pais.ID = Convert.ToInt32(reader["id_pais"]);
                pais.Nome = reader["nome_pais"].ToString();
                pais.Sigla = reader["sigla_pais"].ToString();

                paises.Add(pais);
            }

            connection.Close();
            return paises;
        }
    }
}
