using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using System.Data;
using Npgsql;
using Dominio.Cliente;
using Dominio.Livro;

namespace Core.DAO
{
    class EditoraDAO : AbstractDAO
    {
        public EditoraDAO() : base("tb_editora", "id_editora")
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

            Editora editora = (Editora)entidade;
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT * FROM tb_editora ");
            sql.Append(" WHERE 1 = 1 ");

            if (editora.ID != 0)
            {
                sql.Append("AND id_editora = :1 ");
            }

            if (!String.IsNullOrEmpty(editora.Nome))
            {
                sql.Append("AND nome_editora = :2 ");
            }

            if (editora.Cidade.ID != 0)
            {
                sql.Append("AND cidade_fk = :3 ");
            }

            sql.Append("ORDER BY nome_editora");


            pst.CommandText = sql.ToString();
            parameters = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("1", editora.ID),
                    new NpgsqlParameter("2", editora.Nome),
                    new NpgsqlParameter("3", editora.Cidade.ID)
                };
            
            pst.Parameters.Clear();
            pst.Parameters.AddRange(parameters);
            pst.Connection = connection;
            pst.CommandType = CommandType.Text;

            reader = pst.ExecuteReader();

            // Lista de retorno da consulta do banco de dados, que conterá os produtores encontrados
            List<EntidadeDominio> editoras = new List<EntidadeDominio>();
            while (reader.Read())
            {
                editora = new Editora();
                editora.ID = Convert.ToInt32(reader["id_editora"]);
                editora.Nome = reader["nome_editora"].ToString();
                editora.Cidade.ID = Convert.ToInt32(reader["cidade_fk"]);

                editoras.Add(editora);
            }
            connection.Close();
            return editoras;
        }
    }
}
