using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.DAO;
using Dominio;
using Dominio.Livro;
using EComLivro.Command;
using EComLivro.Models;

namespace EComLivro.Logic
{
    public class ShoppingCartActions : IDisposable
    {
        public string ShoppingCartId { get; set; }

        private LivroContext _db = new LivroContext();

        public const string CartSessionKey = "CartId";

        protected List<EntidadeDominio> entidades = new List<EntidadeDominio>();

        public void AddToCart(int id)
        {
            // Retorna o livro do BD      
            ShoppingCartId = GetCartId();

            var cartItem = _db.ShoppingCartItems.SingleOrDefault(
                c => c.cart_id == ShoppingCartId
                && c.livro_id == id);

            if (cartItem == null)
            {
                // necessário instância dessa forma para não ter abiguidade do nome da classe
                Dominio.Livro.Livro livro = new Dominio.Livro.Livro();
                LivroDAO livroDAO = new LivroDAO();
                EstoqueDAO estoqueDAO = new EstoqueDAO();
                Estoque estoque = new Estoque();

                livro.ID = id;
                estoque.Livro.ID = livro.ID;

                //entidades = commands["CONSULTAR"].execute(livro).Entidades;
                entidades = livroDAO.Consultar(livro);
                livro = (Dominio.Livro.Livro)entidades.ElementAt(0);

                //entidades = commands["CONSULTAR"].execute(estoque).Entidades;
                entidades = estoqueDAO.Consultar(estoque);
                estoque = (Estoque)entidades.ElementAt(0);

                // Cria um novo CartItem se não existir               
                cartItem = new CartItem
                {
                    item_id = Guid.NewGuid().ToString(),
                    livro_id = livro.ID,
                    titulo_livro = livro.Titulo,
                    valor_venda = estoque.ValorVenda,
                    cart_id = ShoppingCartId,
                    quantidade = 1,
                    data_criada = DateTime.Now
                };

                _db.ShoppingCartItems.Add(cartItem);
            }
            else
            {
                // se o item não existir no carrinho,
                // então adiciona um em quantidade        
                cartItem.quantidade++;
            }
            _db.SaveChanges();
        }

        public void Dispose()
        {
            if (_db != null)
            {
                _db.Dispose();
                _db = null;
            }

        }

        public string GetCartId()
        {
            if (HttpContext.Current.Session[CartSessionKey] == null)
            {
                // falta imprementação de login
                //if (!string.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name))
                //{
                //    HttpContext.Current.Session[CartSessionKey] = HttpContext.Current.User.Identity.Name;
                //}
                //else
                {
                    // gera um novo GUID randomico
                    Guid tempCartId = Guid.NewGuid();
                    HttpContext.Current.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return HttpContext.Current.Session[CartSessionKey].ToString();
        }

        public List<CartItem> GetCartItems()
        {
            ShoppingCartId = GetCartId();

            return _db.ShoppingCartItems.Where(
                c => c.cart_id == ShoppingCartId).ToList();
        }

        public decimal GetTotal()
        {
            ShoppingCartId = GetCartId();

            // multipica o valor de venda do livro pela quantidade e pega 
            // valor corrente de cada um dos livros no carrinho.
            // No final ele soma todos os totais e pega o valor total do carrinho.
            decimal? total = decimal.Zero;
            total = (decimal?)(from cartItems in _db.ShoppingCartItems
                               where cartItems.cart_id == ShoppingCartId
                               select (int?)cartItems.quantidade *
                               cartItems.valor_venda).Sum();
            return total ?? decimal.Zero;
        }

        public ShoppingCartActions GetCart(HttpContext context)
        {
            using (var cart = new ShoppingCartActions())
            {
                cart.ShoppingCartId = cart.GetCartId();
                return cart;
            }
        }

        public void UpdateShoppingCartDatabase(String cartId, ShoppingCartUpdates[] CartItemUpdates)
        {
            using (var db = new EComLivro.Models.LivroContext())
            {
                try
                {
                    int CartItemCount = CartItemUpdates.Count();
                    List<CartItem> myCart = GetCartItems();
                    foreach (var cartItem in myCart)
                    {
                        // itera todas as linhas da lista do carrinho
                        for (int i = 0; i < CartItemCount; i++)
                        {
                            if (cartItem.livro_id == CartItemUpdates[i].LivroId)
                            {
                                if (CartItemUpdates[i].PurchaseQuantity < 1 || CartItemUpdates[i].RemoverItem == true)
                                {
                                    RemoveItem(cartId, cartItem.livro_id);
                                }
                                else
                                {
                                    UpdateItem(cartId, cartItem.livro_id, CartItemUpdates[i].PurchaseQuantity);
                                }
                            }
                        }
                    }
                }
                catch (Exception exp)
                {
                    throw new Exception("ERROR: Incapaz de executar alteração no BD do Carrinho - " + exp.Message.ToString(), exp);
                }
            }
        }

        public void RemoveItem(string removeCartID, int removeProductID)
        {
            using (var _db = new EComLivro.Models.LivroContext())
            {
                try
                {
                    var myItem = (from c in _db.ShoppingCartItems where c.cart_id == removeCartID && c.livro_id == removeProductID select c).FirstOrDefault();
                    if (myItem != null)
                    {
                        // Remove Item.
                        _db.ShoppingCartItems.Remove(myItem);
                        _db.SaveChanges();
                    }
                }
                catch (Exception exp)
                {
                    throw new Exception("ERROR: Incapaz de remover item do carrinho - " + exp.Message.ToString(), exp);
                }
            }
        }

        public void UpdateItem(string updateCartID, int updateProductID, int quantity)
        {
            using (var _db = new EComLivro.Models.LivroContext())
            {
                try
                {
                    var myItem = (from c in _db.ShoppingCartItems where c.cart_id == updateCartID && c.livro_id == updateProductID select c).FirstOrDefault();
                    if (myItem != null)
                    {
                        myItem.quantidade = quantity;
                        _db.SaveChanges();
                    }
                }
                catch (Exception exp)
                {
                    throw new Exception("ERROR: Unable to Update Cart Item - " + exp.Message.ToString(), exp);
                }
            }
        }

        public void EmptyCart()
        {
            ShoppingCartId = GetCartId();
            var cartItems = _db.ShoppingCartItems.Where(
                c => c.cart_id == ShoppingCartId);
            foreach (var cartItem in cartItems)
            {
                _db.ShoppingCartItems.Remove(cartItem);
            }
            // Save changes.             
            _db.SaveChanges();
        }

        public int GetCount()
        {
            ShoppingCartId = GetCartId();

            // Get the count of each item in the cart and sum them up          
            int? count = (from cartItems in _db.ShoppingCartItems
                          where cartItems.cart_id == ShoppingCartId
                          select (int?)cartItems.quantidade).Sum();
            // Return 0 if all entries are null         
            return count ?? 0;
        }

        public struct ShoppingCartUpdates
        {
            public int LivroId;
            public int PurchaseQuantity;
            public bool RemoverItem;
        }
    }
}