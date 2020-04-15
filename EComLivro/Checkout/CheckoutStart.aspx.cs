using Core.Aplicacao;
using Dominio.Cliente;
using Dominio.Livro;
using Dominio.Venda;
using EComLivro.Logic;
using EComLivro.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EComLivro.Checkout
{
    public partial class CheckoutStart : ViewGenerico
    {
        ClientePF cliente = new ClientePF();
        private Resultado resultado = new Resultado();
        decimal cartTotal;
        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["idCupomPromo"] = null;
                Session["dropIdCupomTroca1"] = null;
                Session["dropIdCupomTroca2"] = null;
                Session["dropIdCupomTroca3"] = null;
                Session["dropIdCupomTroca4"] = null;
                Session["dropIdCupomTroca5"] = null;
                Session["txtValorCCPagto1"] = null;
                Session["txtValorCCPagto2"] = null;
                Session["txtValorCCPagto3"] = null;
                Session["txtValorCCPagto4"] = null;
                Session["txtValorCCPagto5"] = null;

                cliente = new ClientePF();
                // pega o e-mail/usuário conectado e passa para cliente
                cliente.Email = Context.User.Identity.Name;

                // pesquisa no BD pelo cliente com e-mail
                entidades = commands["CONSULTAR"].execute(cliente).Entidades;

                cliente = (ClientePF)entidades.ElementAt(0);

                // passa ID de cliente e consulta na tabela n-n
                foreach (ClientePFXEndereco clienteXEndereco in
                    commands["CONSULTAR"].execute(new ClientePFXEndereco { ID = cliente.ID }).Entidades)
                {
                    // Passa endereços para o cliente
                    cliente.Enderecos.Add(clienteXEndereco.Endereco);
                }

                dropIdEnderecoEntrega.DataSource = EnderecoEntregaDatatable(cliente.Enderecos);
                dropIdEnderecoEntrega.DataValueField = "ID";
                dropIdEnderecoEntrega.DataTextField = "Name";
                dropIdEnderecoEntrega.DataBind();

                // passa ID de cliente e consulta na tabela n-n
                foreach (ClientePFXCC clienteXCC in
                    commands["CONSULTAR"].execute(new ClientePFXCC { ID = cliente.ID }).Entidades)
                {
                    // Passa ccs para o cliente
                    cliente.CartoesCredito.Add(clienteXCC.CC);
                }

                dropIdCC1.DataSource = CartaoCreditoDatatable(cliente.CartoesCredito);
                dropIdCC1.DataValueField = "ID";
                dropIdCC1.DataTextField = "Name";
                dropIdCC1.DataBind();

                dropIdCC2.DataSource = CartaoCreditoDatatable(cliente.CartoesCredito);
                dropIdCC2.DataValueField = "ID";
                dropIdCC2.DataTextField = "Name";
                dropIdCC2.DataBind();

                dropIdCC3.DataSource = CartaoCreditoDatatable(cliente.CartoesCredito);
                dropIdCC3.DataValueField = "ID";
                dropIdCC3.DataTextField = "Name";
                dropIdCC3.DataBind();

                dropIdCC4.DataSource = CartaoCreditoDatatable(cliente.CartoesCredito);
                dropIdCC4.DataValueField = "ID";
                dropIdCC4.DataTextField = "Name";
                dropIdCC4.DataBind();

                dropIdCC5.DataSource = CartaoCreditoDatatable(cliente.CartoesCredito);
                dropIdCC5.DataValueField = "ID";
                dropIdCC5.DataTextField = "Name";
                dropIdCC5.DataBind();

                dropIdCupomTroca1.DataSource = CupomDatatable(commands["CONSULTAR"].execute(new Cupom() { IdCliente = cliente.ID, Status = 'A', Tipo = new TipoCupom() { ID = 1 } }).Entidades.Cast<Cupom>().ToList());
                dropIdCupomTroca1.DataValueField = "ID";
                dropIdCupomTroca1.DataTextField = "Name";
                dropIdCupomTroca1.DataBind();

                dropIdCupomTroca2.DataSource = CupomDatatable(commands["CONSULTAR"].execute(new Cupom() { IdCliente = cliente.ID, Status = 'A', Tipo = new TipoCupom() { ID = 1 } }).Entidades.Cast<Cupom>().ToList());
                dropIdCupomTroca2.DataValueField = "ID";
                dropIdCupomTroca2.DataTextField = "Name";
                dropIdCupomTroca2.DataBind();

                dropIdCupomTroca3.DataSource = CupomDatatable(commands["CONSULTAR"].execute(new Cupom() { IdCliente = cliente.ID, Status = 'A', Tipo = new TipoCupom() { ID = 1 } }).Entidades.Cast<Cupom>().ToList());
                dropIdCupomTroca3.DataValueField = "ID";
                dropIdCupomTroca3.DataTextField = "Name";
                dropIdCupomTroca3.DataBind();

                dropIdCupomTroca4.DataSource = CupomDatatable(commands["CONSULTAR"].execute(new Cupom() { IdCliente = cliente.ID, Status = 'A', Tipo = new TipoCupom() { ID = 1 } }).Entidades.Cast<Cupom>().ToList());
                dropIdCupomTroca4.DataValueField = "ID";
                dropIdCupomTroca4.DataTextField = "Name";
                dropIdCupomTroca4.DataBind();

                dropIdCupomTroca5.DataSource = CupomDatatable(commands["CONSULTAR"].execute(new Cupom() { IdCliente = cliente.ID, Status = 'A', Tipo = new TipoCupom() { ID = 1 } }).Entidades.Cast<Cupom>().ToList());
                dropIdCupomTroca5.DataValueField = "ID";
                dropIdCupomTroca5.DataTextField = "Name";
                dropIdCupomTroca5.DataBind();

                if (Session["payment_amt"] != null)
                {
                    string amt = Session["payment_amt"].ToString();

                    //bool ret = payPalCaller.ShortcutExpressCheckout(amt, ref token, ref retMsg);
                    //if (ret)
                    //{
                    //    Session["token"] = token;
                    //    Response.Redirect(retMsg);
                    //}
                    //else
                    //{
                    //    Response.Redirect("CheckoutError.aspx?" + retMsg);
                    //}

                    //Response.Redirect("Checkout/CheckoutReview.aspx");
                }
                else
                {
                    Response.Redirect("Checkout/CheckoutError.aspx?ErrorCode=AmtMissing");
                }
            }

            using (ShoppingCartActions usersShoppingCart = new ShoppingCartActions())
            {
                cartTotal = 0;
                cartTotal = usersShoppingCart.GetTotal();
                if (cartTotal > 0)
                {
                    // Display Total.
                    lblSubtotal.Text = String.Format("{0:c}", cartTotal);
                    lblFrete.Text = "-";
                    lblTotal.Text = "-";

                    AplicaCupom();

                    CalculaFrete();

                    RecebeValorCC();
                }
                else
                {
                    lblSubtotal.Text = "-";
                    UpdateBtn.Visible = false;
                    CheckoutBtn.Visible = false;
                }
            }
        }

        public static DataTable EnderecoEntregaDatatable(List<Endereco> input)
        {
            DataTable data = new DataTable();
            data.Columns.Add(new DataColumn("ID", typeof(int)));
            data.Columns.Add(new DataColumn("Name", typeof(string)));

            DataRow dr = data.NewRow();
            dr[0] = 0;
            dr[1] = "Selecione um Endereço de Entrega";
            data.Rows.Add(dr);

            int a = input.Count;
            for (int i = 0; i < a; i++)
            {
                Endereco endereco = input.ElementAt(i);
                dr = data.NewRow();
                dr[0] = endereco.ID;
                dr[1] = endereco.Nome + ", " + endereco.Destinatario + ", " + endereco.TipoResidencia.Nome + ", " +
                    endereco.TipoLogradouro.Nome + " " + endereco.Rua + ", " + endereco.Numero + ", " + endereco.Bairro + ", " +
                    endereco.Cidade.Nome + "/" + endereco.Cidade.Estado.Sigla + ", CEP: " + endereco.CEP + ", " + endereco.Observacao;
                data.Rows.Add(dr);
            }
            return data;
        }

        public static DataTable CartaoCreditoDatatable(List<CartaoCredito> input)
        {
            DataTable data = new DataTable();
            data.Columns.Add(new DataColumn("ID", typeof(int)));
            data.Columns.Add(new DataColumn("Name", typeof(string)));

            DataRow dr = data.NewRow();
            dr[0] = 0;
            dr[1] = "Selecione um Cartão de Crédito";
            data.Rows.Add(dr);

            int a = input.Count;
            for (int i = 0; i < a; i++)
            {
                CartaoCredito cc = input.ElementAt(i);
                dr = data.NewRow();
                dr[0] = cc.ID;
                dr[1] = cc.NomeImpresso + ", " + cc.NumeroCC + ", " + cc.Bandeira.Nome + ", " +
                    cc.CodigoSeguranca;
                data.Rows.Add(dr);
            }
            return data;
        }

        public static DataTable CupomDatatable(List<Cupom> input)
        {
            DataTable data = new DataTable();
            data.Columns.Add(new DataColumn("ID", typeof(int)));
            data.Columns.Add(new DataColumn("Name", typeof(string)));

            DataRow dr = data.NewRow();
            dr[0] = 0;
            dr[1] = "Selecione um Cupom de Troca";
            data.Rows.Add(dr);

            int a = input.Count;
            for (int i = 0; i < a; i++)
            {
                Cupom cupom = input.ElementAt(i);
                dr = data.NewRow();
                dr[0] = cupom.ID;
                dr[1] = cupom.CodigoCupom + " - R$ " + cupom.ValorCupom.ToString("N2");
                data.Rows.Add(dr);
            }
            return data;
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

                    Estoque estoque = commands["CONSULTAR"].execute(new Estoque() { Livro = new Dominio.Livro.Livro() { ID = cartUpdates[i].LivroId } }).Entidades.Cast<Estoque>().ElementAt(0);

                    TextBox quantityTextBox = new TextBox();
                    quantityTextBox = (TextBox)CartList.Rows[i].FindControl("PurchaseQuantity");

                    if (estoque.Qtde < Convert.ToInt32(quantityTextBox.Text))
                    {
                        cartUpdates[i].PurchaseQuantity = quantidadeAnterior;
                        lblResultadoCarrinho.Text = "Quantidade em estoque do livro " + estoque.Livro.Titulo + " é de " + estoque.Qtde + " unidade(s)";
                        lblResultadoCarrinho.Visible = true;
                    }
                    else
                    {
                        cartUpdates[i].PurchaseQuantity = Convert.ToInt16(quantityTextBox.Text.ToString());
                        lblResultadoCarrinho.Text = "";
                        lblResultadoCarrinho.Visible = false;
                    }
                }
                usersShoppingCart.UpdateShoppingCartDatabase(cartId, cartUpdates);
                CartList.DataBind();
                lblSubtotal.Text = String.Format("{0:c}", usersShoppingCart.GetTotal());
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
            if (GetShoppingCartItems().Count == 0)
            {
                Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
            }
        }

        protected void CheckoutBtn_Click(object sender, EventArgs e)
        {
            Pedido pedido = new Pedido();

            // pegando e adicionando usuário ao pedido
            pedido.Usuario = Context.User.Identity.Name;

            // pegando e adicionando endereço (ID) selecionado ao pedido
            pedido.EnderecoEntrega.ID = Convert.ToInt32(dropIdEnderecoEntrega.SelectedValue);

            // pegando e adicionando frete ao pedido
            float frete = (float)0.0;
            if (!lblFrete.Text.Trim().Equals("-"))
            {
                if (float.TryParse(lblFrete.Text.Remove(0, 3), out frete))
                {
                    pedido.Frete = frete;
                }
            }

            // pegando e adicionando total ao pedido
            float total = (float)0.0;
            if (!lblTotal.Text.Trim().Equals("-"))
            {
                if (float.TryParse(lblTotal.Text.Remove(0, 3), out total))
                {
                    pedido.Total = total;
                }
            }

            // Pegando e adicionando os item do carrinho em pedido
            foreach (CartItem cartItem in GetShoppingCartItems())
            {
                PedidoDetalhe detalhe = new PedidoDetalhe();
                detalhe.Livro.ID = cartItem.livro_id;
                detalhe.Livro.Titulo = cartItem.titulo_livro;
                detalhe.Quantidade = cartItem.quantidade;
                detalhe.ValorUnit = cartItem.valor_venda;

                pedido.Detalhes.Add(detalhe);
            }

            CartaoCreditoPedido cartaoPedido = new CartaoCreditoPedido();
            if (Session["txtValorCCPagto1"] != null)
            {
                cartaoPedido.CC.ID = Convert.ToInt32(dropIdCC1.Text); 
                decimal valorCC = 0;
                if (decimal.TryParse(Session["txtValorCCPagto1"].ToString(), NumberStyles.Number, CultureInfo.CreateSpecificCulture("en-US"), out valorCC))
                {
                    cartaoPedido.ValorCCPagto = (float)valorCC;
                }
                pedido.CCs.Add(cartaoPedido);
            }

            cartaoPedido = new CartaoCreditoPedido();
            if (Session["txtValorCCPagto2"] != null)
            {
                cartaoPedido.CC.ID = Convert.ToInt32(dropIdCC2.Text);
                decimal valorCC = 0;
                if (decimal.TryParse(Session["txtValorCCPagto2"].ToString(), NumberStyles.Number, CultureInfo.CreateSpecificCulture("en-US"), out valorCC))
                {
                    cartaoPedido.ValorCCPagto = (float)valorCC;
                }
                pedido.CCs.Add(cartaoPedido);
            }

            cartaoPedido = new CartaoCreditoPedido();
            if (Session["txtValorCCPagto3"] != null)
            {
                cartaoPedido.CC.ID = Convert.ToInt32(dropIdCC3.Text);
                decimal valorCC = 0;
                if (decimal.TryParse(Session["txtValorCCPagto3"].ToString(), NumberStyles.Number, CultureInfo.CreateSpecificCulture("en-US"), out valorCC))
                {
                    cartaoPedido.ValorCCPagto = (float)valorCC;
                }
                pedido.CCs.Add(cartaoPedido);
            }

            cartaoPedido = new CartaoCreditoPedido();
            if (Session["txtValorCCPagto4"] != null)
            {
                cartaoPedido.CC.ID = Convert.ToInt32(dropIdCC4.Text);
                decimal valorCC = 0;
                if (decimal.TryParse(Session["txtValorCCPagto4"].ToString(), NumberStyles.Number, CultureInfo.CreateSpecificCulture("en-US"), out valorCC))
                {
                    cartaoPedido.ValorCCPagto = (float)valorCC;
                }
                pedido.CCs.Add(cartaoPedido);
            }

            cartaoPedido = new CartaoCreditoPedido();
            if (Session["txtValorCCPagto5"] != null)
            {
                cartaoPedido.CC.ID = Convert.ToInt32(dropIdCC5.Text);
                decimal valorCC = 0;
                if (decimal.TryParse(Session["txtValorCCPagto5"].ToString(), NumberStyles.Number, CultureInfo.CreateSpecificCulture("en-US"), out valorCC))
                {
                    cartaoPedido.ValorCCPagto = (float)valorCC;
                }
                pedido.CCs.Add(cartaoPedido);
            }

            if (Session["idCupomPromo"] != null)
            {
                pedido.CupomPromocional.ID = Convert.ToInt32(Session["idCupomPromo"]);
            }

            Cupom cupomTroca = new Cupom();
            if (Session["dropIdCupomTroca1"] != null)
            {
                cupomTroca.ID = Convert.ToInt32(Session["dropIdCupomTroca1"]);
                pedido.CuponsTroca.Add(cupomTroca);
            }

            cupomTroca = new Cupom();
            if (Session["dropIdCupomTroca2"] != null)
            {
                cupomTroca.ID = Convert.ToInt32(Session["dropIdCupomTroca2"]);
                pedido.CuponsTroca.Add(cupomTroca);
            }

            cupomTroca = new Cupom();
            if (Session["dropIdCupomTroca3"] != null)
            {
                cupomTroca.ID = Convert.ToInt32(Session["dropIdCupomTroca3"]);
                pedido.CuponsTroca.Add(cupomTroca);
            }

            cupomTroca = new Cupom();
            if (Session["dropIdCupomTroca4"] != null)
            {
                cupomTroca.ID = Convert.ToInt32(Session["dropIdCupomTroca4"]);
                pedido.CuponsTroca.Add(cupomTroca);
            }

            cupomTroca = new Cupom();
            if (Session["dropIdCupomTroca5"] != null)
            {
                cupomTroca.ID = Convert.ToInt32(Session["dropIdCupomTroca5"]);
                pedido.CuponsTroca.Add(cupomTroca);
            }

            // passando Status inicial para o pedido
            // Status.ID = 1 / EM PROCESSAMENTO
            pedido.Status.ID = 1;

            resultado = commands["SALVAR"].execute(pedido);
            if (!string.IsNullOrEmpty(resultado.Msg))
            {
                lblResultado.Visible = true;
                lblResultado.Text = resultado.Msg;
            }
            else
            {

                Response.Redirect("./CheckoutReview.aspx?idPedido=" + pedido.ID);
            }

            //using (ShoppingCartActions usersShoppingCart = new ShoppingCartActions())
            //{
            //    Session["payment_amt"] = usersShoppingCart.GetTotal(); // falta frete
            //}
            //Response.Redirect("~/Checkout/CheckoutStart.aspx");
        }

        protected void dropIdEnderecoEntrega_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculaFrete();
        }

        public void CalculaFrete()
        {
            // Irá calcular frete conforme o endereço de entrega alterar

            double frete = 0;

            if (Convert.ToInt32(dropIdEnderecoEntrega.SelectedIndex) != 0)
            {
                Endereco endereco = new Endereco();
                // passar o ID do endereço selecionado 
                endereco.ID = Convert.ToInt32(dropIdEnderecoEntrega.SelectedValue);

                // pesquisa no BD pelo cliente com e-mail
                entidades = commands["CONSULTAR"].execute(endereco).Entidades;

                endereco = (Endereco)entidades.ElementAt(0);
                int quantidade = 0;

                using (ShoppingCartActions usersShoppingCart = new ShoppingCartActions())
                {
                    // pega quantidade de livros no carrinho
                    quantidade = usersShoppingCart.GetCount();
                }

                // if por ID de estado e arranjo de região
                // SUL
                if (endereco.Cidade.Estado.ID == 18 ||
                    endereco.Cidade.Estado.ID == 23 ||
                    endereco.Cidade.Estado.ID == 24)
                {
                    frete = (quantidade * 5.5);
                }
                // SUDESTE
                else if (endereco.Cidade.Estado.ID == 8 ||
                    endereco.Cidade.Estado.ID == 11 ||
                    endereco.Cidade.Estado.ID == 19 ||
                    endereco.Cidade.Estado.ID == 26)
                {
                    frete = (quantidade * 4.5);
                }
                // CENTRO-OESTE
                else if (endereco.Cidade.Estado.ID == 7 ||
                    endereco.Cidade.Estado.ID == 9 ||
                    endereco.Cidade.Estado.ID == 12 ||
                    endereco.Cidade.Estado.ID == 13)
                {
                    frete = (quantidade * 6.5);
                }
                // NORTE
                else if (endereco.Cidade.Estado.ID == 1 ||
                    endereco.Cidade.Estado.ID == 3 ||
                    endereco.Cidade.Estado.ID == 4 ||
                    endereco.Cidade.Estado.ID == 14 ||
                    endereco.Cidade.Estado.ID == 21 ||
                    endereco.Cidade.Estado.ID == 22 ||
                    endereco.Cidade.Estado.ID == 27)
                {
                    frete = (quantidade * 8.5);
                }
                // NORDESTE (2, 5, 6, 10, 15, 16, 17, 20 e 25)
                else
                {
                    frete = (quantidade * 7.5);
                }
            }

            if (frete != 0)
            {
                // passa para lblFrete o valor do frete
                lblFrete.Text = String.Format("{0:c}", frete);
                float subtotal;
                if (float.TryParse(lblSubtotal.Text.Remove(0, 3), out subtotal))
                {
                    lblTotal.Text = String.Format("{0:c}", frete + subtotal);
                }
            }
        }

        protected void btnAplicaCupomPromo_Click(object sender, EventArgs e)
        {
            Cupom cupomPromo = new Cupom();
            cupomPromo.CodigoCupom = txtCupomPromo.Text;

            entidades = commands["CONSULTAR"].execute(cupomPromo).Entidades;

            // verifica se veio APENAS 1 
            if (entidades.Count == 1)
            {
                cupomPromo = (Cupom)entidades.ElementAt(0);

                //se o código é IGUAL ao que foi digitado
                if (cupomPromo.CodigoCupom.Trim().Equals(txtCupomPromo.Text.Trim()))
                {
                    // verifica se é um cupom promocional
                    if (cupomPromo.Tipo.ID == 2)
                    {
                        // verifica se cupom está ativo
                        if (cupomPromo.Status == 'A')
                        {
                            Session["idCupomPromo"] = cupomPromo.ID;
                            lblResultadoAplicaCupomPromo.Visible = false;
                            lblResultadoAplicaCupomPromo.Text = "";
                            AplicaCupom();
                        }
                        else
                        {
                            lblResultadoAplicaCupomPromo.Visible = true;
                            lblResultadoAplicaCupomPromo.Text = "CUPOM INATIVO!";
                            Session["idCupomPromo"] = null;
                        }

                    }
                    else
                    {
                        lblResultadoAplicaCupomPromo.Visible = true;
                        lblResultadoAplicaCupomPromo.Text = "NÃO É UM CUPOM PROMOCIONAL!";
                        Session["idCupomPromo"] = null;
                    }
                }
                else
                {
                    lblResultadoAplicaCupomPromo.Visible = true;
                    lblResultadoAplicaCupomPromo.Text = "CUPOM INEXISTENTE!";
                    Session["idCupomPromo"] = null;
                }
            }
            else
            {
                lblResultadoAplicaCupomPromo.Visible = true;
                lblResultadoAplicaCupomPromo.Text = "CUPOM INEXISTENTE!";
                Session["idCupomPromo"] = null;
            }
        }

        protected void dropIdCupomTroca1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropIdCupomTroca1.SelectedIndex != 0)
            {
                // passar o ID do cupom selecionado 
                Session["dropIdCupomTroca1"] = Convert.ToInt32(dropIdCupomTroca1.SelectedValue);
                AplicaCupom();
                dropIdCupomTroca2.Visible = true;
            }
            else
            {
                Session["dropIdCupomTroca1"] = null;
                Session["dropIdCupomTroca2"] = null;
                Session["dropIdCupomTroca3"] = null;
                Session["dropIdCupomTroca4"] = null;
                Session["dropIdCupomTroca5"] = null;
                dropIdCupomTroca2.Visible = false;
                dropIdCupomTroca3.Visible = false;
                dropIdCupomTroca4.Visible = false;
                dropIdCupomTroca5.Visible = false;
                dropIdCupomTroca2.SelectedIndex = 0;
                dropIdCupomTroca3.SelectedIndex = 0;
                dropIdCupomTroca4.SelectedIndex = 0;
                dropIdCupomTroca5.SelectedIndex = 0;
            }
        }

        protected void dropIdCupomTroca2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropIdCupomTroca2.SelectedIndex != 0)
            {
                // passar o ID do cupom selecionado 
                Session["dropIdCupomTroca2"] = Convert.ToInt32(dropIdCupomTroca2.SelectedValue);
                AplicaCupom();
                dropIdCupomTroca3.Visible = true;

            }
            else
            {
                Session["dropIdCupomTroca2"] = null;
                Session["dropIdCupomTroca3"] = null;
                Session["dropIdCupomTroca4"] = null;
                Session["dropIdCupomTroca5"] = null;
                dropIdCupomTroca3.Visible = false;
                dropIdCupomTroca4.Visible = false;
                dropIdCupomTroca5.Visible = false;
                dropIdCupomTroca3.SelectedIndex = 0;
                dropIdCupomTroca4.SelectedIndex = 0;
                dropIdCupomTroca5.SelectedIndex = 0;
            }

        }

        protected void dropIdCupomTroca3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropIdCupomTroca3.SelectedIndex != 0)
            {
                // passar o ID do cupom selecionado 
                Session["dropIdCupomTroca3"] = Convert.ToInt32(dropIdCupomTroca3.SelectedValue);
                AplicaCupom();
                dropIdCupomTroca4.Visible = true;

            }
            else
            {
                Session["dropIdCupomTroca3"] = null;
                Session["dropIdCupomTroca4"] = null;
                Session["dropIdCupomTroca5"] = null;
                dropIdCupomTroca4.Visible = false;
                dropIdCupomTroca5.Visible = false;
                dropIdCupomTroca4.SelectedIndex = 0;
                dropIdCupomTroca5.SelectedIndex = 0;
            }

        }

        protected void dropIdCupomTroca4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropIdCupomTroca4.SelectedIndex != 0)
            {
                // passar o ID do cupom selecionado 
                Session["dropIdCupomTroca4"] = Convert.ToInt32(dropIdCupomTroca4.SelectedValue);
                AplicaCupom();
                dropIdCupomTroca5.Visible = true;

            }
            else
            {
                Session["dropIdCupomTroca4"] = null;
                Session["dropIdCupomTroca5"] = null;
                dropIdCupomTroca5.Visible = false;
                dropIdCupomTroca5.SelectedIndex = 0;
            }
        }

        protected void dropIdCupomTroca5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropIdCupomTroca5.SelectedIndex != 0)
            {
                // passar o ID do cupom selecionado 
                Session["dropIdCupomTroca5"] = Convert.ToInt32(dropIdCupomTroca5.SelectedValue);
                AplicaCupom();

            }
            else
            {
                Session["dropIdCupomTroca5"] = null;
                dropIdCupomTroca5.SelectedIndex = 0;
            }
        }

        public void AplicaCupom()
        {
            Cupom cupom = new Cupom();
            double total = (double)cartTotal;

            if (Session["idCupomPromo"] != null)
            {
                cupom = new Cupom();
                // passar o ID do cupom selecionado 
                cupom.ID = Convert.ToInt32(Session["idCupomPromo"].ToString());

                cupom = (Cupom)commands["CONSULTAR"].execute(cupom).Entidades.ElementAt(0);
                total = total - (total * cupom.ValorCupom);
            }

            if (Session["dropIdCupomTroca1"] != null)
            {
                cupom = new Cupom();
                // passar o ID do cupom selecionado 
                cupom.ID = Convert.ToInt32(Session["dropIdCupomTroca1"].ToString());

                cupom = (Cupom)commands["CONSULTAR"].execute(cupom).Entidades.ElementAt(0);
                total = total - (cupom.ValorCupom);
            }

            if (Session["dropIdCupomTroca2"] != null)
            {
                cupom = new Cupom();
                // passar o ID do cupom selecionado 
                cupom.ID = Convert.ToInt32(Session["dropIdCupomTroca2"].ToString());

                cupom = (Cupom)commands["CONSULTAR"].execute(cupom).Entidades.ElementAt(0);
                total = total - (cupom.ValorCupom);
            }

            if (Session["dropIdCupomTroca3"] != null)
            {
                cupom = new Cupom();
                // passar o ID do cupom selecionado 
                cupom.ID = Convert.ToInt32(Session["dropIdCupomTroca3"].ToString());

                cupom = (Cupom)commands["CONSULTAR"].execute(cupom).Entidades.ElementAt(0);
                total = total - (cupom.ValorCupom);
            }

            if (Session["dropIdCupomTroca4"] != null)
            {
                cupom = new Cupom();
                // passar o ID do cupom selecionado 
                cupom.ID = Convert.ToInt32(Session["dropIdCupomTroca4"].ToString());

                cupom = (Cupom)commands["CONSULTAR"].execute(cupom).Entidades.ElementAt(0);
                total = total - (cupom.ValorCupom);
            }

            if (Session["dropIdCupomTroca5"] != null)
            {
                cupom = new Cupom();
                // passar o ID do cupom selecionado 
                cupom.ID = Convert.ToInt32(Session["dropIdCupomTroca5"].ToString());

                cupom = (Cupom)commands["CONSULTAR"].execute(cupom).Entidades.ElementAt(0);
                total = total - (cupom.ValorCupom);
            }

            if (total < 0)
            {
                total = 0.0;
            }

            lblSubtotal.Text = String.Format("{0:c}", total);
            if (!lblFrete.Text.Trim().Equals("-"))
            {
                float frete;
                if (float.TryParse(lblFrete.Text.Remove(0, 3), out frete))
                {
                    lblTotal.Text = String.Format("{0:c}", frete + total);
                    RecebeValorCC();
                }
            }
        }

        public void RecebeValorCC()
        {
            decimal aPagar = 0;
            decimal valorCC = 0;
            if (!lblTotal.Text.Trim().Equals("-"))
            {
                if (decimal.TryParse(lblTotal.Text.Remove(0, 3), out valorCC))
                {
                    aPagar = valorCC;
                }
            }
            else if (decimal.TryParse(lblSubtotal.Text.Remove(0, 3), out valorCC))
            {
                aPagar = valorCC;
            }

            if (Session["txtValorCCPagto1"] != null)
            {
                valorCC = 0;
                if (decimal.TryParse(Session["txtValorCCPagto1"].ToString(), NumberStyles.Number, CultureInfo.CreateSpecificCulture("en-US"), out valorCC))
                {
                    aPagar = aPagar - valorCC;
                }
            }

            if (Session["txtValorCCPagto2"] != null)
            {
                valorCC = 0;
                if (decimal.TryParse(Session["txtValorCCPagto2"].ToString(), NumberStyles.Number, CultureInfo.CreateSpecificCulture("en-US"), out valorCC))
                {
                    aPagar = aPagar - valorCC;
                }
            }

            if (Session["txtValorCCPagto3"] != null)
            {
                valorCC = 0;
                if (decimal.TryParse(Session["txtValorCCPagto3"].ToString(), NumberStyles.Number, CultureInfo.CreateSpecificCulture("en-US"), out valorCC))
                {
                    aPagar = aPagar - valorCC;
                }
            }

            if (Session["txtValorCCPagto4"] != null)
            {
                valorCC = 0;
                if (decimal.TryParse(Session["txtValorCCPagto4"].ToString(), NumberStyles.Number, CultureInfo.CreateSpecificCulture("en-US"), out valorCC))
                {
                    aPagar = aPagar - valorCC;
                }
            }

            if (Session["txtValorCCPagto5"] != null)
            {
                valorCC = 0;
                if (decimal.TryParse(Session["txtValorCCPagto5"].ToString(), NumberStyles.Number, CultureInfo.CreateSpecificCulture("en-US"), out valorCC))
                {
                    aPagar = aPagar - valorCC;
                }
            }

            if (aPagar < 0)
            {
                lblResultadoPagto.Visible = true;
                lblResultadoPagto.Text = "VALORES DOS CARTÕES DE CRÉDITO NÃO PODE SER MAIOR QUE O VALOR A PAGAR!"; 

                valorCC = 0; 
                if (!lblTotal.Text.Trim().Equals("-"))
                {
                    if (decimal.TryParse(lblTotal.Text.Remove(0, 3), out valorCC))
                    {
                        aPagar = valorCC;
                    }
                }
            } else
            {
                lblResultadoPagto.Visible = false;
            }

            if(aPagar == 0)
            {
                CheckoutBtn.Enabled = true;
            } else
            {
                CheckoutBtn.Enabled = false;
            }

            lblAPagar.Text = String.Format("{0:c}", aPagar);
        }

        protected void txtValorCCPagto1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtValorCCPagto1.Text))
            {
                // passar o ID do cupom selecionado 
                Session["txtValorCCPagto1"] = txtValorCCPagto1.Text;
                RecebeValorCC();
                dropIdCC2.Visible = true;
                txtValorCCPagto2.Visible = true;
            }
            else
            {
                Session["txtValorCCPagto1"] = null;
                Session["txtValorCCPagto2"] = null;
                Session["txtValorCCPagto3"] = null;
                Session["txtValorCCPagto4"] = null;
                Session["txtValorCCPagto5"] = null;
                dropIdCC2.Visible = false;
                dropIdCC2.SelectedIndex = 0;
                txtValorCCPagto2.Visible = false;
                txtValorCCPagto2.Text = "";
                dropIdCC3.Visible = false;
                dropIdCC3.SelectedIndex = 0;
                txtValorCCPagto3.Visible = false;
                txtValorCCPagto3.Text = "";
                dropIdCC4.Visible = false;
                dropIdCC4.SelectedIndex = 0;
                txtValorCCPagto4.Visible = false;
                txtValorCCPagto4.Text = "";
                dropIdCC5.Visible = false;
                dropIdCC5.SelectedIndex = 0;
                txtValorCCPagto5.Visible = false;
                txtValorCCPagto5.Text = "";
            }
        }

        protected void txtValorCCPagto2_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtValorCCPagto2.Text))
            {
                // passar o ID do cupom selecionado 
                Session["txtValorCCPagto2"] = txtValorCCPagto2.Text;
                RecebeValorCC();
                dropIdCC3.Visible = true;
                txtValorCCPagto3.Visible = true;
            }
            else
            {
                Session["txtValorCCPagto2"] = null;
                Session["txtValorCCPagto3"] = null;
                Session["txtValorCCPagto4"] = null;
                Session["txtValorCCPagto5"] = null;
                dropIdCC3.Visible = false;
                dropIdCC3.SelectedIndex = 0;
                txtValorCCPagto3.Visible = false;
                txtValorCCPagto3.Text = "";
                dropIdCC4.Visible = false;
                dropIdCC4.SelectedIndex = 0;
                txtValorCCPagto4.Visible = false;
                txtValorCCPagto4.Text = "";
                dropIdCC5.Visible = false;
                dropIdCC5.SelectedIndex = 0;
                txtValorCCPagto5.Visible = false;
                txtValorCCPagto5.Text = "";
            }
        }

        protected void txtValorCCPagto3_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtValorCCPagto3.Text))
            {
                // passar o ID do cupom selecionado 
                Session["txtValorCCPagto3"] = txtValorCCPagto3.Text;
                RecebeValorCC();
                dropIdCC4.Visible = true;
                txtValorCCPagto4.Visible = true;
            }
            else
            {
                Session["txtValorCCPagto3"] = null;
                Session["txtValorCCPagto4"] = null;
                Session["txtValorCCPagto5"] = null;
                dropIdCC4.Visible = false;
                dropIdCC4.SelectedIndex = 0;
                txtValorCCPagto4.Visible = false;
                txtValorCCPagto4.Text = "";
                dropIdCC5.Visible = false;
                dropIdCC5.SelectedIndex = 0;
                txtValorCCPagto5.Visible = false;
                txtValorCCPagto5.Text = "";
            }
        }

        protected void txtValorCCPagto4_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtValorCCPagto4.Text))
            {
                // passar o ID do cupom selecionado 
                Session["txtValorCCPagto4"] = txtValorCCPagto4.Text;
                RecebeValorCC();
                dropIdCC5.Visible = true;
                txtValorCCPagto5.Visible = true;
            }
            else
            {
                Session["txtValorCCPagto4"] = null;
                Session["txtValorCCPagto5"] = null;
                dropIdCC5.Visible = false;
                dropIdCC5.SelectedIndex = 0;
                txtValorCCPagto5.Visible = false;
                txtValorCCPagto5.Text = "";
            }
        }

        protected void txtValorCCPagto5_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtValorCCPagto5.Text))
            {
                // passar o ID do cupom selecionado 
                Session["txtValorCCPagto5"] = txtValorCCPagto5.Text;
                RecebeValorCC();
            }
            else
            {
                Session["txtValorCCPagto5"] = null;
            }
        }
    }
}