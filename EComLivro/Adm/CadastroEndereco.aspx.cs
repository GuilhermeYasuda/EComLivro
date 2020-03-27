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
    public partial class CadastroEndereco : ViewGenerico
    {
        ClientePFXEndereco clientePFXEndereco = new ClientePFXEndereco();
        Endereco endereco = new Endereco();
        private Resultado resultado = new Resultado();
        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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

                if (!string.IsNullOrEmpty(Request.QueryString["idClientePF"]))
                {
                    clientePFXEndereco.ID = Convert.ToInt32(Request.QueryString["idClientePF"]);
                    txtIdClientePF.Text = clientePFXEndereco.ID.ToString();
                    txtIdClientePF.Enabled = false;
                }
                if (!string.IsNullOrEmpty(Request.QueryString["idEndereco"]))
                {
                    btnCadastrar.Visible = false;
                    btnAlterar.Visible = true;
                    idLinhaCodigoClientePF.Visible = false;
                    txtIdClientePF.Visible = false;
                    idLinhaCodigo.Visible = true;
                    txtIdEndereco.Visible = true;
                    txtIdEndereco.Enabled = false;
                    idTitle.InnerText = "Alterar Endereço";
                    idBreadCrumb.InnerText = "Alterar Endereço";
                    endereco.ID = Convert.ToInt32(Request.QueryString["idEndereco"]);
                    entidades = commands["CONSULTAR"].execute(endereco).Entidades;
                    endereco = (Endereco)entidades.ElementAt(0);
                    txtIdEndereco.Text = endereco.ID.ToString();

                    // ------------------------ Dados Endereço - COMEÇO ------------------------------
                    txtIdEndereco.Text = endereco.ID.ToString();

                    txtNomeEndereco.Text = endereco.Nome;
                    txtNomeDestinatario.Text = endereco.Destinatario;
                    dropIdTipoResidencia.SelectedValue = endereco.TipoResidencia.ID.ToString();
                    dropIdTipoLogradouro.SelectedValue = endereco.TipoLogradouro.ID.ToString();
                    txtRua.Text = endereco.Rua;
                    txtNumero.Text = endereco.Numero;
                    txtBairro.Text = endereco.Bairro;
                    txtCEP.Text = endereco.CEP;
                    txtObservacao.Text = endereco.Observacao;
                    dropIdPais.SelectedValue = endereco.Cidade.Estado.Pais.ID.ToString();
                    dropIdEstado.SelectedValue = endereco.Cidade.Estado.ID.ToString();
                    dropIdEstado.Enabled = true;
                    dropIdCidade.SelectedValue = endereco.Cidade.ID.ToString();
                    dropIdCidade.Enabled = true;
                    // ------------------------ Dados Endereço - FIM ------------------------------
                }
                else if (!string.IsNullOrEmpty(Request.QueryString["delIdEndereco"]))
                {
                    endereco.ID = Convert.ToInt32(Request.QueryString["delIdEndereco"]);
                    resultado = commands["EXCLUIR"].execute(endereco);

                    // verifica se deu erro de validação
                    if (!string.IsNullOrEmpty(resultado.Msg))
                    {
                        lblResultado.Visible = true;
                        lblResultado.Text = resultado.Msg;
                    }
                    // caso tudo OK delera e redireciona o usuário para ListaCliente.aspx
                    else
                    {
                        Response.Redirect("./ListaCliente.aspx");
                    }
                }

            }
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
        }

        protected void btnCadastrar_Click(object sender, EventArgs e)
        {
            clientePFXEndereco.ID = Convert.ToInt32(txtIdClientePF.Text);

            // -------------------------- ENDEREÇO COMEÇO --------------------------------------
            clientePFXEndereco.Endereco.Nome = txtNomeEndereco.Text.Trim();
            clientePFXEndereco.Endereco.Destinatario = txtNomeDestinatario.Text.Trim();
            clientePFXEndereco.Endereco.Rua = txtRua.Text.Trim();
            clientePFXEndereco.Endereco.Numero = txtNumero.Text.Trim();
            clientePFXEndereco.Endereco.Bairro = txtBairro.Text.Trim();
            clientePFXEndereco.Endereco.CEP = txtCEP.Text.Trim();
            clientePFXEndereco.Endereco.Observacao = txtObservacao.Text.Trim();

            clientePFXEndereco.Endereco.TipoResidencia.ID = Convert.ToInt32(dropIdTipoResidencia.SelectedValue);
            clientePFXEndereco.Endereco.TipoLogradouro.ID = Convert.ToInt32(dropIdTipoLogradouro.SelectedValue);
            clientePFXEndereco.Endereco.Cidade.ID = Convert.ToInt32(dropIdCidade.SelectedValue);
            clientePFXEndereco.Endereco.Cidade.Estado.ID = Convert.ToInt32(dropIdEstado.SelectedValue);
            clientePFXEndereco.Endereco.Cidade.Estado.Pais.ID = Convert.ToInt32(dropIdPais.SelectedValue);
            // -------------------------- ENDEREÇO FIM --------------------------------------

            resultado = commands["SALVAR"].execute(clientePFXEndereco);
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
            endereco.ID = Convert.ToInt32(txtIdEndereco.Text);

            // -------------------------- ENDEREÇO COMEÇO --------------------------------------
            endereco.Nome = txtNomeEndereco.Text.Trim();
            endereco.Destinatario = txtNomeDestinatario.Text.Trim();
            endereco.Rua = txtRua.Text.Trim();
            endereco.Numero = txtNumero.Text.Trim();
            endereco.Bairro = txtBairro.Text.Trim();
            endereco.CEP = txtCEP.Text.Trim();
            endereco.Observacao = txtObservacao.Text.Trim();

            endereco.TipoResidencia.ID = Convert.ToInt32(dropIdTipoResidencia.SelectedValue);
            endereco.TipoLogradouro.ID = Convert.ToInt32(dropIdTipoLogradouro.SelectedValue);
            endereco.Cidade.ID = Convert.ToInt32(dropIdCidade.SelectedValue);
            endereco.Cidade.Estado.ID = Convert.ToInt32(dropIdEstado.SelectedValue);
            endereco.Cidade.Estado.Pais.ID = Convert.ToInt32(dropIdPais.SelectedValue);
            // -------------------------- ENDEREÇO FIM --------------------------------------

            resultado = commands["ALTERAR"].execute(endereco);
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