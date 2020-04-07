using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EComLivro.Checkout
{
    public partial class CheckoutComplete : ViewGenerico
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Verifica se completou o processo de checkout
                if ((string)Session["userCheckoutCompleted"] != "true")
                {
                    Session["userCheckoutCompleted"] = string.Empty;
                    Response.Redirect("CheckoutError.aspx?" + "Desc=Unvalidated%20Checkout.");
                }

                if (Session["currentOrderId"] != string.Empty)
                {
                    OrderId.Text = Convert.ToInt32(Session["currentOrderID"]).ToString();
                }

                // limpa carrinho
                using (EComLivro.Logic.ShoppingCartActions usersShoppingCart =
                    new EComLivro.Logic.ShoppingCartActions())
                {
                    usersShoppingCart.EmptyCart();
                }

                // Limpa sessão
                Session["currentOrderId"] = string.Empty;
            }
            else
            {
                //Response.Redirect("./CheckoutError.aspx?Desc=ID%20de%20pedido%20incompatível.");
            }
        }

        protected void Continue_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Loja/Index.aspx");
        }
    }
}
