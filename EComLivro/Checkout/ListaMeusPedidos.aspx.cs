using Dominio.Venda;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EComLivro.Checkout
{
    public partial class ListaMeusPedidos : ViewGenerico
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Pedido pedido = new Pedido();

                pedido.Usuario = Context.User.Identity.GetUserName();

                //pedido = commands["CONSULTAR"].execute(pedido).Entidades.Cast<Pedido>().ElementAt(0);

                List<Pedido> listaPedido = commands["CONSULTAR"].execute(pedido).Entidades.Cast<Pedido>().ToList();

                //pedido.EnderecoEntrega = commands["CONSULTAR"].execute(new Endereco() { ID = pedido.EnderecoEntrega.ID } ).Entidades.Cast<Endereco>().ElementAt(0);

                //pedido.Detalhes = commands["CONSULTAR"].execute(new PedidoDetalhe() { IdPedido = pedido.ID }).Entidades.Cast<PedidoDetalhe>().ToList();

                //pedido.CCs = commands["CONSULTAR"].execute(new CartaoCreditoPedido() { IdPedido = pedido.ID }).Entidades.Cast<CartaoCreditoPedido>().ToList();

                //entidades = new List<EntidadeDominio>();
                //entidades = commands["CONSULTAR"].execute(new PedidoXCupom() { ID = pedido.ID }).Entidades;
                //if(entidades.Count > 0) { 
                //    pedido.CupomPromocional = commands["CONSULTAR"].execute(new PedidoXCupom() { ID = pedido.ID }).Entidades.Cast<PedidoXCupom>().ElementAt(0).Cupom;
                //}


                //entidades = new List<EntidadeDominio>();
                //entidades = commands["CONSULTAR"].execute(new Cupom() { IdPedido = pedido.ID }).Entidades;
                //if (entidades.Count > 0)
                //{
                //    pedido.CuponsTroca = commands["CONSULTAR"].execute(new Cupom() { IdPedido = pedido.ID }).Entidades.Cast<Cupom>().ToList();
                //}


                //// Set OrderId.
                //Session["currentOrderId"] = pedido.ID;

                // Exibi as informações do Pedido
                //listaPedido.Add(pedido);
                OrderList.DataSource = listaPedido;
                OrderList.DataBind();

                //// Mostra os detalhes do pedido
                //OrderItemList.DataSource = pedido.Detalhes;
                //OrderItemList.DataBind();

            }
            else
            {
                //Response.Redirect("./CheckoutError.aspx?Desc=ID%20de%20pedido%20incompatível.");
            }
        }

        protected void Confirm_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Account/Manage.aspx");
        }
    }
}