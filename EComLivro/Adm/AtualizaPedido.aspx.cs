using Core.Aplicacao;
using Dominio.Venda;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EComLivro.Adm
{
    public partial class AtualizaPedido : ViewGenerico
    {
        Pedido pedido = new Pedido();
        private Resultado resultado = new Resultado();

        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["idPedido"] = null;
                if (!string.IsNullOrEmpty(Request.QueryString["idPedido"]))
                {
                    pedido.ID = Convert.ToInt32(Request.QueryString["idPedido"]);
                    pedido = commands["CONSULTAR"].execute(pedido).Entidades.Cast<Pedido>().ElementAt(0);

                    switch (pedido.Status.ID)
                    {
                        case 1:
                            pedido.Status.ID++;
                            break;
                        case 2:
                            pedido.Status.ID += 2;
                            break;
                        // o case 3 é feito na hora da validação do pagamento,
                        // caso o pagamento seja REPROVADO, seta-se o status id 3
                        case 4:
                            pedido.Status.ID++;
                            break;
                        case 5:
                            pedido.Status.ID++;
                            break;
                        case 6:
                            pedido.Status.ID++;
                            break;
                        case 7:
                            pedido.Status.ID++;
                            break;
                        default:
                            break;
                    }

                    pedido.CCs = commands["CONSULTAR"].execute(new CartaoCreditoPedido() { IdPedido = pedido.ID }).Entidades.Cast<CartaoCreditoPedido>().ToList();

                    resultado = commands["ALTERAR"].execute(pedido);
                    if (!string.IsNullOrEmpty(resultado.Msg))
                    {
                        Response.Redirect("./ListaPedidos.aspx?resultadoAtualiza=" + resultado.Msg);
                    }
                    else
                    {
                        if(pedido.Status.ID == 8)
                        {
                            Session["idPedido"] = pedido.ID;
                            Response.Redirect("./ReentradaEstoqueTroca.aspx");
                        }
                        Response.Redirect("./ListaPedidos.aspx");
                    }
                }
                else
                {
                    Debug.Fail("ERROR : Nunca devemos chegar ao AtualizaPedido.aspx sem um idPedido.");
                    throw new Exception("ERROR : É ilegal carregar o AtualizaPedido.aspx sem definir um idPedido.");
                }

            }
        }
    }
}