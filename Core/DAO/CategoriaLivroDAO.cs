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
    class CategoriaLivroDAO : AbstractDAO
    {
        public CategoriaLivroDAO() : base("tb_cat_livro", "id_cat_livro")
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

            Categoria categoria = (Categoria)entidade;
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT * FROM tb_cat_livro ");
            sql.Append(" WHERE 1 = 1 ");

            if (categoria.ID != 0)
            {
                sql.Append("AND id_cat_livro = :1 ");
            }

            if (!String.IsNullOrEmpty(categoria.Nome))
            {
                sql.Append("AND nome_cat_livro = :2 ");
            }

            if (!String.IsNullOrEmpty(categoria.Descricao))
            {
                sql.Append("AND descricao_cat_livro = :3 ");
            }

            sql.Append("ORDER BY nome_cat_livro");


            pst.CommandText = sql.ToString();
            parameters = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("1", categoria.ID),
                    new NpgsqlParameter("2", categoria.Nome),
                    new NpgsqlParameter("3", categoria.Descricao)
                };
            
            pst.Parameters.Clear();
            pst.Parameters.AddRange(parameters);
            pst.Connection = connection;
            pst.CommandType = CommandType.Text;

            reader = pst.ExecuteReader();

            // Lista de retorno da consulta do banco de dados, que conterá os produtores encontrados
            List<EntidadeDominio> categorias = new List<EntidadeDominio>();
            while (reader.Read())
            {
                categoria = new Categoria();
                categoria.ID = Convert.ToInt32(reader["id_cat_livro"]);
                categoria.Nome = reader["nome_cat_livro"].ToString();
                categoria.Descricao = reader["descricao_cat_livro"].ToString();

                categorias.Add(categoria);
            }
            connection.Close();
            return categorias;
        }
    }
}
