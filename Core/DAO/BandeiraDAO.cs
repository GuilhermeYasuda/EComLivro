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
    class BandeiraDAO : AbstractDAO
    {
        public BandeiraDAO() : base("tb_bandeira", "id_bandeira")
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

            Bandeira bandeira = (Bandeira)entidade;
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT * FROM tb_bandeira ");

            if (bandeira.ID != 0)
            {
                sql.Append("WHERE id_bandeira = :1 ");
            }

            sql.Append("ORDER BY nome_bandeira");


            pst.CommandText = sql.ToString();
            parameters = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("1", bandeira.ID)
                };
            
            pst.Parameters.Clear();
            pst.Parameters.AddRange(parameters);
            pst.Connection = connection;
            pst.CommandType = CommandType.Text;

            reader = pst.ExecuteReader();

            // Lista de retorno da consulta do banco de dados, que conterá os produtores encontrados
            List<EntidadeDominio> bandeiras = new List<EntidadeDominio>();
            while (reader.Read())
            {
                bandeira = new Bandeira();
                bandeira.ID = Convert.ToInt32(reader["id_bandeira"]);
                bandeira.Nome = reader["nome_bandeira"].ToString();

                bandeiras.Add(bandeira);
            }

            connection.Close();
            return bandeiras;
        }
    }
}
