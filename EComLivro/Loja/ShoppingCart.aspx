<%@ Page Title="" Language="C#" MasterPageFile="~/Loja/MastePageLoja.Master" AutoEventWireup="true" CodeBehind="ShoppingCart.aspx.cs" Inherits="EComLivro.Loja.ShoppingCart" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="cart-table-area section-padding-100">
        <div class="container-fluid">
            <div class="row">
                <div class="col-12 col-lg-8">
                    <div class="cart-title mt-50">
                        <h2 id="ShoppingCartTitle" runat="server">Carrinho</h2>
                    </div>

                    <div class="cart-table clearfix">
                        <asp:GridView ID="CartList" runat="server" AutoGenerateColumns="False" GridLines="Vertical" CellPadding="4"
                            ItemType="EComLivro.Models.CartItem" SelectMethod="GetShoppingCartItems"
                            CssClass="table table-striped table-bordered table-responsive">
                            <Columns>
                                <asp:BoundField DataField="livro_id" HeaderText="ID" SortExpression="livro_id" HeaderStyle-Width="15%" />
                                <asp:BoundField DataField="titulo_livro" HeaderText="Título" HeaderStyle-Width="15%"/>
                                <asp:BoundField DataField="valor_venda" HeaderText="Valor Unit." DataFormatString="{0:c}" HeaderStyle-Width="15%"/>
                                <asp:TemplateField HeaderText="Quantidade" HeaderStyle-Width="15%">
                                    <ItemTemplate>
                                        <div class="qty">
                                            <div class="qty-btn d-flex">
                                                <p>Qtde</p>
                                                <div class="quantity">
                                                    <span class="qty-minus" onclick="var effect = document.getElementById('PurchaseQuantity'); var qty = effect.value; if( !isNaN( qty ) &amp;&amp; qty &gt; 1 ) effect.value--;return false;"><i class="fa fa-minus" aria-hidden="true"></i></span>
                                                    <%--<input id="PurchaseQuantity" type="number" class="qty-text" step="1" min="1" max="300" name="quantity" value="<%#: Item.quantidade %>" runat="server">--%>
                                                    <asp:TextBox ID="PurchaseQuantity" Width="40" runat="server" Text="<%#: Item.quantidade %>"></asp:TextBox>
                                                    <span class="qty-plus" onclick="var effect = document.getElementById('PurchaseQuantity'); var qty = effect.value; if( !isNaN( qty )) effect.value++;return false;"><i class="fa fa-plus" aria-hidden="true"></i></span>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total do Item" HeaderStyle-Width="20%">
                                    <ItemTemplate>
                                        <span>
                                            <%#: String.Format("{0:c}", ((Convert.ToDouble(Item.quantidade)) *  Convert.ToDouble(Item.valor_venda)))%>
                                        </span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remover Item" HeaderStyle-Width="20%" >
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Remover" runat="server"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>

                    <asp:Button ID="UpdateBtn" CssClass="btn amado-btn w-100" runat="server" Text="Atualizar Carrinho" OnClick="UpdateBtn_Click" />

                </div>
                <div class="col-12 col-lg-4">
                    <div class="cart-summary" id="CartTotal" runat="server">
                        <h5>Total Carrinho</h5>
                        <ul class="summary-table">
                            <li>
                                <span>
                                    <asp:Label ID="LabelTotalText" runat="server" Text="Subtotal do Pedido: "></asp:Label>
                                </span>
                                <span>
                                    <asp:Label ID="lblTotal" runat="server" EnableViewState="false"></asp:Label>
                                </span>
                            </li>
                            <%--<li><span>delivery:</span> <span>Free</span></li>
                            <li><span>total:</span> <span>$140.00</span></li>--%>
                        </ul>
                        <div class="cart-btn mt-100">
                            <asp:Button ID="CheckoutBtn" CssClass="btn amado-btn w-100" runat="server" OnClick="CheckoutBtn_Click" Text="Checkout"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
