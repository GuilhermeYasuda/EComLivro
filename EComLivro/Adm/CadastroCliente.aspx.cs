using Core.Aplicacao;
using Dominio.Cliente;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EComLivro.Adm
{
    public partial class CadastroCliente : ViewGenerico
    {
        ClientePF cliente = new ClientePF();
        private Resultado resultado = new Resultado();
        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dropIdGenero.DataSource = GeneroDatatable();
                dropIdGenero.DataValueField = "ID";
                dropIdGenero.DataTextField = "Name";
                dropIdGenero.DataBind();

                dropIdTipoTelefone.DataSource = TipoTelefoneDatatable(commands["CONSULTAR"].execute(new TipoTelefone()).Entidades.Cast<TipoTelefone>().ToList());
                dropIdTipoTelefone.DataValueField = "ID";
                dropIdTipoTelefone.DataTextField = "Name";
                dropIdTipoTelefone.DataBind();

                dropIdTipoResidencia.DataSource = TipoResidenciaDatatable(commands["CONSULTAR"].execute(new TipoResidencia()).Entidades.Cast<TipoResidencia>().ToList());
                dropIdTipoResidencia.DataValueField = "ID";
                dropIdTipoResidencia.DataTextField = "Name";
                dropIdTipoResidencia.DataBind();

                dropIdTipoLogradouro.DataSource = TipoLogradouroDatatable(commands["CONSULTAR"].execute(new TipoLogradouro()).Entidades.Cast<TipoLogradouro>().ToList());
                dropIdTipoLogradouro.DataValueField = "ID";
                dropIdTipoLogradouro.DataTextField = "Name";
                dropIdTipoLogradouro.DataBind();

                dropIdPais.DataSource = PaisDatatable(commands["CONSULTAR"].execute(new Pais()).Entidades.Cast<Pais>().ToList());
                dropIdPais.DataValueField = "ID";
                dropIdPais.DataTextField = "Name";
                dropIdPais.DataBind();

                dropIdEstado.DataSource = EstadoDatatable(commands["CONSULTAR"].execute(new Estado()).Entidades.Cast<Estado>().ToList());
                dropIdEstado.DataValueField = "ID";
                dropIdEstado.DataTextField = "Name";
                dropIdEstado.DataBind();

                dropIdCidade.DataSource = CidadeDatatable(commands["CONSULTAR"].execute(new Cidade()).Entidades.Cast<Cidade>().ToList());
                dropIdCidade.DataValueField = "ID";
                dropIdCidade.DataTextField = "Name";
                dropIdCidade.DataBind();

                // cadastro de cartão será em outra tela
                //dropIdBandeira.DataSource = BandeiraDatatable(commands["CONSULTAR"].execute(new Bandeira()).Entidades.Cast<Bandeira>().ToList());
                //dropIdBandeira.DataValueField = "ID";
                //dropIdBandeira.DataTextField = "Name";
                //dropIdBandeira.DataBind();


                if (!string.IsNullOrEmpty(Request.QueryString["idClientePF"]))
                {
                    btnCadastrar.Visible = false;
                    btnAlterar.Visible = true;
                    idLinhaCodigo.Visible = true;
                    txtIdClientePF.Visible = true;
                    txtIdClientePF.Enabled = false;
                    idTitle.InnerText = "Alterar Cliente";
                    idBreadCrumb.InnerText = "Alterar Cliente";
                    cliente.ID = Convert.ToInt32(Request.QueryString["idClientePF"]);
                    entidades = commands["CONSULTAR"].execute(cliente).Entidades;
                    cliente = (ClientePF)entidades.ElementAt(0);
                    txtIdClientePF.Text = cliente.ID.ToString();

                    //int i = cliente.Nome.LastIndexOf(" ");
                    // ------------------------ Dados Pessoais - COMEÇO ------------------------------
                    txtNome.Text = cliente.Nome.Substring(0, cliente.Nome.LastIndexOf(" "));
                    txtSobrenome.Text = cliente.Nome.Substring(cliente.Nome.LastIndexOf(" ") + 1);
                    txtCPF.Text = cliente.CPF;
                    txtDtNascimento.Text = cliente.DataNascimento.ToString().Substring(6, 4) + "-" +
                                            cliente.DataNascimento.ToString().Substring(3, 2) + "-" +
                                            cliente.DataNascimento.ToString().Substring(0, 2);

                    dropIdGenero.SelectedValue = cliente.Genero.ToString();
                    // ------------------------ Dados Pessoais - FIM ------------------------------

                    // ------------------------ Dados Contato - COMEÇO ------------------------------
                    txtEmail.Text = cliente.Email;

                    txtIdTelefone.Text = cliente.Telefone.ID.ToString();
                    dropIdTipoTelefone.SelectedValue = cliente.Telefone.TipoTelefone.ID.ToString();
                    txtDDD.Text = cliente.Telefone.DDD;
                    txtTelefone.Text = cliente.Telefone.NumeroTelefone;
                    // ------------------------ Dados Contato - FIM ------------------------------

                    // ------------------------ Dados Endereço - COMEÇO ------------------------------
                    // Na alteração só altera os dados cadastrais ORIGINAIS, 
                    // os outros endereços serão alterados de forma diferente
                    txtIdEndereco.Text = cliente.Enderecos.First().ID.ToString();

                    txtNomeEndereco.Text = cliente.Enderecos.First().Nome;
                    txtNomeDestinatario.Text = cliente.Enderecos.First().Destinatario;
                    dropIdTipoResidencia.SelectedValue = cliente.Enderecos.First().TipoResidencia.ID.ToString();
                    dropIdTipoLogradouro.SelectedValue = cliente.Enderecos.First().TipoLogradouro.ID.ToString();
                    txtRua.Text = cliente.Enderecos.First().Rua;
                    txtNumero.Text = cliente.Enderecos.First().Numero;
                    txtBairro.Text = cliente.Enderecos.First().Bairro;
                    txtCEP.Text = cliente.Enderecos.First().CEP;
                    txtObservacao.Text = cliente.Enderecos.First().Observacao;
                    dropIdPais.SelectedValue = cliente.Enderecos.First().Cidade.Estado.Pais.ID.ToString();
                    dropIdEstado.SelectedValue = cliente.Enderecos.First().Cidade.Estado.ID.ToString();
                    dropIdEstado.Enabled = true;
                    dropIdCidade.SelectedValue = cliente.Enderecos.First().Cidade.ID.ToString();
                    dropIdCidade.Enabled = true;
                    // ------------------------ Dados Endereço - FIM ------------------------------
                }
                else if (!string.IsNullOrEmpty(Request.QueryString["delIdClientePF"]))
                {
                    cliente.ID = Convert.ToInt32(Request.QueryString["delIdClientePF"]);
                    resultado = commands["EXCLUIR"].execute(cliente);

                    // verifica se deu erro de validação
                    if (!string.IsNullOrEmpty(resultado.Msg))
                    {
                        lblResultado.Visible = true;
                        lblResultado.Text = resultado.Msg;
                    }
                    // caso tudo OK delera e redireciona o usuário para ListaClientes.aspx
                    else
                    {
                        Response.Redirect("./ListaCliente.aspx");
                    }
                }

            }
        }

        public static DataTable GeneroDatatable()
        {
            DataTable data = new DataTable();
            data.Columns.Add(new DataColumn("ID", typeof(char)));
            data.Columns.Add(new DataColumn("Name", typeof(string)));

            DataRow dr = data.NewRow();
            dr[0] = '0';
            dr[1] = "Selecione um Gênero";
            data.Rows.Add(dr);

            dr = data.NewRow();
            dr[0] = 'F';
            dr[1] = "F".ToString();
            data.Rows.Add(dr);

            dr = data.NewRow();
            dr[0] = 'M';
            dr[1] = "M".ToString();
            data.Rows.Add(dr);

            return data;
        }

        public static DataTable TipoTelefoneDatatable(List<TipoTelefone> input)
        {
            DataTable data = new DataTable();
            data.Columns.Add(new DataColumn("ID", typeof(int)));
            data.Columns.Add(new DataColumn("Name", typeof(string)));

            DataRow dr = data.NewRow();
            dr[0] = 0;
            dr[1] = "Selecione um Tipo de Telefone";
            data.Rows.Add(dr);

            int a = input.Count;
            for (int i = 0; i < a; i++)
            {
                TipoTelefone tipoTelefone = input.ElementAt(i);
                dr = data.NewRow();
                dr[0] = tipoTelefone.ID;
                dr[1] = tipoTelefone.Nome;
                data.Rows.Add(dr);
            }
            return data;
        }

        public static DataTable TipoResidenciaDatatable(List<TipoResidencia> input)
        {
            DataTable data = new DataTable();
            data.Columns.Add(new DataColumn("ID", typeof(int)));
            data.Columns.Add(new DataColumn("Name", typeof(string)));

            DataRow dr = data.NewRow();
            dr[0] = 0;
            dr[1] = "Selecione um Tipo de Residência";
            data.Rows.Add(dr);

            int a = input.Count;
            for (int i = 0; i < a; i++)
            {
                TipoResidencia tipoResidencia = input.ElementAt(i);
                dr = data.NewRow();
                dr[0] = tipoResidencia.ID;
                dr[1] = tipoResidencia.Nome;
                data.Rows.Add(dr);
            }
            return data;
        }

        public static DataTable TipoLogradouroDatatable(List<TipoLogradouro> input)
        {
            DataTable data = new DataTable();
            data.Columns.Add(new DataColumn("ID", typeof(int)));
            data.Columns.Add(new DataColumn("Name", typeof(string)));

            DataRow dr = data.NewRow();
            dr[0] = 0;
            dr[1] = "Selecione um Tipo de Logradouro";
            data.Rows.Add(dr);

            int a = input.Count;
            for (int i = 0; i < a; i++)
            {
                TipoLogradouro tipoLogradouro = input.ElementAt(i);
                dr = data.NewRow();
                dr[0] = tipoLogradouro.ID;
                dr[1] = tipoLogradouro.Nome;
                data.Rows.Add(dr);
            }
            return data;
        }

        public static DataTable PaisDatatable(List<Pais> input)
        {
            DataTable data = new DataTable();
            data.Columns.Add(new DataColumn("ID", typeof(int)));
            data.Columns.Add(new DataColumn("Name", typeof(string)));

            DataRow dr = data.NewRow();
            dr[0] = 0;
            dr[1] = "Selecione um Pais";
            data.Rows.Add(dr);

            int a = input.Count;
            for (int i = 0; i < a; i++)
            {
                Pais pais = input.ElementAt(i);
                dr = data.NewRow();
                dr[0] = pais.ID;
                dr[1] = pais.Nome;
                data.Rows.Add(dr);
            }
            return data;
        }
        public static DataTable EstadoDatatable(List<Estado> input)
        {
            DataTable data = new DataTable();
            data.Columns.Add(new DataColumn("ID", typeof(int)));
            data.Columns.Add(new DataColumn("Name", typeof(string)));

            DataRow dr = data.NewRow();
            dr[0] = 0;
            dr[1] = "Selecione um Estado";
            data.Rows.Add(dr);

            int a = input.Count;
            for (int i = 0; i < a; i++)
            {
                Estado estado = input.ElementAt(i);
                dr = data.NewRow();
                dr[0] = estado.ID;
                dr[1] = estado.Nome;
                data.Rows.Add(dr);
            }
            return data;
        }

        public static DataTable CidadeDatatable(List<Cidade> input)
        {
            DataTable data = new DataTable();
            data.Columns.Add(new DataColumn("ID", typeof(int)));
            data.Columns.Add(new DataColumn("Name", typeof(string)));

            DataRow dr = data.NewRow();
            dr[0] = 0;
            dr[1] = "Selecione uma Cidade";
            data.Rows.Add(dr);

            int a = input.Count;
            for (int i = 0; i < a; i++)
            {
                Cidade cidade = input.ElementAt(i);
                dr = data.NewRow();
                dr[0] = cidade.ID;
                dr[1] = cidade.Nome;
                data.Rows.Add(dr);
            }
            return data;
        }

        public static DataTable BandeiraDatatable(List<Bandeira> input)
        {
            DataTable data = new DataTable();
            data.Columns.Add(new DataColumn("ID", typeof(int)));
            data.Columns.Add(new DataColumn("Name", typeof(string)));

            DataRow dr = data.NewRow();
            dr[0] = 0;
            dr[1] = "Selecione uma Bandeira";
            data.Rows.Add(dr);

            int a = input.Count;
            for (int i = 0; i < a; i++)
            {
                Bandeira bandeira = input.ElementAt(i);
                dr = data.NewRow();
                dr[0] = bandeira.ID;
                dr[1] = bandeira.Nome;
                data.Rows.Add(dr);
            }
            return data;
        }

        protected void dropIdEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["estado_sel"] = dropIdEstado.SelectedIndex;

            if (!dropIdEstado.SelectedValue.Equals("0"))
            {
                Session["cidade_sel"] = dropIdCidade.SelectedIndex;
                dropIdCidade.DataSource = CidadeDatatable(commands["CONSULTAR"].execute(new Cidade() { Estado = new Estado() { ID = int.Parse(dropIdEstado.SelectedValue) } }).Entidades.Cast<Cidade>().ToList());
                dropIdCidade.DataBind();
                dropIdCidade.Enabled = true;
            }
            else
            {
                dropIdCidade.DataSource = CidadeDatatable(commands["CONSULTAR"].execute(new Cidade()).Entidades.Cast<Cidade>().ToList());
                dropIdCidade.DataValueField = "ID";
                dropIdCidade.DataTextField = "Name";
                dropIdCidade.DataBind();
                dropIdCidade.Enabled = false;
            }
        }

        protected void dropIdPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["pais_sel"] = dropIdPais.SelectedIndex;

            if (!dropIdPais.SelectedValue.Equals("0"))
            {
                Session["estado_sel"] = dropIdEstado.SelectedIndex;
                dropIdCidade.DataSource = CidadeDatatable(commands["CONSULTAR"].execute(new Cidade() { Estado = new Estado() { ID = int.Parse(dropIdEstado.SelectedValue), Pais = new Pais() { ID = int.Parse(dropIdPais.SelectedValue) } } }).Entidades.Cast<Cidade>().ToList());
                dropIdEstado.DataBind();
                dropIdEstado.Enabled = true;
            }
            else
            {
                dropIdEstado.DataSource = EstadoDatatable(commands["CONSULTAR"].execute(new Estado()).Entidades.Cast<Estado>().ToList());
                dropIdEstado.DataValueField = "ID";
                dropIdEstado.DataTextField = "Name";
                dropIdEstado.DataBind();
                dropIdEstado.Enabled = false;

                dropIdCidade.DataSource = CidadeDatatable(commands["CONSULTAR"].execute(new Cidade()).Entidades.Cast<Cidade>().ToList());
                dropIdCidade.DataValueField = "ID";
                dropIdCidade.DataTextField = "Name";
                dropIdCidade.DataBind();
                dropIdCidade.Enabled = false;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("./ListaCliente.aspx");
        }

        protected void btnResetar_Click(object sender, EventArgs e)
        {
            // -------------------- DADOS PESSOAIS COMEÇO ------------------------------------------------
            txtNome.Text = "";
            txtSobrenome.Text = "";
            txtCPF.Text = "";
            txtDtNascimento.Text = "";

            dropIdGenero.DataSource = GeneroDatatable();
            dropIdGenero.DataValueField = "ID";
            dropIdGenero.DataTextField = "Name";
            dropIdGenero.DataBind();
            // -------------------- DADOS PESSOAIS FIM ------------------------------------------------

            // -------------------- DADOS CONTATO COMEÇO ------------------------------------------------
            txtEmail.Text = "";

            dropIdTipoTelefone.DataSource = TipoTelefoneDatatable(commands["CONSULTAR"].execute(new TipoTelefone()).Entidades.Cast<TipoTelefone>().ToList());
            dropIdTipoTelefone.DataValueField = "ID";
            dropIdTipoTelefone.DataTextField = "Name";
            dropIdTipoTelefone.DataBind();

            txtDDD.Text = "";
            txtTelefone.Text = "";
            // -------------------- DADOS CONTATO FIM ------------------------------------------------

            // -------------------- ENDEREÇO COMEÇO ------------------------------------------------
            txtNomeEndereco.Text = "";

            dropIdTipoResidencia.DataSource = TipoResidenciaDatatable(commands["CONSULTAR"].execute(new TipoResidencia()).Entidades.Cast<TipoResidencia>().ToList());
            dropIdTipoResidencia.DataValueField = "ID";
            dropIdTipoResidencia.DataTextField = "Name";
            dropIdTipoResidencia.DataBind();

            dropIdTipoLogradouro.DataSource = TipoLogradouroDatatable(commands["CONSULTAR"].execute(new TipoLogradouro()).Entidades.Cast<TipoLogradouro>().ToList());
            dropIdTipoLogradouro.DataValueField = "ID";
            dropIdTipoLogradouro.DataTextField = "Name";
            dropIdTipoLogradouro.DataBind();

            txtRua.Text = "";
            txtNumero.Text = "";
            txtBairro.Text = "";
            txtCEP.Text = "";

            dropIdPais.DataSource = PaisDatatable(commands["CONSULTAR"].execute(new Pais()).Entidades.Cast<Pais>().ToList());
            dropIdPais.DataValueField = "ID";
            dropIdPais.DataTextField = "Name";
            dropIdPais.DataBind();

            dropIdEstado.DataSource = EstadoDatatable(commands["CONSULTAR"].execute(new Estado()).Entidades.Cast<Estado>().ToList());
            dropIdEstado.DataValueField = "ID";
            dropIdEstado.DataTextField = "Name";
            dropIdEstado.DataBind();

            dropIdCidade.DataSource = CidadeDatatable(commands["CONSULTAR"].execute(new Cidade()).Entidades.Cast<Cidade>().ToList());
            dropIdCidade.DataValueField = "ID";
            dropIdCidade.DataTextField = "Name";
            dropIdCidade.DataBind();

            txtObservacao.Text = "";
            // -------------------- ENDEREÇO FIM ------------------------------------------------

            // cadastro de cartão será em outra tela
            // -------------------- CARTÃO COMEÇO ------------------------------------------------
            //txtNomeImpresso.Text = "";
            //txtNumeroCC.Text = "";

            //dropIdBandeira.DataSource = BandeiraDatatable(commands["CONSULTAR"].execute(new Bandeira()).Entidades.Cast<Bandeira>().ToList());
            //dropIdBandeira.DataValueField = "ID";
            //dropIdBandeira.DataTextField = "Name";
            //dropIdBandeira.DataBind();

            //txtCodigoSeguranca.Text = "";
            // -------------------- CARTÃO FIM ------------------------------------------------
        }

        protected void btnCadastrar_Click(object sender, EventArgs e)
        {
            /*
             * depois instanciar a classe que cliente terá como array e fazer as linhas abaixo para cada campo da classe
             * no caso Endereço
             */
            //string[] Nomes = this.txtNomeCliente.Text.Trim().Split(',');
            //string[] Cpfs = this.txtCpf.Text.Trim().Split(',');

            // -------------------------- DADOS PESSOAIS COMEÇO --------------------------------------
            cliente.Nome = txtNome.Text + " " + txtSobrenome.Text;
            cliente.CPF = txtCPF.Text;

            if (!txtDtNascimento.Text.Equals(""))
            {
                string[] data = txtDtNascimento.Text.Split('-');
                cliente.DataNascimento = new DateTime(Convert.ToInt32(data[0].ToString()), Convert.ToInt32(data[1].ToString()), Convert.ToInt32(data[2].ToString()));
            }

            cliente.Genero = (dropIdGenero.SelectedValue.First());
            // -------------------------- DADOS PESSOAIS FIM --------------------------------------

            // -------------------------- DADOS CONTATO COMEÇO --------------------------------------
            cliente.Email = txtEmail.Text;

            cliente.Telefone.TipoTelefone.ID = Convert.ToInt32(dropIdTipoTelefone.SelectedValue);
            cliente.Telefone.DDD = txtDDD.Text;
            cliente.Telefone.NumeroTelefone = txtTelefone.Text;
            // -------------------------- DADOS CONTATO FIM --------------------------------------

            // -------------------------- ENDEREÇO COMEÇO --------------------------------------

            // clonagem ocorrendo corretamente, mas na hora da atribuição dando erro, 
            // por isso será tirado e outra abordagem será feita

            //string[] nomesEnderecos = txtNomeEndereco.Text.Trim().Split(',');
            //string[] nomesDestinatarios = txtNomeDestinatario.Text.Trim().Split(',');
            //string[] ruas = txtRua.Text.Trim().Split(',');
            //string[] numeros = txtNumero.Text.Trim().Split(',');
            //string[] bairros = txtBairro.Text.Trim().Split(',');
            //string[] ceps = txtCEP.Text.Trim().Split(',');
            //string[] observacoes = txtObservacao.Text.Trim().Split(',');

            //List<string> idsTipoResidencia = new List<string>();
            //List<string> idsTipoLogradouro = new List<string>();
            //List<string> idsPais = new List<string>();
            //List<string> idsEstado = new List<string>();
            //List<string> idsCidade = new List<string>();

            //// pega o elemento da primeira
            //idsTipoResidencia.Add(dropIdTipoResidencia.SelectedValue);
            //idsTipoLogradouro.Add(dropIdTipoLogradouro.SelectedValue);
            //idsPais.Add(dropIdPais.SelectedValue);
            //idsEstado.Add(dropIdEstado.SelectedValue);
            //idsCidade.Add(dropIdCidade.SelectedValue);

            //for (int i = 0; i < nomesEnderecos.Length - 1; i++)
            //{
            //    int n = (1 + 5 * i);
            //    idsTipoResidencia.Add(Request.Form["dropIdTipoResidencia_" + n]);
            //    idsTipoLogradouro.Add(Request.Form["dropIdTipoLogradouro_" + n]);
            //    idsPais.Add(Request.Form["dropIdPais_" + n]);
            //    idsEstado.Add(Request.Form["dropIdEstado_" + n]);
            //    idsCidade.Add(Request.Form["dropIdCidade_" + n]);
            //}

            //List<Endereco> enderecos = new List<Endereco>();
            //for (int i = 0; i < nomesEnderecos.Length; i++)
            //{
            //    Endereco endereco = new Endereco();

            //    endereco.Nome = nomesEnderecos[i];
            //    endereco.Destinatario = nomesDestinatarios[i];
            //    endereco.Rua = ruas[i];
            //    endereco.Numero = numeros[i];
            //    endereco.Bairro = bairros[i];
            //    endereco.CEP = ceps[i];
            //    endereco.Observacao = observacoes[i];

            //    endereco.TipoResidencia.ID = Convert.ToInt32(idsTipoResidencia[i]);
            //    endereco.TipoLogradouro.ID = Convert.ToInt32(idsTipoLogradouro[i]);
            //    endereco.Cidade.ID = Convert.ToInt32(idsCidade[i]);
            //    endereco.Cidade.Estado.ID = Convert.ToInt32(idsEstado[i]);
            //    endereco.Cidade.Estado.Pais.ID = Convert.ToInt32(idsPais[i]);

            //    enderecos.Add(endereco);
            //}

            //cliente.Enderecos = enderecos;

            string nomesEndereco = txtNomeEndereco.Text.Trim();
            string nomesDestinatario = txtNomeDestinatario.Text.Trim();
            string rua = txtRua.Text.Trim();
            string numero = txtNumero.Text.Trim();
            string bairro = txtBairro.Text.Trim();
            string cep = txtCEP.Text.Trim();
            string observacao = txtObservacao.Text.Trim();

            Endereco endereco = new Endereco();

            endereco.Nome = nomesEndereco;
            endereco.Destinatario = nomesDestinatario;
            endereco.Rua = rua;
            endereco.Numero = numero;
            endereco.Bairro = bairro;
            endereco.CEP = cep;
            endereco.Observacao = observacao;

            endereco.TipoResidencia.ID = Convert.ToInt32(dropIdTipoResidencia.SelectedValue);
            endereco.TipoLogradouro.ID = Convert.ToInt32(dropIdTipoLogradouro.SelectedValue);
            endereco.Cidade.ID = Convert.ToInt32(dropIdCidade.SelectedValue);
            endereco.Cidade.Estado.ID = Convert.ToInt32(dropIdEstado.SelectedValue);
            endereco.Cidade.Estado.Pais.ID = Convert.ToInt32(dropIdPais.SelectedValue);

            cliente.Enderecos.Add(endereco);
            // -------------------------- ENDEREÇO FIM --------------------------------------

            // cadastro de cartão será em outra tela
            // -------------------------- CARTÃO COMEÇO --------------------------------------
            //string[] nomesImpressos = txtNomeImpresso.Text.Trim().Split(',');
            //string[] numerosCC = txtNumeroCC.Text.Trim().Split(',');
            ////string[] idsBandeira = dropIdBandeira.Text.Trim().Split(','); 
            //string[] codigosSeguranca = txtCodigoSeguranca.Text.Trim().Split(',');

            //List<string> idsBandeira = new List<string>();

            //idsBandeira.Add(dropIdBandeira.SelectedValue);

            //for (int i = 0; i < nomesImpressos.Length - 1; i++)
            //{
            //    int n = (1 + i);
            //    idsBandeira.Add(Request.Form["dropIdBandeira_" + n]);
            //}

            //List<CartaoCredito> cartoes = new List<CartaoCredito>();

            //for (int i = 0; i < nomesImpressos.Length; i++)
            //{
            //    CartaoCredito cartao = new CartaoCredito();

            //    cartao.NomeImpresso = nomesImpressos[i];
            //    cartao.NumeroCC = numerosCC[i];
            //    cartao.CodigoSeguranca = codigosSeguranca[i];
            //    cartao.Bandeira.ID = Convert.ToInt32(idsBandeira[i]);

            //    cartoes.Add(cartao);
            //}

            //cliente.CartoesCredito = cartoes;
            // -------------------------- CARTÃO FIM --------------------------------------

            //Response.Redirect("./ListaCliente.aspx");

            resultado = commands["SALVAR"].execute(cliente);
            if (!string.IsNullOrEmpty(resultado.Msg))
            {
                lblResultado.Visible = true;
                lblResultado.Text = resultado.Msg;
            }
            else
            {
                Response.Redirect("./ListaCliente.aspx");
            }
        }

        protected void btnAlterar_Click(object sender, EventArgs e)
        {
            cliente.ID = Convert.ToInt32(txtIdClientePF.Text);

            // -------------------------- DADOS PESSOAIS COMEÇO --------------------------------------
            cliente.Nome = txtNome.Text + " " + txtSobrenome.Text;
            cliente.CPF = txtCPF.Text;

            if (!txtDtNascimento.Text.Equals(""))
            {
                string[] data = txtDtNascimento.Text.Split('-');
                cliente.DataNascimento = new DateTime(Convert.ToInt32(data[0].ToString()), Convert.ToInt32(data[1].ToString()), Convert.ToInt32(data[2].ToString()));
            }

            cliente.Genero = (dropIdGenero.SelectedValue.First());
            // -------------------------- DADOS PESSOAIS FIM --------------------------------------

            // -------------------------- DADOS CONTATO COMEÇO --------------------------------------
            cliente.Email = txtEmail.Text;

            cliente.Telefone.ID = Convert.ToInt32(txtIdTelefone.Text);
            cliente.Telefone.TipoTelefone.ID = Convert.ToInt32(dropIdTipoTelefone.SelectedValue);
            cliente.Telefone.DDD = txtDDD.Text;
            cliente.Telefone.NumeroTelefone = txtTelefone.Text;
            // -------------------------- DADOS CONTATO FIM --------------------------------------

            // -------------------------- ENDEREÇO COMEÇO --------------------------------------

            string nomesEndereco = txtNomeEndereco.Text.Trim();
            string nomesDestinatario = txtNomeDestinatario.Text.Trim();
            string rua = txtRua.Text.Trim();
            string numero = txtNumero.Text.Trim();
            string bairro = txtBairro.Text.Trim();
            string cep = txtCEP.Text.Trim();
            string observacao = txtObservacao.Text.Trim();

            Endereco endereco = new Endereco();

            endereco.ID = Convert.ToInt32(txtIdEndereco.Text);
            endereco.Nome = nomesEndereco;
            endereco.Destinatario = nomesDestinatario;
            endereco.Rua = rua;
            endereco.Numero = numero;
            endereco.Bairro = bairro;
            endereco.CEP = cep;
            endereco.Observacao = observacao;

            endereco.TipoResidencia.ID = Convert.ToInt32(dropIdTipoResidencia.SelectedValue);
            endereco.TipoLogradouro.ID = Convert.ToInt32(dropIdTipoLogradouro.SelectedValue);
            endereco.Cidade.ID = Convert.ToInt32(dropIdCidade.SelectedValue);
            endereco.Cidade.Estado.ID = Convert.ToInt32(dropIdEstado.SelectedValue);
            endereco.Cidade.Estado.Pais.ID = Convert.ToInt32(dropIdPais.SelectedValue);

            cliente.Enderecos.Add(endereco);
            // -------------------------- ENDEREÇO FIM --------------------------------------

            resultado = commands["ALTERAR"].execute(cliente);
            if (!string.IsNullOrEmpty(resultado.Msg))
            {
                lblResultado.Visible = true;
                lblResultado.Text = resultado.Msg;
            }
            else
            {
                Response.Redirect("./ListaCliente.aspx");
            }
        }

    }
}