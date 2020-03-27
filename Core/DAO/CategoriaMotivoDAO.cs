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
    class CategoriaMotivoDAO : AbstractDAO
    {
        public CategoriaMotivoDAO() : base("tb_cat_motivo", "id_cat_motivo")
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

            CategoriaMotivo categoria = (CategoriaMotivo)entidade;
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT * FROM tb_cat_motivo ");
            sql.Append(" WHERE 1 = 1 ");

            if (categoria.ID != 0)
            {
                sql.Append("AND id_cat_motivo = :1 ");
            }

            if (!String.IsNullOrEmpty(categoria.Nome))
            {
                sql.Append("AND nome_cat_motivo = :2 ");
            }

            if (categoria.Ativo != 'Z')
            {
                sql.Append("AND ativo = :3 ");
            }

            if (!String.IsNullOrEmpty(categoria.Descricao))
            {
                sql.Append("AND descricao_cat_motivo = :4 ");
            }

            sql.Append("ORDER BY ativo, nome_cat_motivo");


            pst.CommandText = sql.ToString();
            parameters = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("1", categoria.ID),
                    new NpgsqlParameter("2", categoria.Nome),
                    new NpgsqlParameter("3", categoria.Ativo),
                    new NpgsqlParameter("4", categoria.Descricao)
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
                categoria = new CategoriaMotivo();
                categoria.ID = Convert.ToInt32(reader["id_cat_motivo"]);
                categoria.Nome = reader["nome_cat_motivo"].ToString();
                categoria.Ativo = reader["ativo"].ToString().First();
                categoria.Descricao = reader["descricao_cat_motivo"].ToString();

                categorias.Add(categoria);
            }
            connection.Close();
            return categorias;
        }
    }
}
