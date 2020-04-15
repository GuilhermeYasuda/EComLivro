using Core.Aplicacao;
using Dominio.Cliente;
using Dominio.Venda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EComLivro.Adm
{
    public partial class GeraCupomTroca : ViewGenerico
    {
        Pedido pedido = new Pedido();
        Resultado resultado = new Resultado();

        protected override void Page_Load(object sender, EventArgs e)
        {
            pedido = commands["CONSULTAR"].execute(new Pedido() { ID = Convert.ToInt32(Session["idPedido"]) }).Entidades.Cast<Pedido>().ElementAt(0);

            ClientePF cliente = commands["CONSULTAR"].execute(new ClientePF() { Email = pedido.Usuario }).Entidades.Cast<ClientePF>().ElementAt(0);

            Cupom cupom = new Cupom();
            cupom.IdCliente = cliente.ID;
            cupom.Status = 'A';
            cupom.Tipo.ID = 1;
            cupom.ValorCupom = pedido.Total;
            cupom.CodigoCupom = "CUPOMTROCA" + pedido.ID + cupom.IdCliente + cupom.ValorCupom.ToString().Replace(",", "");

            resultado = commands["SALVAR"].execute(cupom);

            Response.Redirect("./ListaPedidos.aspx");

        }
    }
}