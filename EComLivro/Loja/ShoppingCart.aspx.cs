using EComLivro.Logic;
using EComLivro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.Collections;
using System.Web.ModelBinding;
using Dominio.Livro;

namespace EComLivro.Loja
{
    public partial class ShoppingCart : ViewGenerico
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            using (ShoppingCartActions usersShoppingCart = new ShoppingCartActions())
            {
                decimal cartTotal = 0;
                cartTotal = usersShoppingCart.GetTotal();
                if (cartTotal > 0)
                {
                    // Display Total.
                    lblTotal.Text = String.Format("{0:c}", cartTotal);
                }
                else
                {
                    lblTotal.Text = "-";
                    ShoppingCartTitle.InnerText = "Carrinho está vazio!";
                    UpdateBtn.Visible = false;
                    CartTotal.Visible = false;
                    CheckoutBtn.Visible = false;
                }
                //UpdateCartItems();
            }
        }

        public List<CartItem> GetShoppingCartItems()
        {
            ShoppingCartActions actions = new ShoppingCartActions();
            return actions.GetCartItems();
        }

        public List<CartItem> UpdateCartItems()
        {
            using (ShoppingCartActions usersShoppingCart = new ShoppingCartActions())
            {
                String cartId = usersShoppingCart.GetCartId();

                ShoppingCartActions.ShoppingCartUpdates[] cartUpdates = new ShoppingCartActions.ShoppingCartUpdates[CartList.Rows.Count];
                for (int i = 0; i < CartList.Rows.Count; i++)
                {
                    IOrderedDictionary rowValues = new OrderedDictionary();
                    rowValues = GetValues(CartList.Rows[i]);
                    cartUpdates[i].LivroId = Convert.ToInt32(rowValues["livro_id"]);

                    CheckBox cbRemove = new CheckBox();
                    cbRemove = (CheckBox)CartList.Rows[i].FindControl("Remover");
                    cartUpdates[i].RemoverItem = cbRemove.Checked;

                    int quantidadeAnterior = GetShoppingCartItems().Cast<CartItem>().ElementAt(i).quantidade;

                    Estoque estoque = commands["CONSULTAR"].execute(new Estoque() { Livro = new Dominio.Livro.Livro() { ID = cartUpdates[i].LivroId } } ).Entidades.Cast<Estoque>().ElementAt(0);

                    TextBox quantityTextBox = new TextBox();
                    quantityTextBox = (TextBox)CartList.Rows[i].FindControl("PurchaseQuantity");
                    
                    if(estoque.Qtde < Convert.ToInt32(quantityTextBox.Text))
                    {
                        cartUpdates[i].PurchaseQuantity = quantidadeAnterior;
                        lblResultadoCarrinho.Text = "Quantidade em estoque do livro " + estoque.Livro.Titulo + " é de " + estoque.Qtde + " unidade(s)";
                        lblResultadoCarrinho.Visible = true;
                    } else
                    {
                        cartUpdates[i].PurchaseQuantity = Convert.ToInt16(quantityTextBox.Text.ToString());
                        lblResultadoCarrinho.Text = "";
                        lblResultadoCarrinho.Visible = false;
                    }
                    
                }
                usersShoppingCart.UpdateShoppingCartDatabase(cartId, cartUpdates);
                CartList.DataBind();
                lblTotal.Text = String.Format("{0:c}", usersShoppingCart.GetTotal());
                return usersShoppingCart.GetCartItems();
            }
        }

        public static IOrderedDictionary GetValues(GridViewRow row)
        {
            IOrderedDictionary values = new OrderedDictionary();
            foreach (DataControlFieldCell cell in row.Cells)
            {
                if (cell.Visible)
                {
                    // extrai os valores da célula
                    cell.ContainingField.ExtractValuesFromCell(values, cell, row.RowState, true);
                }
            }
            return values;
        }

        protected void UpdateBtn_Click(object sender, EventArgs e)
        {
            UpdateCartItems(); 
            if(GetShoppingCartItems().Count == 0)
            {
                Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
            }
        }

        protected void CheckoutBtn_Click(object sender, EventArgs e)
        {
            using (ShoppingCartActions usersShoppingCart = new ShoppingCartActions())
            {
                Session["payment_amt"] = usersShoppingCart.GetTotal();
            }
            Response.Redirect("~/Checkout/CheckoutStart.aspx");
        }
    }
}