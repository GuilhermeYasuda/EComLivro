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
    class CidadeDAO : AbstractDAO
    {
        public CidadeDAO() : base("tb_cidades", "id_cidade")
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
            Cidade cidade = (Cidade)entidade;
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT * FROM tb_cidades JOIN tb_estados ON (tb_cidades.estado_id = tb_estados.id_estado) ");

            sql.Append("WHERE 1 = 1 ");

            if (cidade.ID != 0)
            {
                sql.Append("AND id_cidade = :1 ");
            }

            if (cidade.Estado.ID != 0)
            {
                sql.Append("AND estado_id = :2 ");
            }

            sql.Append("ORDER BY nome_cidade");


            pst.CommandText = sql.ToString();
            parameters = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("1", cidade.ID),
                    new NpgsqlParameter("2", cidade.Estado.ID)
                };

            pst.Parameters.Clear();
            pst.Parameters.AddRange(parameters);
            pst.Connection = connection;
            pst.CommandType = CommandType.Text;

            reader = pst.ExecuteReader();

            // Lista de retorno da consulta do banco de dados, que conterá os produtores encontrados
            List<EntidadeDominio> cidades = new List<EntidadeDominio>();
            while (reader.Read())
            {
                cidade = new Cidade();
                cidade.ID = Convert.ToInt32(reader["id_cidade"]);
                cidade.Nome = reader["nome_cidade"].ToString();
                cidade.Estado.ID = Convert.ToInt32(reader["estado_id"].ToString());

                cidades.Add(cidade);
            }
            connection.Close();
            return cidades;
        }
    }
}
