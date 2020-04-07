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
    class TipoCupomDAO : AbstractDAO
    {
        public TipoCupomDAO() : base("tb_tipo_cupom", "id_tipo_cupom")
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

            TipoCupom tipo = (TipoCupom)entidade;
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT * FROM tb_tipo_cupom ");
            sql.Append(" WHERE 1 = 1 ");

            if (tipo.ID != 0)
            {
                sql.Append("AND id_tipo_cupom = :1 ");
            }

            if (!String.IsNullOrEmpty(tipo.Nome))
            {
                sql.Append("AND nome_tipo_cupom = :2 ");
            }

            sql.Append("ORDER BY nome_tipo_cupom");


            pst.CommandText = sql.ToString();
            parameters = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("1", tipo.ID),
                    new NpgsqlParameter("2", tipo.Nome)
                };
            
            pst.Parameters.Clear();
            pst.Parameters.AddRange(parameters);
            pst.Connection = connection;
            pst.CommandType = CommandType.Text;

            reader = pst.ExecuteReader();

            // Lista de retorno da consulta do banco de dados, que conterá os produtores encontrados
            List<EntidadeDominio> tipos = new List<EntidadeDominio>();
            while (reader.Read())
            {
                tipo = new TipoCupom();
                tipo.ID = Convert.ToInt32(reader["id_tipo_cupom"]);
                tipo.Nome = reader["nome_tipo_cupom"].ToString();

                tipos.Add(tipo);
            }
            connection.Close();
            return tipos;
        }
    }
}
