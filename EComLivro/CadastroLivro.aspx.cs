using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EComLivro
{
    public partial class CadastroLivro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("./ListaLivro.aspx");
        }

        protected void btnResetar_Click(object sender, EventArgs e)
        {
            //dropIdAutor.SelectedIndex = 0;
            //dropIdAutor.DataSource = "";
            //dropIdAutor.DataBind();
            //dropIdAutor.Enabled = false;

            //dropIdCategoria.SelectedIndex = 0;
            //dropIdCategoria.DataSource = "";
            //dropIdCategoria.DataBind();
            //dropIdCategoria.Enabled = false;

            txtTitulo.Text = "";
            txtNumeroPaginas.Text = "";
            txtSinopse.Text = "";
            txtISBN.Text = "";
            txtCodigoBarras.Text = "";

            //dropIdEditora.SelectedIndex = 0;
            //dropIdEditora.DataSource = "";
            //dropIdEditora.DataBind();
            //dropIdEditora.Enabled = false;

            txtAno.Text = "";
            txtEdição.Text = "";

            txtAltura.Text = "";
            txtLargura.Text = "";
            txtProfundidade.Text = "";
            txtPeso.Text = "";

            //dropIdGrupoPrecificacao.SelectedIndex = 0;
            //dropIdGrupoPrecificacao.DataSource = "";
            //dropIdGrupoPrecificacao.DataBind();
            //dropIdGrupoPrecificacao.Enabled = false;

        }

        protected void btnCadastrar_Click(object sender, EventArgs e)
        {
            /*
             * depois instanciar a classe que cliente terá como array e fazer as linhas abaixo para cada campo da classe
             * no caso Endereço
             */
            //string[] Nomes = this.txtNomeCliente.Text.Trim().Split(',');
            //string[] Cpfs = this.txtCpf.Text.Trim().Split(',');

            Response.Redirect("./ListaLivro.aspx");
        }

        protected void btnAlterar_Click(object sender, EventArgs e)
        {
            Response.Redirect("./ListaLivro.aspx");
        }
    }
}