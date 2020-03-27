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

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["idLivro"]))
                {
                    livro.ID = Convert.ToInt32(Request.QueryString["idLivro"]);
                    txtIdLivro.Text = livro.ID.ToString();

                    entidades = commands["CONSULTAR"].execute(livro).Entidades;
                    livro = (Livro)entidades.ElementAt(0);

                    txtBreadCrumb.InnerText = livro.Titulo;
                    txtTituloLivro.InnerText = livro.Titulo;
                    txtPrecoLivro.InnerText = "R$ " + livro.GrupoPrecificacao.MargemLucro.ToString("N2");
                    txtDescricao.InnerText = livro.Sinopse;
                }
            }
        }
    }
}