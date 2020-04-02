using EComLivro.Logic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EComLivro.Loja
{
    public partial class AddToCart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string rawId = Request.QueryString["ProductID"];
            string rawId = Request.QueryString["idLivro"];
            int livroId;

            if (!String.IsNullOrEmpty(rawId) && int.TryParse(rawId, out livroId))
            {
                using (ShoppingCartActions usersShoppingCart = new ShoppingCartActions())
                {
                    usersShoppingCart.AddToCart(Convert.ToInt16(rawId));
                }
            }
            else
            {
                Debug.Fail("ERROR : Nunca devemos chegar ao AddToCart.aspx sem um idLivro.");
                throw new Exception("ERROR : É ilegal carregar o AddToCart.aspx sem definir um idLivro.");
            }
            Response.Redirect("ShoppingCart.aspx");
        }
    }
}