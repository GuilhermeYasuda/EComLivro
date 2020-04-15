using Core.Aplicacao;
using Core.DAO;
using Dominio;
using Dominio.Cliente;
using Dominio.Livro;
using Dominio.Venda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EComLivro.Checkout
{
    public partial class GerarPedidoTroca : ViewGenerico
    {
        private Resultado resultado = new Resultado();
        protected override void Page_Load(object sender, EventArgs e)
        {
            var pedido = new Pedido();
            if (!string.IsNullOrEmpty(Request.QueryString["idPedido"]))
            {
                pedido.ID = Convert.ToInt32(Request.QueryString["idPedido"]);
            }

            pedido = commands["CONSULTAR"].execute(pedido).Entidades.Cast<Pedido>().ElementAt(0);

            // setando valores para troca
            //pedido.Total -= pedido.Frete;   // deduz o frete para ter o valor líquido do pedido
            pedido.Frete = (float)0.01;     // frete não pode ser 0 devido a validação

            // pega itens do pedido todo
            pedido.Detalhes = commands["CONSULTAR"].execute(new PedidoDetalhe() { IdPedido = pedido.ID }).Entidades.Cast<PedidoDetalhe>().ToList();

            // verifica se vai ser trocado apenas um livro do pedido
            if (!string.IsNullOrEmpty(Request.QueryString["idLivro"]))
            {
                // passa o livro específico a ser trocado
                pedido.Detalhes = commands["CONSULTAR"].execute(new PedidoDetalhe() { IdPedido = pedido.ID, Livro = new Livro() { ID = Convert.ToInt32(Request.QueryString["idLivro"]) } }).Entidades.Cast<PedidoDetalhe>().ToList();
            }

            pedido.Total = 0;

            foreach (PedidoDetalhe detalhe in pedido.Detalhes)
            {
                pedido.Total += detalhe.ValorUnit * detalhe.Quantidade;
            }

            // seta status de "EM TROCA"
            pedido.Status.ID = 6;

            resultado = commands["SALVAR"].execute(pedido);

            Response.Redirect("./CheckoutReview.aspx?idPedido=" + pedido.ID);

        }
    }

}