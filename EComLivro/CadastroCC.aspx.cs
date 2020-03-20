using Core.Aplicacao;
using Dominio;
using Dominio.Cliente;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EComLivro
{
    public partial class CadastroCC : ViewGenerico
    {
        ClientePFXCC clientePFXCC = new ClientePFXCC();
        CartaoCredito cc = new CartaoCredito();
        private Resultado resultado = new Resultado();
        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dropIdBandeira.DataSource = BandeiraDatatable(commands["CONSULTAR"].execute(new Bandeira()).Entidades.Cast<Bandeira>().ToList());
                dropIdBandeira.DataValueField = "ID";
                dropIdBandeira.DataTextField = "Name";
                dropIdBandeira.DataBind();


                if (!string.IsNullOrEmpty(Request.QueryString["idClientePF"]))
                {
                    clientePFXCC.ID = Convert.ToInt32(Request.QueryString["idClientePF"]);
                    txtIdClientePF.Text = clientePFXCC.ID.ToString();
                    txtIdClientePF.Enabled = false;
                }
                else if (!string.IsNullOrEmpty(Request.QueryString["idCC"]))
                {
                    btnCadastrar.Visible = false;
                    btnAlterar.Visible = true;
                    idLinhaCodigoClientePF.Visible = false;
                    txtIdClientePF.Visible = false;
                    idLinhaCodigo.Visible = true;
                    txtIdCC.Visible = true;
                    txtIdCC.Enabled = false;
                    idTitle.InnerText = "Alterar Cartão de Crédito";
                    idBreadCrumb.InnerText = "Alterar Cartão de Crédito";
                    cc.ID = Convert.ToInt32(Request.QueryString["idCC"]);
                    entidades = commands["CONSULTAR"].execute(cc).Entidades;
                    cc = (CartaoCredito)entidades.ElementAt(0);
                    txtIdCC.Text = cc.ID.ToString();

                    // ------------------------ Dados CC - COMEÇO ------------------------------
                    txtNomeImpresso.Text = cc.NomeImpresso;
                    txtNumeroCC.Text = cc.NumeroCC;
                    txtCodigoSeguranca.Text = cc.CodigoSeguranca;
                    dropIdBandeira.SelectedValue = cc.Bandeira.ID.ToString();
                    // ------------------------ Dados CC - FIM ------------------------------

                }
                else if (!string.IsNullOrEmpty(Request.QueryString["delIdCC"]))
                {
                    cc.ID = Convert.ToInt32(Request.QueryString["delIdCC"]);
                    resultado = commands["EXCLUIR"].execute(cc);

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

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("./ListaCliente.aspx");
        }

        protected void btnResetar_Click(object sender, EventArgs e)
        {
            // -------------------- CARTÃO COMEÇO ------------------------------------------------
            txtNomeImpresso.Text = "";
            txtNumeroCC.Text = "";

            dropIdBandeira.DataSource = BandeiraDatatable(commands["CONSULTAR"].execute(new Bandeira()).Entidades.Cast<Bandeira>().ToList());
            dropIdBandeira.DataValueField = "ID";
            dropIdBandeira.DataTextField = "Name";
            dropIdBandeira.DataBind();

            txtCodigoSeguranca.Text = "";
            // -------------------- CARTÃO FIM ------------------------------------------------
        }

        protected void btnCadastrar_Click(object sender, EventArgs e)
        {
            clientePFXCC.ID = Convert.ToInt32(txtIdClientePF.Text);

            // -------------------------- CARTÃO COMEÇO --------------------------------------
            clientePFXCC.CC.NomeImpresso = txtNomeImpresso.Text.Trim();
            clientePFXCC.CC.NumeroCC = txtNumeroCC.Text.Trim();
            clientePFXCC.CC.CodigoSeguranca = txtCodigoSeguranca.Text.Trim();
            clientePFXCC.CC.Bandeira.ID = Convert.ToInt32(dropIdBandeira.SelectedValue);
            // -------------------------- CARTÃO FIM --------------------------------------

            resultado = commands["SALVAR"].execute(clientePFXCC);
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
            cc.ID = Convert.ToInt32(txtIdCC.Text);

            // -------------------------- CARTÃO COMEÇO --------------------------------------
            cc.NomeImpresso = txtNomeImpresso.Text.Trim();
            cc.NumeroCC = txtNumeroCC.Text.Trim();
            cc.CodigoSeguranca = txtCodigoSeguranca.Text.Trim();
            cc.Bandeira.ID = Convert.ToInt32(dropIdBandeira.SelectedValue);
            // -------------------------- CARTÃO FIM --------------------------------------

            resultado = commands["ALTERAR"].execute(cc);
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