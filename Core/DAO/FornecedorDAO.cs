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
    class FornecedorDAO : AbstractDAO
    {
        public FornecedorDAO() : base("tb_fornecedor", "id_fornecedor")
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

            Fornecedor fornecedor = (Fornecedor)entidade;
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT * FROM tb_fornecedor ");
            sql.Append(" WHERE 1 = 1 ");

            if (fornecedor.ID != 0)
            {
                sql.Append("AND id_fornecedor = :1 ");
            }

            if (!String.IsNullOrEmpty(fornecedor.Nome))
            {
                sql.Append("AND nome_fornecedor = :2 ");
            }

            if (fornecedor.Cidade.ID != 0)
            {
                sql.Append("AND cidade_fk = :3 ");
            }

            sql.Append("ORDER BY nome_fornecedor");


            pst.CommandText = sql.ToString();
            parameters = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("1", fornecedor.ID),
                    new NpgsqlParameter("2", fornecedor.Nome),
                    new NpgsqlParameter("3", fornecedor.Cidade.ID)
                };
            
            pst.Parameters.Clear();
            pst.Parameters.AddRange(parameters);
            pst.Connection = connection;
            pst.CommandType = CommandType.Text;

            reader = pst.ExecuteReader();

            // Lista de retorno da consulta do banco de dados, que conterá os produtores encontrados
            List<EntidadeDominio> fornecedores = new List<EntidadeDominio>();
            while (reader.Read())
            {
                fornecedor = new Fornecedor();
                fornecedor.ID = Convert.ToInt32(reader["id_fornecedor"]);
                fornecedor.Nome = reader["nome_fornecedor"].ToString();
                fornecedor.Cidade.ID = Convert.ToInt32(reader["cidade_fk"]);

                fornecedores.Add(fornecedor);
            }
            connection.Close();
            return fornecedores;
        }
    }
}
