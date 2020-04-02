using EComLivro.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EComLivro.Loja
{
    public partial class MastePageLoja : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            using (ShoppingCartActions usersShoppingCart = new ShoppingCartActions())
            {
                //string cartStr = string.Format("Carrinho ({0})", usersShoppingCart.GetCount());
                string cartStr = string.Format("({0})", usersShoppingCart.GetCount());
                cartCount.InnerText = cartStr;
            }
        }
    }
}