using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using System.Data;
using Npgsql;
using Dominio.Livro;

namespace Core.DAO
{
    public class EstoqueDAO : AbstractDAO
    {
        public EstoqueDAO() : base("tb_estoque_livro", "id_estoque_livro")
        {

        }

        // construtor para DAOs que também utilizarão o DAO de estoque
        public EstoqueDAO(NpgsqlConnection connection, bool ctrlTransaction) : base(connection, ctrlTransaction, "tb_estoque_livro", "id_estoque_livro")
        {

        }

        public override void Salvar(EntidadeDominio entidade)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            Estoque estoque = (Estoque)entidade;

            pst.CommandText = "INSERT INTO tb_estoque_livro (id_estoque_livro, quantidade_livro, custo_unid, valor_unid, fornecedor_livro_fk, dt_entrada_estoque) VALUES (:1, :2, :3, :4, :5, :6) ";
            parameters = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("1", estoque.Livro.ID),
                    new NpgsqlParameter("2", estoque.Qtde),
                    new NpgsqlParameter("3", estoque.ValorCusto),
                    new NpgsqlParameter("4", estoque.ValorVenda),
                    new NpgsqlParameter("5", estoque.Fornecedor.ID),
                    new NpgsqlParameter("6", estoque.DataCadastro)
                };

            pst.Parameters.Clear();
            pst.Parameters.AddRange(parameters);
            pst.Connection = connection;
            pst.CommandType = CommandType.Text;
            pst.ExecuteNonQuery();

            if (ctrlTransaction == true)
            {
                pst.CommandText = "COMMIT WORK";
                connection.Close();
            }

            return;
        }

        public override void Alterar(EntidadeDominio entidade)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            Estoque estoque = (Estoque)entidade;

            pst.CommandText = "UPDATE tb_estoque_livro SET quantidade_livro = :1, custo_unid =:2, valor_unid = :3, fornecedor_livro_fk = :4, dt_entrada_estoque = :5 WHERE id_estoque_livro = :6";
            parameters = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("1", estoque.Qtde),
                    new NpgsqlParameter("2", estoque.ValorCusto),
                    new NpgsqlParameter("3", estoque.ValorVenda),
                    new NpgsqlParameter("4", estoque.Fornecedor.ID),
                    new NpgsqlParameter("5", estoque.DataCadastro),
                    new NpgsqlParameter("6", estoque.Livro.ID)
                };

            pst.Parameters.Clear();
            pst.Parameters.AddRange(parameters);
            pst.Connection = connection;
            pst.CommandType = CommandType.Text;
            pst.ExecuteNonQuery();

            if (ctrlTransaction == true)
            {
                pst.CommandText = "COMMIT WORK";
                connection.Close();
            }

            return;
        }

        public override List<EntidadeDominio> Consultar(EntidadeDominio entidade)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            Estoque estoque = (Estoque)entidade;
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT * FROM tb_estoque_livro ");
            sql.Append("RIGHT JOIN tb_livro ON (tb_livro.id_livro = tb_estoque_livro.id_estoque_livro) ");
            sql.Append("LEFT JOIN tb_fornecedor ON (tb_fornecedor.id_fornecedor = tb_estoque_livro.fornecedor_livro_fk) ");
            sql.Append("LEFT JOIN tb_cidades ON (tb_fornecedor.cidade_fk = tb_cidades.id_cidade) ");
            sql.Append("JOIN tb_cat_motivo ON (tb_cat_motivo.id_cat_motivo = tb_livro.categoria_motivo_fk) ");

            // WHERE sem efeito, usado apenas para poder diminuir o número de ifs da construção da query
            sql.Append("WHERE 1 = 1 ");

            if (estoque.Livro.ID != 0)
            {
                sql.Append("AND id_estoque_livro = :1 ");
            }

            if (estoque.Qtde != 0)
            {
                sql.Append("AND quantidade_livro = :2 ");
            }

            if (estoque.ValorCusto != 0.0)
            {
                sql.Append("AND custo_unid = :3 ");
            }

            if (estoque.ValorVenda != 0.0)
            {
                sql.Append("AND valor_unid = :4 ");
            }

            if (estoque.Fornecedor.ID != 0)
            {
                sql.Append("AND fornecedor_livro_fk = :5 ");
            }

            if (estoque.DataCadastro != null)
            {
                sql.Append("AND dt_entrada_estoque = :6 ");
            }

            if (estoque.Livro.CategoriaMotivo.ID != 0)
            {
                sql.Append("AND id_cat_motivo = :7 ");
            }

            if (estoque.Livro.CategoriaMotivo.Ativo != 'Z')
            {
                sql.Append("AND ativo = :8 ");
            }

            sql.Append("ORDER BY tb_livro.id_livro ");

            pst.CommandText = sql.ToString();
            parameters = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("1", estoque.Livro.ID),
                    new NpgsqlParameter("2", estoque.Qtde),
                    new NpgsqlParameter("3", estoque.ValorCusto),
                    new NpgsqlParameter("4", estoque.ValorVenda),
                    new NpgsqlParameter("5", estoque.Fornecedor.ID),
                    new NpgsqlParameter("7", estoque.Livro.CategoriaMotivo.ID),
                    new NpgsqlParameter("8", estoque.Livro.CategoriaMotivo.Ativo)
                };

            if (estoque.DataCadastro != null)
            {
                NpgsqlParameter[] parametersAux = new NpgsqlParameter[]
                   {
                    new NpgsqlParameter("6", estoque.DataCadastro)
                   };

                parameters.Concat(parametersAux);
            }

            pst.Parameters.Clear();
            pst.Parameters.AddRange(parameters);
            pst.Connection = connection;
            pst.CommandType = CommandType.Text;

            reader = pst.ExecuteReader();

            // Lista de retorno da consulta do banco de dados, que conterá os endereços encontrados
            List<EntidadeDominio> estoques = new List<EntidadeDominio>();
            while (reader.Read())
            {
                estoque = new Estoque();
                estoque.Livro.ID = Convert.ToInt32(reader["id_livro"]);
                estoque.Livro.Titulo = reader["titulo_livro"].ToString();

                if (!DBNull.Value.Equals(reader["quantidade_livro"]))
                    estoque.Qtde = Convert.ToInt32(reader["quantidade_livro"].ToString());

                if (!DBNull.Value.Equals(reader["custo_unid"]))
                    estoque.ValorCusto = Convert.ToSingle(reader["custo_unid"]);

                if (!DBNull.Value.Equals(reader["valor_unid"]))
                    estoque.ValorVenda = Convert.ToSingle(reader["valor_unid"]);

                if (!DBNull.Value.Equals(reader["id_fornecedor"]))
                    estoque.Fornecedor.ID = Convert.ToInt32(reader["id_fornecedor"]);

                if (!DBNull.Value.Equals(reader["nome_fornecedor"]))
                    estoque.Fornecedor.Nome = reader["nome_fornecedor"].ToString();

                if (!DBNull.Value.Equals(reader["id_cidade"]))
                    estoque.Fornecedor.Cidade.ID = Convert.ToInt32(reader["id_cidade"]);

                if (!DBNull.Value.Equals(reader["nome_cidade"]))
                    estoque.Fornecedor.Cidade.Nome = reader["nome_cidade"].ToString();

                if (!DBNull.Value.Equals(reader["dt_entrada_estoque"]))
                    estoque.DataCadastro = Convert.ToDateTime(reader["dt_entrada_estoque"].ToString());

                estoque.Livro.CategoriaMotivo.ID = Convert.ToInt32(reader["id_cat_motivo"]);
                estoque.Livro.CategoriaMotivo.Ativo = reader["ativo"].ToString().First();
                estoque.Livro.CategoriaMotivo.Nome = reader["nome_cat_motivo"].ToString();
                estoque.Livro.CategoriaMotivo.Descricao = reader["descricao_cat_motivo"].ToString();

                estoques.Add(estoque);
            }
            connection.Close();
            return estoques;
        }
    }
}
