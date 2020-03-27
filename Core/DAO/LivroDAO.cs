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
    public class LivroDAO : AbstractDAO
    {
        public LivroDAO() : base("tb_livro", "id_livro")
        {

        }

        public override void Salvar(EntidadeDominio entidade)
        {
            throw new NotImplementedException();
        }

        public override void Alterar(EntidadeDominio entidade)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            Livro livro = (Livro)entidade;

            pst.CommandText = "UPDATE tb_livro SET categoria_motivo_fk = :1, motivo = :2 WHERE id_livro = :3 ";
            parameters = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("1", livro.CategoriaMotivo.ID),
                    new NpgsqlParameter("2", livro.Motivo),
                    new NpgsqlParameter("3", livro.ID)
                };

            pst.Parameters.Clear();
            pst.Parameters.AddRange(parameters);
            pst.Connection = connection;
            pst.CommandType = CommandType.Text;
            pst.ExecuteNonQuery();
            pst.CommandText = "COMMIT WORK";
            connection.Close();
            return;
        }

        public override List<EntidadeDominio> Consultar(EntidadeDominio entidade)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            Livro livro = (Livro)entidade;
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT * FROM tb_livro JOIN tb_livro_autor ON (tb_livro.id_livro = tb_livro_autor.id_livro) ");
            sql.Append("                            JOIN tb_autor ON (tb_livro_autor.id_autor = tb_autor.id_autor) ");
            sql.Append("                            JOIN tb_livro_cat ON (tb_livro.id_livro = tb_livro_cat.id_livro) ");
            sql.Append("                            JOIN tb_cat_livro ON (tb_livro_cat.id_cat_livro = tb_cat_livro.id_cat_livro) ");
            sql.Append("                            JOIN tb_editora ON (tb_livro.editora_fk = tb_editora.id_editora) ");
            sql.Append("                            JOIN tb_cidades ON (tb_editora.cidade_fk = tb_cidades.id_cidade) ");
            sql.Append("                            JOIN tb_estados ON (tb_cidades.estado_id = tb_estados.id_estado) ");
            sql.Append("                            JOIN tb_paises ON (tb_estados.pais_id = tb_paises.id_pais) ");
            sql.Append("                            JOIN tb_dimensoes ON (tb_livro.dimensoes_fk = tb_dimensoes.id_dimensoes) ");
            sql.Append("                            JOIN tb_grupo_preco ON (tb_grupo_preco.id_grupo_preco = tb_livro.grupo_preco_fk) ");
            sql.Append("                            JOIN tb_cat_motivo ON (tb_cat_motivo.id_cat_motivo = tb_livro.categoria_motivo_fk) ");

            // WHERE sem efeito, usado apenas para poder diminuir o número de ifs da construção da query
            sql.Append("WHERE 1 = 1 ");

            if (livro.ID != 0)
            {
                sql.Append("AND tb_livro.id_livro = :1 ");
            }

            if (livro.Autores.Count > 0)
            {
                if (livro.Autores.ElementAt(0).ID != 0)
                {
                    sql.Append("AND id_autor = :2 ");
                }

                if (!String.IsNullOrEmpty(livro.Autores.ElementAt(0).Nome))
                {
                    sql.Append("AND nome_autor = :3 ");
                }
            }

            if (livro.Categorias.Count > 0)
            {
                if (livro.Categorias.ElementAt(0).ID != 0)
                {
                    sql.Append("AND id_cat_livro = :4 ");
                }

                if (!String.IsNullOrEmpty(livro.Categorias.ElementAt(0).Nome))
                {
                    sql.Append("AND nome_cat_livro = :5 ");
                }

                if (!String.IsNullOrEmpty(livro.Categorias.ElementAt(0).Descricao))
                {
                    sql.Append("AND descricao_cat_livro = :6 ");
                }
            }

            if (!String.IsNullOrEmpty(livro.Ano))
            {
                sql.Append("AND ano_livro = :7 ");
            }

            if (!String.IsNullOrEmpty(livro.Titulo))
            {
                sql.Append("AND titulo_livro = :8 ");
            }

            if (livro.Editora != null)
            {
                if (livro.Editora.ID != 0)
                {
                    sql.Append("AND id_editora = :9 ");
                }

                if (!String.IsNullOrEmpty(livro.Editora.Nome))
                {
                    sql.Append("AND nome_editora = :10 ");
                }
            }

            if (!String.IsNullOrEmpty(livro.Edicao))
            {
                sql.Append("AND edicao_livro = :11 ");
            }

            if (!String.IsNullOrEmpty(livro.ISBN))
            {
                sql.Append("AND isbn = :12 ");
            }

            if (!String.IsNullOrEmpty(livro.NumeroPaginas))
            {
                sql.Append("AND numero_paginas = :13 ");
            }

            if (!String.IsNullOrEmpty(livro.Sinopse))
            {
                sql.Append("AND sinopse = :14 ");
            }

            if (livro.Dimensoes != null)
            {
                if (livro.Dimensoes.ID != 0)
                {
                    sql.Append("AND id_dimensoes = :15 ");
                }

                if (livro.Dimensoes.Altura != 0)
                {
                    sql.Append("AND altura = :16 ");
                }

                if (livro.Dimensoes.Largura != 0)
                {
                    sql.Append("AND largura = :17 ");
                }

                if (livro.Dimensoes.Profundidade != 0)
                {
                    sql.Append("AND profundidade = :18 ");
                }

                if (livro.Dimensoes.Peso != 0.0)
                {
                    sql.Append("AND peso = :19 ");
                }
            }

            if(livro.GrupoPrecificacao != null)
            {
                if(livro.GrupoPrecificacao.ID != 0)
                {
                    sql.Append("AND id_grupo_preco = :20 ");
                }

                if (!String.IsNullOrEmpty(livro.GrupoPrecificacao.Nome))
                {
                    sql.Append("AND nome_grupo_preco = :21 ");
                }

                if (livro.GrupoPrecificacao.MargemLucro != 0.0)
                {
                    sql.Append("AND margem_lucro = :22 ");
                }
            }

            if (!String.IsNullOrEmpty(livro.CodigoBarras))
            {
                sql.Append("AND codigo_barras_livro = :23 ");
            }

            if (livro.CategoriaMotivo != null)
            {
                if (livro.CategoriaMotivo.ID != 0)
                {
                    sql.Append("AND id_cat_motivo = :24 ");
                }

                if (livro.CategoriaMotivo.Ativo != 'Z')
                {
                    sql.Append("AND ativo = :25 ");
                }
            }

            if (!String.IsNullOrEmpty(livro.Motivo))
            {
                sql.Append("AND motivo = :26 ");
            }

            if (livro.DataCadastro != null)
            {
                sql.Append("AND dt_cadastro_livro = :27 ");
            }

            sql.Append("ORDER BY tb_livro.id_livro, tb_autor.id_autor, tb_cat_livro.id_cat_livro ");

            pst.CommandText = sql.ToString();
            parameters = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("1", livro.ID),
                    new NpgsqlParameter("7", livro.Ano),
                    new NpgsqlParameter("8", livro.Titulo),
                    new NpgsqlParameter("9", livro.Editora.ID),
                    new NpgsqlParameter("10", livro.Editora.Nome),
                    new NpgsqlParameter("11", livro.Edicao),
                    new NpgsqlParameter("12", livro.ISBN),
                    new NpgsqlParameter("13", livro.NumeroPaginas),
                    new NpgsqlParameter("14", livro.Sinopse),
                    new NpgsqlParameter("15", livro.Dimensoes.ID),
                    new NpgsqlParameter("16", livro.Dimensoes.Altura),
                    new NpgsqlParameter("17", livro.Dimensoes.Largura),
                    new NpgsqlParameter("18", livro.Dimensoes.Profundidade),
                    new NpgsqlParameter("19", livro.Dimensoes.Peso),
                    new NpgsqlParameter("20", livro.GrupoPrecificacao.ID),
                    new NpgsqlParameter("21", livro.GrupoPrecificacao.Nome),
                    new NpgsqlParameter("22", livro.GrupoPrecificacao.MargemLucro),
                    new NpgsqlParameter("23", livro.CodigoBarras),
                    new NpgsqlParameter("24", livro.CategoriaMotivo.ID),
                    new NpgsqlParameter("25", livro.CategoriaMotivo.Ativo),
                    new NpgsqlParameter("26", livro.Motivo)
                };

            if (livro.Autores.Count > 0)
            {
                NpgsqlParameter[] parametersAux = new NpgsqlParameter[]
                   {
                    new NpgsqlParameter("2", livro.Autores.ElementAt(0).ID),
                    new NpgsqlParameter("3", livro.Autores.ElementAt(0).Nome)
                   };

                parameters.Concat(parametersAux);
            }

            if (livro.Categorias.Count > 0)
            {
                NpgsqlParameter[] parametersAux = new NpgsqlParameter[]
                   {
                    new NpgsqlParameter("4", livro.Categorias.ElementAt(0).ID),
                    new NpgsqlParameter("5", livro.Categorias.ElementAt(0).Nome),
                    new NpgsqlParameter("6", livro.Categorias.ElementAt(0).Descricao)
                   };

                parameters.Concat(parametersAux);
            }

            if (livro.DataCadastro != null)
            {
                NpgsqlParameter[] parametersAux = new NpgsqlParameter[]
                   {
                    new NpgsqlParameter("27", livro.DataCadastro)
                   };

                parameters.Concat(parametersAux);
            }
            pst.Parameters.Clear();
            pst.Parameters.AddRange(parameters);
            pst.Connection = connection;
            pst.CommandType = CommandType.Text;

            reader = pst.ExecuteReader();

            // Lista de retorno da consulta do banco de dados, que conterá os livros encontrados
            List<EntidadeDominio> livros = new List<EntidadeDominio>();

            Livro livroAux = new Livro();
            livroAux.ID = 0;

            Autor autor = new Autor();
            Categoria categoria = new Categoria();

            Autor autorAux = new Autor();
            CartaoCredito categoriaAux = new CartaoCredito();

            livro = new Livro();

            while (reader.Read())
            {
                // verifica se livro que está trazendo do BD é igual ao anterior
                if (Convert.ToInt32(reader["id_livro"]) != livroAux.ID)
                {
                    livro = new Livro();
                    livro.ID = Convert.ToInt32(reader["id_livro"]);

                    // passando id do livro que está vindo para o auxiliar
                    livroAux.ID = livro.ID;

                    //// -------------------- AUTOR - COMEÇO ----------------------------------
                    autor = new Autor();

                    autor.ID = Convert.ToInt32(reader["id_autor"]);
                    autor.Nome = reader["nome_autor"].ToString();

                    livro.Autores.Add(autor);
                    autorAux.ID = autor.ID;
                    //// -------------------- AUTOR - FIM ----------------------------------

                    // -------------------- CATEGORIA LIVRO - COMEÇO ----------------------------------
                    categoria = new Categoria();

                    categoria.ID = Convert.ToInt32(reader["id_cat_livro"]);
                    categoria.Nome = reader["nome_cat_livro"].ToString();
                    categoria.Descricao = reader["descricao_cat_livro"].ToString();

                    livro.Categorias.Add(categoria);
                    categoriaAux.ID = categoria.ID;
                    // -------------------- CATEGORIA LIVRO - FIM ----------------------------------

                    livro.Ano = reader["ano_livro"].ToString();
                    livro.Titulo = reader["titulo_livro"].ToString();

                    // -------------------- EDITORA - COMEÇO ----------------------------------
                    livro.Editora.ID = Convert.ToInt32(reader["id_editora"]);
                    livro.Editora.Nome = reader["nome_editora"].ToString();
                    livro.Editora.Cidade.ID = Convert.ToInt32(reader["id_cidade"]);
                    livro.Editora.Cidade.Nome = reader["nome_cidade"].ToString();
                    // -------------------- EDITORA - FIM ----------------------------------

                    livro.Edicao = reader["edicao_livro"].ToString();
                    livro.ISBN = reader["isbn"].ToString();
                    livro.NumeroPaginas = reader["numero_paginas"].ToString();
                    livro.Sinopse = reader["sinopse"].ToString();

                    // -------------------- DIMENSÕES - COMEÇO ----------------------------------
                    livro.Dimensoes.ID = Convert.ToInt32(reader["id_dimensoes"]);
                    livro.Dimensoes.Altura = Convert.ToInt32(reader["altura"]);
                    livro.Dimensoes.Largura = Convert.ToInt32(reader["largura"]);
                    livro.Dimensoes.Profundidade = Convert.ToInt32(reader["profundidade"]);
                    livro.Dimensoes.Peso = Convert.ToSingle(reader["peso"]);
                    // -------------------- DIMENSÕES - FIM ----------------------------------

                    // -------------------- GRUPO PRECIFICAÇÃO - COMEÇO ----------------------------------
                    livro.GrupoPrecificacao.ID = Convert.ToInt32(reader["id_grupo_preco"]);
                    livro.GrupoPrecificacao.Nome = reader["nome_grupo_preco"].ToString();
                    livro.GrupoPrecificacao.MargemLucro = Convert.ToSingle(reader["margem_lucro"]);
                    // -------------------- GRUPO PRECIFICAÇÃO - FIM ----------------------------------

                    livro.CodigoBarras = reader["codigo_barras_livro"].ToString();

                    // -------------------- CATEGORIA MOTIVO - COMEÇO ----------------------------------
                    livro.CategoriaMotivo.ID = Convert.ToInt32(reader["id_cat_motivo"]);
                    livro.CategoriaMotivo.Ativo = reader["ativo"].ToString().First();
                    livro.CategoriaMotivo.Nome = reader["nome_cat_motivo"].ToString();
                    livro.CategoriaMotivo.Descricao = reader["descricao_cat_motivo"].ToString();
                    // -------------------- CATEGORIA MOTIVO - FIM ----------------------------------

                    livro.Motivo = reader["motivo"].ToString();
                    livro.DataCadastro = Convert.ToDateTime(reader["dt_cadastro_livro"].ToString());

                }
                else
                {
                    //// -------------------- AUTOR - COMEÇO ----------------------------------
                    autor = new Autor();

                    autor.ID = Convert.ToInt32(reader["id_autor"]);
                    if (autorAux.ID != autor.ID)
                    {
                        autor.Nome = reader["nome_autor"].ToString();

                        livro.Autores.Add(autor);

                        autorAux.ID = autor.ID;
                    }
                    //// -------------------- AUTOR - FIM ----------------------------------

                    // -------------------- CATEGORIA LIVRO - COMEÇO ----------------------------------
                    categoria = new Categoria();

                    categoria.ID = Convert.ToInt32(reader["id_cat_livro"]);
                    if (categoriaAux.ID != categoria.ID)
                    {
                        categoria.Nome = reader["nome_cat_livro"].ToString();
                        categoria.Descricao = reader["descricao_cat_livro"].ToString();

                        livro.Categorias.Add(categoria);

                        categoriaAux.ID = categoria.ID;
                    }
                    // -------------------- CATEGORIA LIVRO - FIM ----------------------------------
                }

                livros.Add(livro);
            }
            connection.Close();
            return livros;
        }

    }
}
