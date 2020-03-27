using Core.Aplicacao;
using Dominio;
using Dominio.Livro;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EComLivro.Adm
{
    public partial class AtivacaoLivro : ViewGenerico
    {
        Livro livro = new Livro();
        private Resultado resultado = new Resultado();
        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dropIdCategoriaMotivo.DataSource = CategoriaMotivoDatatable(commands["CONSULTAR"].execute(new CategoriaMotivo()).Entidades.Cast<CategoriaMotivo>().ToList());
                dropIdCategoriaMotivo.DataValueField = "ID";
                dropIdCategoriaMotivo.DataTextField = "Name";
                dropIdCategoriaMotivo.DataBind();

                if (!string.IsNullOrEmpty(Request.QueryString["idLivro"]))
                {
                    livro.ID = Convert.ToInt32(Request.QueryString["idLivro"]);
                    txtIdLivro.Text = livro.ID.ToString();
                    txtIdLivro.Enabled = false;
                    
                    entidades = commands["CONSULTAR"].execute(livro).Entidades;
                    livro = (Livro)entidades.ElementAt(0);

                    if(livro.CategoriaMotivo.Ativo != 'A')
                    {
                        idBreadCrumb.InnerText = "Ativação do Livro";
                        idTitle.InnerText = "Ativação do Livro";
                        idSubTitle.InnerText = "Dados de Ativação do Livro";
                        btnAtivarInativar.Text = "Ativar";
                        txtMotivo.Attributes.Add("placeholder", "Informe o Motivo da Ativação do Livro");
                    } else
                    {
                        idBreadCrumb.InnerText = "Inativação do Livro";
                        idTitle.InnerText = "Inativação do Livro";
                        idSubTitle.InnerText = "Dados de Inativação do Livro";
                        btnAtivarInativar.Text = "Inativar";
                        txtMotivo.Attributes.Add("placeholder", "Informe o Motivo da Inativação do Livro");
                    }

                    // ------------------------ Dados Categoria Motivo - COMEÇO ------------------------------
                    //dropIdCategoriaMotivo.SelectedValue = livro.CategoriaMotivo.ID.ToString();

                    if (livro.CategoriaMotivo.Ativo != 'A')
                    {
                        dropIdCategoriaMotivo.DataSource = CategoriaMotivoDatatable(commands["CONSULTAR"].execute(new CategoriaMotivo() { Ativo = 'A' }).Entidades.Cast<CategoriaMotivo>().ToList());
                    } else
                    {
                        dropIdCategoriaMotivo.DataSource = CategoriaMotivoDatatable(commands["CONSULTAR"].execute(new CategoriaMotivo() { Ativo = 'I' }).Entidades.Cast<CategoriaMotivo>().ToList());
                    }
                    dropIdCategoriaMotivo.DataValueField = "ID";
                    dropIdCategoriaMotivo.DataTextField = "Name";
                    dropIdCategoriaMotivo.DataBind();

                    txtMotivo.Text = "";
                    // ------------------------ Dados Categoria Motivo - FIM ------------------------------

                }
            }
        }

        public static DataTable CategoriaMotivoDatatable(List<CategoriaMotivo> input)
        {
            DataTable data = new DataTable();
            data.Columns.Add(new DataColumn("ID", typeof(int)));
            data.Columns.Add(new DataColumn("Name", typeof(string)));

            DataRow dr = data.NewRow();
            dr[0] = 0;
            if (input.ElementAt(0).Ativo == 'A')
            {
                dr[1] = "Selecione uma Categoria de Motivo de Ativação";
            }
            else
            {
                dr[1] = "Selecione uma Categoria de Motivo de Inativação";
            }
            data.Rows.Add(dr);

            int a = input.Count;
            for (int i = 0; i < a; i++)
            {
                CategoriaMotivo categoria = input.ElementAt(i);
                dr = data.NewRow();
                dr[0] = categoria.ID;
                dr[1] = categoria.Ativo + " - " + categoria.Nome;
                data.Rows.Add(dr);
            }
            return data;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("./ListaLivro.aspx");
        }

        protected void btnResetar_Click(object sender, EventArgs e)
        {
            // -------------------- ATIVAÇÃO/INATIVAÇÃO COMEÇO ------------------------------------------------
            dropIdCategoriaMotivo.DataSource = CategoriaMotivoDatatable(commands["CONSULTAR"].execute(new CategoriaMotivo()).Entidades.Cast<CategoriaMotivo>().ToList());
            dropIdCategoriaMotivo.DataValueField = "ID";
            dropIdCategoriaMotivo.DataTextField = "Name";
            dropIdCategoriaMotivo.DataBind();

            txtMotivo.Text = "";
            // -------------------- ATIVAÇÃO/INATIVAÇÃO FIM ------------------------------------------------
        }

        protected void btnAtivarInativar_Click(object sender, EventArgs e)
        {
            livro.ID = Convert.ToInt32(txtIdLivro.Text);

            // -------------------------- ATIVAÇÃO/INATIVAÇÃO COMEÇO --------------------------------------
            livro.CategoriaMotivo.ID = Convert.ToInt32(dropIdCategoriaMotivo.SelectedValue);
            livro.Motivo = txtMotivo.Text.Trim();
            // -------------------------- ATIVAÇÃO/INATIVAÇÃO FIM --------------------------------------

            resultado = commands["ALTERAR"].execute(livro);
            if (!string.IsNullOrEmpty(resultado.Msg))
            {
                lblResultado.Visible = true;
                lblResultado.Text = resultado.Msg;
            }
            else
            {
                Response.Redirect("./ListaLivro.aspx");
            }
        }

    }
}