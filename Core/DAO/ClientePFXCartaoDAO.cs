using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Npgsql;
using System.Data;
using Dominio.Cliente;

namespace Core.DAO
{
    public class ClientePFXCartaoDAO : AbstractDAO
    {
        // construtor padrão
        public ClientePFXCartaoDAO() : base("tb_cliente_cartao", "id_cliente")
        {

        }

        // construtor padrão
        public ClientePFXCartaoDAO(string id) : base("tb_cliente_cartao", id)
        {

        }

        // construtor para DAOs que também utilizarão o DAO do relacionamento n x n de cliente e cartão
        public ClientePFXCartaoDAO(NpgsqlConnection connection, bool ctrlTransaction) : base(connection, ctrlTransaction, "tb_cliente_cartao", "id_cliente")
        {

        }

        public override void Salvar(EntidadeDominio entidade)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            ClientePFXCC clientePFXCC = (ClientePFXCC)entidade;

            // construtor já passando conexão de cliente para cartao
            CartaoCreditoDAO ccDAO = new CartaoCreditoDAO(connection, false);
            ccDAO.Salvar(clientePFXCC.CC);

            pst.CommandText = "INSERT INTO tb_cliente_cartao(id_cliente, id_cartao) VALUES (:1, :2); ";
            parameters = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("1", clientePFXCC.ID),
                    new NpgsqlParameter("2", clientePFXCC.CC.ID)
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
            // não implementado devido que será excluído TODOS os registros para inserção do novo
            // e para que não tenha problema na hora de alteração de quantidade de cartões
            throw new NotImplementedException();
        }

        public override List<EntidadeDominio> Consultar(EntidadeDominio entidade)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            ClientePFXCC clientePFXCC = (ClientePFXCC)entidade;
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT * FROM tb_cliente_cartao JOIN tb_cartao_credito ON (tb_cliente_cartao.id_cartao = tb_cartao_credito.id_cc) ");
            sql.Append("                                JOIN tb_bandeira ON (tb_cartao_credito.bandeira_cc_fk = tb_bandeira.id_bandeira) ");

            // WHERE sem efeito, usado apenas para poder diminuir o número de ifs da construção da query
            sql.Append("WHERE 1 = 1 ");

            if (clientePFXCC.ID != 0)
            {
                sql.Append("AND id_cliente = :1 ");
            }

            if (clientePFXCC.CC != null)
            {
                if (clientePFXCC.CC.ID != 0)
                {
                    sql.Append(" AND id_cartao = :2 ");
                }
            }

            sql.Append("ORDER BY id_cliente ");

            pst.CommandText = sql.ToString();
            parameters = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("1", clientePFXCC.ID),
                    new NpgsqlParameter("2", clientePFXCC.CC.ID)
                };

            pst.Parameters.Clear();
            pst.Parameters.AddRange(parameters);
            pst.Connection = connection;
            pst.CommandType = CommandType.Text;

            reader = pst.ExecuteReader();

            // Lista de retorno da consulta do banco de dados, que conterá os cartões do cliente encontrados
            List<EntidadeDominio> clientePFXCCs = new List<EntidadeDominio>();
            while (reader.Read())
            {
                clientePFXCC = new ClientePFXCC();
                clientePFXCC.ID = Convert.ToInt32(reader["id_cliente"]);
                clientePFXCC.CC.ID = Convert.ToInt32(reader["id_cc"]);
                clientePFXCC.CC.NomeImpresso = reader["nome_impresso_cc"].ToString();
                clientePFXCC.CC.NumeroCC = reader["numero_cc"].ToString();
                clientePFXCC.CC.Bandeira.ID = Convert.ToInt32(reader["id_bandeira"]);
                clientePFXCC.CC.Bandeira.Nome = reader["nome_bandeira"].ToString();
                clientePFXCC.CC.CodigoSeguranca = reader["codigo_seguranca_cc"].ToString();

                clientePFXCCs.Add(clientePFXCC);
            }
            connection.Close();
            return clientePFXCCs;
        }
    }
}
