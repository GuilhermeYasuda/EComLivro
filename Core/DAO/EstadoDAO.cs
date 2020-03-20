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
    class EstadoDAO : AbstractDAO
    {
        public EstadoDAO() : base("tb_estados", "id_estado")
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

            Estado estado = (Estado)entidade;
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT * FROM tb_estados JOIN tb_paises ON (tb_estados.pais_id = tb_paises.id_pais) ");

            sql.Append("WHERE 1 = 1 ");

            if (estado.ID != 0)
            {
                sql.Append("AND id_estado = :1 ");
            }

            if (estado.Pais.ID != 0)
            {
                sql.Append("AND pais_id = :2 ");
            }

            sql.Append("ORDER BY nome_estado");


            pst.CommandText = sql.ToString();
            parameters = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("1", estado.ID),
                    new NpgsqlParameter("2", estado.Pais.ID)
                };
            
            pst.Parameters.Clear();
            pst.Parameters.AddRange(parameters);
            pst.Connection = connection;
            pst.CommandType = CommandType.Text;

            reader = pst.ExecuteReader();

            // Lista de retorno da consulta do banco de dados, que conterá os produtores encontrados
            List<EntidadeDominio> estados = new List<EntidadeDominio>();
            while (reader.Read())
            {
                estado = new Estado();
                estado.ID = Convert.ToInt32(reader["id_estado"]);
                estado.Nome = reader["nome_estado"].ToString();
                estado.Sigla = reader["sigla_estado"].ToString(); ;
                estado.Pais.ID = Convert.ToInt32(reader["pais_id"].ToString());

                estados.Add(estado);
            }
            connection.Close();
            return estados;
        }
    }
}
