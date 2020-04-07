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
    public class ClientePFXEnderecoDAO : AbstractDAO
    {
        // construtor padrão
        public ClientePFXEnderecoDAO() : base("tb_cliente_endereco", "id_cliente")
        {

        }

        // construtor padrão
        public ClientePFXEnderecoDAO(string id) : base("tb_cliente_endereco", id)
        {

        }

        // construtor para DAOs que também utilizarão o DAO do relacionamento n x n de cliente e endereço
        public ClientePFXEnderecoDAO(NpgsqlConnection connection, bool ctrlTransaction) : base(connection, ctrlTransaction, "tb_cliente_endereco", "id_cliente")
        {

        }

        public override void Salvar(EntidadeDominio entidade)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            ClientePFXEndereco clientePFXEndereco = (ClientePFXEndereco)entidade;

            // construtor já passando conexão de cliente para cartao
            EnderecoDAO enderecoDAO = new EnderecoDAO(connection, false);
            enderecoDAO.Salvar(clientePFXEndereco.Endereco);

            pst.CommandText = "INSERT INTO tb_cliente_endereco(id_cliente, id_endereco) VALUES (:1, :2); ";
            parameters = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("1", clientePFXEndereco.ID),
                    new NpgsqlParameter("2", clientePFXEndereco.Endereco.ID)
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
            // e para que não tenha problema na hora de alteração de quantidade de endereços
            throw new NotImplementedException();
        }

        public override List<EntidadeDominio> Consultar(EntidadeDominio entidade)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            ClientePFXEndereco clientePFXEndereco = (ClientePFXEndereco)entidade;
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT * FROM tb_cliente_endereco JOIN tb_endereco ON (tb_cliente_endereco.id_endereco = tb_endereco.id_endereco) ");
            sql.Append("                                    JOIN tb_cidades ON (tb_endereco.cidade_fk = tb_cidades.id_cidade) ");
            sql.Append("                                    JOIN tb_estados ON (tb_cidades.estado_id = tb_estados.id_estado) ");
            sql.Append("                                    JOIN tb_paises ON (tb_estados.pais_id = tb_paises.id_pais) ");
            sql.Append("                                    JOIN tb_tipo_residencia ON (tb_endereco.tipo_residencia_fk = tb_tipo_residencia.id_tipo_res) ");
            sql.Append("                                    JOIN tb_tipo_logradouro ON (tb_endereco.tipo_logradouro_fk = tb_tipo_logradouro.id_tipo_log) ");

            // WHERE sem efeito, usado apenas para poder diminuir o número de ifs da construção da query
            sql.Append("WHERE 1 = 1 ");

            if (clientePFXEndereco.ID != 0)
            {
                sql.Append("AND id_cliente = :1 ");
            }

            if (clientePFXEndereco.Endereco != null)
            {
                if (clientePFXEndereco.Endereco.ID != 0)
                {
                    sql.Append(" AND id_endereco = :2 ");
                }
            }

            sql.Append("ORDER BY tb_cliente_endereco.id_cliente, tb_cliente_endereco.id_endereco ");

            pst.CommandText = sql.ToString();
            parameters = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("1", clientePFXEndereco.ID),
                    new NpgsqlParameter("2", clientePFXEndereco.Endereco.ID)
                };

            pst.Parameters.Clear();
            pst.Parameters.AddRange(parameters);
            pst.Connection = connection;
            pst.CommandType = CommandType.Text;

            reader = pst.ExecuteReader();

            // Lista de retorno da consulta do banco de dados, que conterá os endereços do cliente encontrados
            List<EntidadeDominio> clientePFXEnderecos = new List<EntidadeDominio>();
            while (reader.Read())
            {
                clientePFXEndereco = new ClientePFXEndereco();

                clientePFXEndereco.ID = Convert.ToInt32(reader["id_cliente"]);

                clientePFXEndereco.Endereco.ID = Convert.ToInt32(reader["id_endereco"]);
                clientePFXEndereco.Endereco.Nome = reader["nome_endereco"].ToString();
                clientePFXEndereco.Endereco.Destinatario = reader["destinatario_endereco"].ToString();
                clientePFXEndereco.Endereco.TipoResidencia.ID = Convert.ToInt32(reader["id_tipo_res"]);
                clientePFXEndereco.Endereco.TipoResidencia.Nome = reader["nome_tipo_res"].ToString();
                clientePFXEndereco.Endereco.TipoLogradouro.ID = Convert.ToInt32(reader["id_tipo_log"]);
                clientePFXEndereco.Endereco.TipoLogradouro.Nome = reader["nome_tipo_log"].ToString();
                clientePFXEndereco.Endereco.Rua = reader["log_endereco"].ToString();
                clientePFXEndereco.Endereco.Numero = reader["numero_endereco"].ToString();
                clientePFXEndereco.Endereco.Bairro = reader["bairro_endereco"].ToString();
                clientePFXEndereco.Endereco.Cidade.ID = Convert.ToInt32(reader["id_cidade"].ToString());
                clientePFXEndereco.Endereco.Cidade.Nome = reader["nome_cidade"].ToString();
                clientePFXEndereco.Endereco.Cidade.Estado.ID = Convert.ToInt32(reader["id_estado"].ToString());
                clientePFXEndereco.Endereco.Cidade.Estado.Nome = reader["nome_estado"].ToString();
                clientePFXEndereco.Endereco.Cidade.Estado.Sigla = reader["sigla_estado"].ToString();
                clientePFXEndereco.Endereco.Cidade.Estado.Pais.ID = Convert.ToInt32(reader["id_pais"].ToString());
                clientePFXEndereco.Endereco.Cidade.Estado.Pais.Nome = reader["nome_pais"].ToString();
                clientePFXEndereco.Endereco.Cidade.Estado.Pais.Sigla = reader["sigla_pais"].ToString();
                clientePFXEndereco.Endereco.CEP = reader["cep_endereco"].ToString();
                clientePFXEndereco.Endereco.Observacao = reader["observacao_endereco"].ToString();

                clientePFXEnderecos.Add(clientePFXEndereco);
            }
            connection.Close();
            return clientePFXEnderecos;
        }
    }
}
