using Core.Aplicacao;
using Dominio;
using Dominio.Livro;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EComLivro.Adm
{
    public partial class EstoqueLivro : ViewGenerico
    {
        Estoque estoque = new Estoque();
        private Resultado resultado = new Resultado();
        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dropIdFornecedor.DataSource = FornecedorDatatable(commands["CONSULTAR"].execute(new Fornecedor()).Entidades.Cast<Fornecedor>().ToList());
                dropIdFornecedor.DataValueField = "ID";
                dropIdFornecedor.DataTextField = "Name";
                dropIdFornecedor.DataBind();

                if (!string.IsNullOrEmpty(Request.QueryString["idLivro"]))
                {
                    estoque.Livro.ID = Convert.ToInt32(Request.QueryString["idLivro"]);
                    txtIdLivro.Text = estoque.Livro.ID.ToString();
                    txtIdLivro.Enabled = false;
                    
                    entidades = commands["CONSULTAR"].execute(estoque).Entidades;

                    if (entidades.Count > 0)
                    {
                        estoque = (Estoque)entidades.ElementAt(0);

                        dropIdFornecedor.SelectedValue = estoque.Fornecedor.ID.ToString();
                    }
                }
                else if (!string.IsNullOrEmpty(Request.QueryString["delIdLivro"]))
                {
                    estoque.ID = Convert.ToInt32(Request.QueryString["delIdLivro"]);
                    resultado = commands["EXCLUIR"].execute(estoque);

                    // verifica se deu erro de validação
                    if (!string.IsNullOrEmpty(resultado.Msg))
                    {
                        lblResultado.Visible = true;
                        lblResultado.Text = resultado.Msg;
                    }
                    // caso tudo OK delera e redireciona o usuário para ListaCliente.aspx
                    else
                    {
                        Response.Redirect("./ListaEstoque.aspx");
                    }
                }
            }
        }

        public static DataTable FornecedorDatatable(List<Fornecedor> input)
        {
            DataTable data = new DataTable();
            data.Columns.Add(new DataColumn("ID", typeof(int)));
            data.Columns.Add(new DataColumn("Name", typeof(string)));

            DataRow dr = data.NewRow();
            dr[0] = 0;
            dr[1] = "Selecione um Fornecedor";
            data.Rows.Add(dr);

            int a = input.Count;
            for (int i = 0; i < a; i++)
            {
                Fornecedor fornecedor = input.ElementAt(i);
                dr = data.NewRow();
                dr[0] = fornecedor.ID;
                dr[1] = fornecedor.Nome;
                data.Rows.Add(dr);
            }
            return data;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("./ListaEstoque.aspx");
        }

        protected void btnResetar_Click(object sender, EventArgs e)
        {
            txtCusto.Text = "";
            txtQtde.Text = "";

            dropIdFornecedor.DataSource = FornecedorDatatable(commands["CONSULTAR"].execute(new Fornecedor()).Entidades.Cast<Fornecedor>().ToList());
            dropIdFornecedor.DataValueField = "ID";
            dropIdFornecedor.DataTextField = "Name";
            dropIdFornecedor.DataBind();

        }

        protected void btnDarEntrada_Click(object sender, EventArgs e)
        {
            estoque.Livro.ID = Convert.ToInt32(txtIdLivro.Text);

            entidades = commands["CONSULTAR"].execute(estoque).Entidades;

            if(!string.IsNullOrEmpty(txtQtde.Text))
                estoque.Qtde = Convert.ToInt32(txtQtde.Text);

            if (!string.IsNullOrEmpty(txtCusto.Text))
                estoque.ValorCusto = float.Parse(txtCusto.Text, CultureInfo.InvariantCulture.NumberFormat);

            estoque.Fornecedor.ID = Convert.ToInt32(dropIdFornecedor.SelectedValue);

            if(entidades.Count > 0)
            {
                resultado = commands["ALTERAR"].execute(estoque);
            }
            else
            {
                resultado = commands["SALVAR"].execute(estoque);
            }

            if (!string.IsNullOrEmpty(resultado.Msg))
            {
                lblResultado.Visible = true;
                lblResultado.Text = resultado.Msg;
            }
            else
            {
                Response.Redirect("./ListaEstoque.aspx");
            }
        }

    }
}