using Core.Aplicacao;
using Dominio.Livro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EComLivro.Loja
{
    public partial class ProductDetails : ViewGenerico
    {
        Livro livro = new Livro();
        Estoque estoque = new Estoque();

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["idLivro"]))
                {
                    livro.ID = Convert.ToInt32(Request.QueryString["idLivro"]);
                    txtIdLivro.Text = livro.ID.ToString();
                    estoque.Livro.ID = livro.ID;

                    entidades = commands["CONSULTAR"].execute(livro).Entidades;
                    livro = (Livro)entidades.ElementAt(0);

                    entidades = commands["CONSULTAR"].execute(estoque).Entidades;
                    estoque = (Estoque)entidades.ElementAt(0);

                    txtBreadCrumb.InnerText = livro.Titulo;
                    txtTituloLivro.InnerText = livro.Titulo;
                    txtPrecoLivro.InnerText = "R$ " + estoque.ValorVenda.ToString("N2");
                    txtDescricao.InnerText = livro.Sinopse;

                    btnAddToCart.InnerHtml = string.Format(
                        "<a class='btn amado-btn' href='AddToCart.aspx?idLivro={0}' title='Adicionar ao Carrinho'>" +
                        "Adicionar ao Carrinho</a>",
                        livro.ID);
                }
            }
        }
    }
}