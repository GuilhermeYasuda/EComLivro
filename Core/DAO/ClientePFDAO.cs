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
    public class ClientePFDAO : AbstractDAO
    {
        public ClientePFDAO() : base("tb_cliente_pf", "id_cli_pf")
        {

        }

        public override void Salvar(EntidadeDominio entidade)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            ClientePF cliente = (ClientePF)entidade;

            // construtor já passando conexão de ClientePFDAO para EnderecoDAO
            EnderecoDAO enderecoDAO = new EnderecoDAO(connection, false);
            foreach (Endereco endereco in cliente.Enderecos)
            {
                enderecoDAO.Salvar(endereco);
            }

            // construtor já passando conexão de ClientePFDAO para CartaoCreditoDAO
            // Cartão de cliente será efetuado com outra abordagem, apenas para compra será obrigatório informar um cc
            // para apenas o cadastro não será obrigatório
            //CartaoCreditoDAO ccDAO = new CartaoCreditoDAO(connection, false);
            //foreach (CartaoCredito cc in cliente.CartoesCredito)
            //{
            //    ccDAO.Salvar(cc);
            //}

            // construtor já passando conexão de cliente para telefone
            TelefoneDAO telefoneDAO = new TelefoneDAO(connection, false);
            telefoneDAO.Salvar(cliente.Telefone);

            pst.CommandText = "INSERT INTO tb_cliente_pf (nome_cli_pf, telefone_cli_fk, email_cli_pf, cpf_cli_pf, genero_cli_pf, dt_nascimento_cli_pf, ativo_cli_pf, dt_cadastro_cli_pf) VALUES (:1, :2, :3, :4, :5, :6, :7, :8) RETURNING id_cli_pf ";
            parameters = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("1", cliente.Nome),
                    new NpgsqlParameter("2", cliente.Telefone.ID),
                    new NpgsqlParameter("3", cliente.Email),
                    new NpgsqlParameter("4", cliente.CPF),
                    new NpgsqlParameter("5", cliente.Genero),
                    new NpgsqlParameter("6", cliente.DataNascimento),
                    new NpgsqlParameter("7", cliente.Ativo),
                    new NpgsqlParameter("8", cliente.DataCadastro)
                };

            pst.Parameters.Clear();
            pst.Parameters.AddRange(parameters);
            pst.Connection = connection;
            pst.CommandType = CommandType.Text;
            cliente.ID = entidade.ID = (int)pst.ExecuteScalar();
            // já executa o comando na linha anterior
            //pst.ExecuteNonQuery();

            // construtor para salvar o relacionamento n x n de cliente e endereço
            ClientePFXEnderecoDAO clienteXEnderecoDAO = new ClientePFXEnderecoDAO(connection, false);
            ClientePFXEndereco clientePFXEndereco = new ClientePFXEndereco();
            clientePFXEndereco.ID = cliente.ID;
            clientePFXEndereco.Endereco = cliente.Enderecos.First();
            clienteXEnderecoDAO.Salvar(clientePFXEndereco);

            // construtor para salvar o relacionamento n x n de cliente e cartão
            // Cartão de cliente será efetuado com outra abordagem, apenas para compra será obrigatório informar um cc
            // para apenas o cadastro não será obrigatório
            //ClientePFXCartaoDAO clienteXccDAO = new ClientePFXCartaoDAO(connection, false);
            //clienteXccDAO.Salvar(cliente);

            pst.CommandText = "COMMIT WORK";
            connection.Close();
            return;
        }

        public override void Alterar(EntidadeDominio entidade)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            ClientePF cliente = (ClientePF)entidade;

            pst.CommandText = "UPDATE tb_cliente_pf SET nome_cli_pf = :1, telefone_cli_fk = :2, email_cli_pf = :3, cpf_cli_pf = :4, genero_cli_pf = :5, dt_nascimento_cli_pf = :6, ativo_cli_pf = :7 WHERE id_cli_pf = :8 ";
            parameters = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("1", cliente.Nome),
                    new NpgsqlParameter("2", cliente.Telefone.ID),
                    new NpgsqlParameter("3", cliente.Email),
                    new NpgsqlParameter("4", cliente.CPF),
                    new NpgsqlParameter("5", cliente.Genero),
                    new NpgsqlParameter("6", cliente.DataNascimento),
                    new NpgsqlParameter("7", cliente.Ativo),
                    new NpgsqlParameter("8", cliente.ID)
                };

            pst.Parameters.Clear();
            pst.Parameters.AddRange(parameters);
            pst.Connection = connection;
            pst.CommandType = CommandType.Text;
            pst.ExecuteNonQuery();

            // construtor já passando conexão de cliente para endereço
            EnderecoDAO enderecoDAO = new EnderecoDAO(connection, false);
            //// primeiro faço exclusão dos registros existentes e depois faço nova inclusão
            //// para não ter erro na quantidade de endereços
            //enderecoDAO.Excluir(cliente);
            //foreach (Endereco endereco in cliente.Enderecos)
            //{
            //    enderecoDAO.Salvar(endereco);
            //}

            // alterado a forma que faz a alteração, quando o cliente for fazer alteração nos dados cadastrais,
            // apenas UM endereço pode ser alterado, logo não preciso mais do foreach e será feito alteração individual
            // tratamento de n-n de endereço e cliente será feito em outra DAO
            enderecoDAO.Alterar(cliente.Enderecos.First());
            
            // construtor já passando conexão de cliente para cc
            // Cartão de cliente será efetuado com outra abordagem, apenas para compra será obrigatório informar um cc
            // para apenas o cadastro não será obrigatório
            //CartaoCreditoDAO ccDAO = new CartaoCreditoDAO(connection, false);
            //// primeiro faço exclusão dos registros existentes e depois faço nova inclusão
            //// para não ter erro na quantidade de endereços
            //ccDAO.Excluir(cliente);
            //foreach (CartaoCredito cc in cliente.CartoesCredito)
            //{
            //    ccDAO.Salvar(cc);
            //}

            // construtor já passando conexão de cliente para telefone
            TelefoneDAO telefoneDAO = new TelefoneDAO(connection, false);
            telefoneDAO.Alterar(cliente.Telefone);

            pst.CommandText = "COMMIT WORK";
            connection.Close();
            return;
        }

        public override List<EntidadeDominio> Consultar(EntidadeDominio entidade)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            ClientePF cliente = (ClientePF)entidade;
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT * FROM tb_cliente_pf JOIN tb_telefone ON (tb_cliente_pf.telefone_cli_fk = tb_telefone.id_telefone) ");
            sql.Append("                            JOIN tb_tipo_telefone ON (tb_telefone.tipo_telefone_fk = tb_tipo_telefone.id_tipo_tel) ");
            sql.Append("                            JOIN tb_cliente_endereco ON (tb_cliente_pf.id_cli_pf = tb_cliente_endereco.id_cliente) ");
            sql.Append("                            JOIN tb_endereco ON (tb_cliente_endereco.id_endereco = tb_endereco.id_endereco) ");
            sql.Append("                            JOIN tb_cidades ON (tb_endereco.cidade_fk = tb_cidades.id_cidade) ");
            sql.Append("                            JOIN tb_estados ON (tb_cidades.estado_id = tb_estados.id_estado) ");
            sql.Append("                            JOIN tb_paises ON (tb_estados.pais_id = tb_paises.id_pais) ");
            sql.Append("                            JOIN tb_tipo_residencia ON (tb_endereco.tipo_residencia_fk = tb_tipo_residencia.id_tipo_res) ");
            sql.Append("                            JOIN tb_tipo_logradouro ON (tb_endereco.tipo_logradouro_fk = tb_tipo_logradouro.id_tipo_log) ");
            sql.Append("                            LEFT JOIN tb_cliente_cartao ON (tb_cliente_pf.id_cli_pf = tb_cliente_cartao.id_cliente) ");
            sql.Append("                            LEFT JOIN tb_cartao_credito ON (tb_cliente_cartao.id_cartao = tb_cartao_credito.id_cc) ");
            sql.Append("                            LEFT JOIN tb_bandeira ON (tb_cartao_credito.bandeira_cc_fk = tb_bandeira.id_bandeira) ");

            // WHERE sem efeito, usado apenas para poder diminuir o número de ifs da construção da query
            sql.Append("WHERE 1 = 1 ");

            if (cliente.ID != 0)
            {
                sql.Append("AND id_cli_pf = :1 ");
            }

            if (!String.IsNullOrEmpty(cliente.Nome))
            {
                sql.Append("AND nome_cli_pf = :2 ");
            }

            // caso precisar fazer consulta combinada para mais de um endereço
            // falta implementação e teste
            //if(cliente.Enderecos != null)
            //{
            //    List<Endereco> enderecos = cliente.Enderecos;

            //    foreach (Endereco endereco in enderecos)
            //    {
            //        if (endereco.ID != 0)
            //        {
            //            sql.Append("AND tb_endereco.id = :3 ");
            //        }

            //        if (endereco.TipoResidencia.ID != 0)
            //        {
            //            sql.Append("AND tb_tipo_residencia.id = :4 ");
            //        }

            //        if (endereco.TipoLogradouro.ID != 0)
            //        {
            //            sql.Append("AND tb_tipo_logradouro.id = :5 ");
            //        }

            //        if (!String.IsNullOrEmpty(endereco.Rua))
            //        {
            //            sql.Append("AND tb_endereco.logradouro = :6 ");
            //        }

            //        if (!String.IsNullOrEmpty(endereco.Numero))
            //        {
            //            sql.Append("AND tb_endereco.numero = :7 ");
            //        }

            //        if (!String.IsNullOrEmpty(endereco.Bairro))
            //        {
            //            sql.Append("AND tb_endereco.bairro = :8 ");
            //        }

            //        if (endereco.Cidade.ID != 0)
            //        {
            //            sql.Append("AND tb_cidades.id = :9 ");
            //        }

            //        if (endereco.Cidade.Estado.ID != 0)
            //        {
            //            sql.Append("AND tb_estados.id = :10 ");
            //        }

            //        if (endereco.Cidade.Estado.Pais.ID != 0)
            //        {
            //            sql.Append("AND tb_paises.id = :11 ");
            //        }

            //        if (!String.IsNullOrEmpty(endereco.CEP))
            //        {
            //            sql.Append("AND tb_endereco.cep = :12 ");
            //        }

            //        if (!String.IsNullOrEmpty(endereco.Observacao))
            //        {
            //            sql.Append("AND tb_endereco.observacao = :13 ");
            //        }
            //    }
            //}

            if (cliente.Enderecos.Count > 0)
            {
                if (cliente.Enderecos.ElementAt(0).ID != 0)
                {
                    sql.Append("AND id_endereco = :3 ");
                }

                if (cliente.Enderecos.ElementAt(0).TipoResidencia.ID != 0)
                {
                    sql.Append("AND id_tipo_res = :4 ");
                }

                if (cliente.Enderecos.ElementAt(0).TipoLogradouro.ID != 0)
                {
                    sql.Append("AND id_tipo_log = :5 ");
                }

                if (!String.IsNullOrEmpty(cliente.Enderecos.ElementAt(0).Rua))
                {
                    sql.Append("AND log_endereco = :6 ");
                }

                if (!String.IsNullOrEmpty(cliente.Enderecos.ElementAt(0).Numero))
                {
                    sql.Append("AND numero_endereco = :7 ");
                }

                if (!String.IsNullOrEmpty(cliente.Enderecos.ElementAt(0).Bairro))
                {
                    sql.Append("AND bairro_endereco = :8 ");
                }

                if (cliente.Enderecos.ElementAt(0).Cidade.ID != 0)
                {
                    sql.Append("AND id_cidade = :9 ");
                }

                if (cliente.Enderecos.ElementAt(0).Cidade.Estado.ID != 0)
                {
                    sql.Append("AND id_estado = :10 ");
                }

                if (cliente.Enderecos.ElementAt(0).Cidade.Estado.Pais.ID != 0)
                {
                    sql.Append("AND id_pais = :11 ");
                }

                if (!String.IsNullOrEmpty(cliente.Enderecos.ElementAt(0).CEP))
                {
                    sql.Append("AND cep_endereco = :12 ");
                }

                if (!String.IsNullOrEmpty(cliente.Enderecos.ElementAt(0).Observacao))
                {
                    sql.Append("AND observacao_endereco = :13 ");
                }
            }

            if (cliente.Telefone != null)
            {
                Telefone telefone = cliente.Telefone;
                if (telefone.ID != 0)
                {
                    sql.Append("AND id_telefone = :14 ");
                }

                if (telefone.TipoTelefone.ID != 0)
                {
                    sql.Append("AND id_tipo_tel = :15 ");
                }

                if (!String.IsNullOrEmpty(telefone.DDD))
                {
                    sql.Append("AND ddd_telefone = :16 ");
                }

                if (!String.IsNullOrEmpty(telefone.NumeroTelefone))
                {
                    sql.Append("AND numero_telefone = :17 ");
                }
            }

            if (!String.IsNullOrEmpty(cliente.Email))
            {
                sql.Append("AND email_cli_pf = :18 ");
            }

            // caso precise fazer pesquisa combinada para mais de um cartão
            // falta implementação e teste
            //if (cliente.CartoesCredito != null)
            //{
            //    List<CartaoCredito> cartoes = cliente.CartoesCredito;

            //    foreach (CartaoCredito cc in cliente.CartoesCredito)
            //    {
            //        if (cc.ID != 0)
            //        {
            //            sql.Append("AND tb_cartao_credito.id = :19 ");
            //        }

            //        if (!String.IsNullOrEmpty(cc.NomeImpresso))
            //        {
            //            sql.Append("AND tb_cartao_credito.nome_impresso = :20 ");
            //        }

            //        if (!String.IsNullOrEmpty(cc.NumeroCC))
            //        {
            //            sql.Append("AND tb_cartao_credito.numero = :21 ");
            //        }

            //        if (cc.Bandeira.ID != 0)
            //        {
            //            sql.Append("AND tb_bandeira.id = :22 ");
            //        }

            //        if (!String.IsNullOrEmpty(cc.CodigoSeguranca))
            //        {
            //            sql.Append("AND tb_cartao_credito.codigo_seguranca = :23 ");
            //        }

            //        if (cc.DataVencimento != null && !cc.DataVencimento.ToString().Equals(null))
            //        {
            //            sql.Append("AND tb_cartao_credito.dt_vencimento = :24 ");
            //        }
            //    }
            //}

            if (cliente.CartoesCredito.Count > 0)
            {
                if (cliente.CartoesCredito.ElementAt(0).ID != 0)
                {
                    sql.Append("AND id_cc = :19 ");
                }

                if (!String.IsNullOrEmpty(cliente.CartoesCredito.ElementAt(0).NomeImpresso))
                {
                    sql.Append("AND nome_impresso_cc = :20 ");
                }

                if (!String.IsNullOrEmpty(cliente.CartoesCredito.ElementAt(0).NumeroCC))
                {
                    sql.Append("AND numero_cc = :21 ");
                }

                if (cliente.CartoesCredito.ElementAt(0).Bandeira.ID != 0)
                {
                    sql.Append("AND id_bandeira = :22 ");
                }

                if (!String.IsNullOrEmpty(cliente.CartoesCredito.ElementAt(0).CodigoSeguranca))
                {
                    sql.Append("AND codigo_seguranca_cc = :23 ");
                }
            }

            if (!String.IsNullOrEmpty(cliente.CPF))
            {
                sql.Append("AND cpf_cli_pf = :24 ");
            }

            if (!cliente.Genero.Equals("") && !cliente.Genero.Equals(null) && !cliente.Genero.Equals('\0') && !cliente.Genero.Equals('0'))
            {
                sql.Append("AND genero_cli_pf = :25 ");
            }

            if (cliente.DataNascimento != null && !cliente.DataNascimento.Equals(null))
            {
                sql.Append("AND dt_nascimento_cli_pf = :26 ");
            }

            if (!cliente.Ativo.Equals("") && !cliente.Ativo.Equals(null))
            {
                sql.Append("AND ativo_cli_pf = :27 ");
            }

            if (cliente.DataCadastro != null)
            {
                sql.Append("AND dt_cadastro_cli_pf = :28 ");
            }

            sql.Append("ORDER BY id_cli_pf, tb_endereco.id_endereco, tb_cartao_credito.id_cc ");

            pst.CommandText = sql.ToString();
            parameters = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("1", cliente.ID),
                    new NpgsqlParameter("2", cliente.Nome),
                    new NpgsqlParameter("14", cliente.Telefone.ID),
                    new NpgsqlParameter("15", cliente.Telefone.TipoTelefone.ID),
                    new NpgsqlParameter("16", cliente.Telefone.DDD),
                    new NpgsqlParameter("17", cliente.Telefone.NumeroTelefone),
                    new NpgsqlParameter("18", cliente.Email),
                    new NpgsqlParameter("24", cliente.CPF),
                    new NpgsqlParameter("25", cliente.Genero),
                    new NpgsqlParameter("27", cliente.Ativo)
                };

            if (cliente.Enderecos.Count > 0)
            {
                NpgsqlParameter[] parametersAux = new NpgsqlParameter[]
                   {
                    new NpgsqlParameter("3", cliente.Enderecos.ElementAt(0).ID),
                    new NpgsqlParameter("4", cliente.Enderecos.ElementAt(0).TipoResidencia.ID),
                    new NpgsqlParameter("5", cliente.Enderecos.ElementAt(0).TipoLogradouro.ID),
                    new NpgsqlParameter("6", cliente.Enderecos.ElementAt(0).Rua),
                    new NpgsqlParameter("7", cliente.Enderecos.ElementAt(0).Numero),
                    new NpgsqlParameter("8", cliente.Enderecos.ElementAt(0).Bairro),
                    new NpgsqlParameter("9", cliente.Enderecos.ElementAt(0).Cidade.ID),
                    new NpgsqlParameter("10", cliente.Enderecos.ElementAt(0).Cidade.Estado.ID),
                    new NpgsqlParameter("11", cliente.Enderecos.ElementAt(0).Cidade.Estado.Pais.ID),
                    new NpgsqlParameter("12", cliente.Enderecos.ElementAt(0).CEP),
                    new NpgsqlParameter("13", cliente.Enderecos.ElementAt(0).Observacao)
                   };

                parameters.Concat(parametersAux);
            }

            if (cliente.Enderecos.Count > 0)
            {
                NpgsqlParameter[] parametersAux = new NpgsqlParameter[]
                   {
                    new NpgsqlParameter("19", cliente.CartoesCredito.ElementAt(0).ID),
                    new NpgsqlParameter("20", cliente.CartoesCredito.ElementAt(0).NomeImpresso),
                    new NpgsqlParameter("21", cliente.CartoesCredito.ElementAt(0).NumeroCC),
                    new NpgsqlParameter("22", cliente.CartoesCredito.ElementAt(0).Bandeira.ID),
                    new NpgsqlParameter("23", cliente.CartoesCredito.ElementAt(0).CodigoSeguranca)
                   };

                parameters.Concat(parametersAux);
            }

            if (cliente.DataNascimento != null)
            {
                NpgsqlParameter[] parametersAux = new NpgsqlParameter[]
                   {
                    new NpgsqlParameter("26", cliente.DataNascimento)
                   };

                parameters.Concat(parametersAux);
            }

            if (cliente.DataCadastro != null)
            {
                NpgsqlParameter[] parametersAux = new NpgsqlParameter[]
                   {
                    new NpgsqlParameter("28", cliente.DataCadastro)
                   };

                parameters.Concat(parametersAux);
            }

            pst.Parameters.Clear();
            pst.Parameters.AddRange(parameters);
            pst.Connection = connection;
            pst.CommandType = CommandType.Text;

            reader = pst.ExecuteReader();

            // Lista de retorno da consulta do banco de dados, que conterá os clientes encontrados
            List<EntidadeDominio> clientes = new List<EntidadeDominio>();

            ClientePF clienteAux = new ClientePF();
            clienteAux.ID = 0;

            Endereco endereco = new Endereco();
            CartaoCredito cc = new CartaoCredito();

            Endereco enderecoAux = new Endereco();
            CartaoCredito ccAux = new CartaoCredito();

            cliente = new ClientePF();

            while (reader.Read())
            {
                // verifica se cliente que está trazendo do BD é igual ao anterior
                if (Convert.ToInt32(reader["id_cli_pf"]) != clienteAux.ID)
                {
                    cliente = new ClientePF();
                    cliente.ID = Convert.ToInt32(reader["id_cli_pf"]);

                    // passando id do cliente que está vindo para o auxiliar
                    clienteAux.ID = cliente.ID;

                    cliente.Nome = reader["nome_cli_pf"].ToString();

                    //// -------------------- ENDEREÇO - COMEÇO ----------------------------------
                    //ClientePFXEnderecoDAO clientePFXEnderecoDAO = new ClientePFXEnderecoDAO();
                    //List<EntidadeDominio> entidades = clientePFXEnderecoDAO.Consultar(cliente);
                    //foreach (EntidadeDominio endereco in entidades)
                    //    //foreach (Endereco endereco in new ClientePFXEnderecoDAO(connection, ctrlTransaction).Consultar(cliente))
                    //{
                    //    cliente.Enderecos.Add((Endereco)endereco);
                    //}
                    endereco = new Endereco();

                    endereco.ID = Convert.ToInt32(reader["id_endereco"]);
                    endereco.Nome = reader["nome_endereco"].ToString();
                    endereco.Destinatario = reader["destinatario_endereco"].ToString();
                    endereco.TipoResidencia.ID = Convert.ToInt32(reader["id_tipo_res"]);
                    endereco.TipoResidencia.Nome = reader["nome_tipo_res"].ToString();
                    endereco.TipoLogradouro.ID = Convert.ToInt32(reader["id_tipo_log"]);
                    endereco.TipoLogradouro.Nome = reader["nome_tipo_log"].ToString();
                    endereco.Rua = reader["log_endereco"].ToString();
                    endereco.Numero = reader["numero_endereco"].ToString();
                    endereco.Bairro = reader["bairro_endereco"].ToString();
                    endereco.Cidade.ID = Convert.ToInt32(reader["id_cidade"].ToString());
                    endereco.Cidade.Nome = reader["nome_cidade"].ToString();
                    endereco.Cidade.Estado.ID = Convert.ToInt32(reader["id_estado"].ToString());
                    endereco.Cidade.Estado.Nome = reader["nome_estado"].ToString();
                    endereco.Cidade.Estado.Sigla = reader["sigla_estado"].ToString();
                    endereco.Cidade.Estado.Pais.ID = Convert.ToInt32(reader["id_pais"].ToString());
                    endereco.Cidade.Estado.Pais.Nome = reader["nome_pais"].ToString();
                    endereco.Cidade.Estado.Pais.Sigla = reader["sigla_pais"].ToString();
                    endereco.CEP = reader["cep_endereco"].ToString();
                    endereco.Observacao = reader["observacao_endereco"].ToString();

                    cliente.Enderecos.Add(endereco);
                    enderecoAux.ID = endereco.ID;
                    //// -------------------- ENDEREÇO - FIM ----------------------------------

                    // -------------------- TELEFONE - COMEÇO ----------------------------------
                    cliente.Telefone.ID = Convert.ToInt32(reader["id_telefone"]);
                    cliente.Telefone.TipoTelefone.ID = Convert.ToInt32(reader["id_tipo_tel"]);
                    cliente.Telefone.TipoTelefone.Nome = reader["nome_tipo_tel"].ToString();
                    cliente.Telefone.DDD = reader["ddd_telefone"].ToString();
                    cliente.Telefone.NumeroTelefone = reader["numero_telefone"].ToString();
                    // -------------------- TELEFONE - FIM ----------------------------------

                    // -------------------- CARTÃO CRÉDITO - COMEÇO ----------------------------------
                    //ClientePFXCartaoDAO clientePFXCartaoDAO = new ClientePFXCartaoDAO(connection, ctrlTransaction);
                    ////foreach (Endereco endereco in clientePFXCartaoDAO.Consultar(cliente))
                    //// testar
                    //foreach (CartaoCredito cc in new ClientePFXCartaoDAO(connection, ctrlTransaction).Consultar(cliente))
                    //{
                    //    cliente.CartoesCredito.Add(cc);
                    //}

                    cc = new CartaoCredito();
                    if (!DBNull.Value.Equals(reader["id_cc"]))
                    {
                        cc.ID = Convert.ToInt32(reader["id_cc"]);
                        cc.ID = Convert.ToInt32(reader["id_cc"]);
                        cc.NomeImpresso = reader["nome_impresso_cc"].ToString();
                        cc.NumeroCC = reader["numero_cc"].ToString();
                        cc.Bandeira.ID = Convert.ToInt32(reader["id_bandeira"]);
                        cc.Bandeira.Nome = reader["nome_bandeira"].ToString();
                        cc.CodigoSeguranca = reader["codigo_seguranca_cc"].ToString();
                    }

                    cliente.CartoesCredito.Add(cc);

                    ccAux.ID = cc.ID;
                    // -------------------- CARTÃO CRÉDITO - FIM ----------------------------------

                    cliente.Email = reader["email_cli_pf"].ToString();
                    cliente.CPF = reader["cpf_cli_pf"].ToString();
                    cliente.Genero = reader["genero_cli_pf"].ToString().First();
                    cliente.DataNascimento = Convert.ToDateTime(reader["dt_nascimento_cli_pf"].ToString());
                    cliente.Ativo = reader["ativo_cli_pf"].ToString().First();
                    cliente.DataCadastro = Convert.ToDateTime(reader["dt_cadastro_cli_pf"].ToString());

                }
                else
                {
                    //// -------------------- ENDEREÇO - COMEÇO ----------------------------------
                    //ClientePFXEnderecoDAO clientePFXEnderecoDAO = new ClientePFXEnderecoDAO();
                    //List<EntidadeDominio> entidades = clientePFXEnderecoDAO.Consultar(cliente);
                    //foreach (EntidadeDominio endereco in entidades)
                    //    //foreach (Endereco endereco in new ClientePFXEnderecoDAO(connection, ctrlTransaction).Consultar(cliente))
                    //{
                    //    cliente.Enderecos.Add((Endereco)endereco);
                    //}

                    endereco = new Endereco();

                    endereco.ID = Convert.ToInt32(reader["id_endereco"]);
                    if (enderecoAux.ID != endereco.ID)
                    {
                        endereco.Nome = reader["nome_endereco"].ToString();
                        endereco.Destinatario = reader["destinatario_endereco"].ToString();
                        endereco.TipoResidencia.ID = Convert.ToInt32(reader["id_tipo_res"]);
                        endereco.TipoResidencia.Nome = reader["nome_tipo_res"].ToString();
                        endereco.TipoLogradouro.ID = Convert.ToInt32(reader["id_tipo_log"]);
                        endereco.TipoLogradouro.Nome = reader["nome_tipo_log"].ToString();
                        endereco.Rua = reader["log_endereco"].ToString();
                        endereco.Numero = reader["numero_endereco"].ToString();
                        endereco.Bairro = reader["bairro_endereco"].ToString();
                        endereco.Cidade.ID = Convert.ToInt32(reader["id_cidade"].ToString());
                        endereco.Cidade.Nome = reader["nome_cidade"].ToString();
                        endereco.Cidade.Estado.ID = Convert.ToInt32(reader["id_estado"].ToString());
                        endereco.Cidade.Estado.Nome = reader["nome_estado"].ToString();
                        endereco.Cidade.Estado.Sigla = reader["sigla_estado"].ToString();
                        endereco.Cidade.Estado.Pais.ID = Convert.ToInt32(reader["id_pais"].ToString());
                        endereco.Cidade.Estado.Pais.Nome = reader["nome_pais"].ToString();
                        endereco.Cidade.Estado.Pais.Sigla = reader["sigla_pais"].ToString();
                        endereco.CEP = reader["cep_endereco"].ToString();
                        endereco.Observacao = reader["observacao_endereco"].ToString();

                        cliente.Enderecos.Add(endereco);

                        enderecoAux.ID = endereco.ID;
                    }
                    //// -------------------- ENDEREÇO - FIM ----------------------------------

                    // -------------------- CARTÃO CRÉDITO - COMEÇO ----------------------------------
                    //ClientePFXCartaoDAO clientePFXCartaoDAO = new ClientePFXCartaoDAO(connection, ctrlTransaction);
                    ////foreach (Endereco endereco in clientePFXCartaoDAO.Consultar(cliente))
                    //// testar
                    //foreach (CartaoCredito cc in new ClientePFXCartaoDAO(connection, ctrlTransaction).Consultar(cliente))
                    //{
                    //    cliente.CartoesCredito.Add(cc);
                    //}

                    cc = new CartaoCredito();
                    if (!DBNull.Value.Equals(reader["id_cc"]))
                    {
                        cc.ID = Convert.ToInt32(reader["id_cc"]);
                        if (ccAux.ID != cc.ID)
                        {
                            cc.NomeImpresso = reader["nome_impresso_cc"].ToString();
                            cc.NumeroCC = reader["numero_cc"].ToString();
                            cc.Bandeira.ID = Convert.ToInt32(reader["id_bandeira"]);
                            cc.Bandeira.Nome = reader["nome_bandeira"].ToString();
                            cc.CodigoSeguranca = reader["codigo_seguranca_cc"].ToString();

                            cliente.CartoesCredito.Add(cc);

                            ccAux.ID = cc.ID;
                        }
                    }

                    // -------------------- CARTÃO CRÉDITO - FIM ----------------------------------
                }

                //// verifica se cliente.ID é maior que clienteAux.ID e não é a primeira interação (clienteAux.ID > 0)
                //if (Convert.ToInt32(reader["id_cli_pf"]) > clienteAux.ID && clienteAux.ID > 0)
                //{
                //    clientes.Add(cliente);
                //}
                clientes.Add(cliente);
            }
            connection.Close();
            return clientes;
        }

    }
}
