<%@ Page Title="" Language="C#" MasterPageFile="~/Loja/MastePageLoja.Master" AutoEventWireup="true" CodeBehind="CheckoutReview.aspx.cs" Inherits="EComLivro.Checkout.CheckoutReview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <div class="cart-table-area section-padding-100">
        <div class="container-fluid">
            <div class="row">
                <div class="col-12 col-lg-8">
                    <div class="cart-title mt-50">
                        <h1>Informações do Pedido</h1>
                        <%--<h2 id="ShoppingCartTitle" runat="server">Carrinho</h2>--%>
                    </div>
                    <h3 style="padding-left: 33px">Livros:</h3>
                    <asp:GridView ID="OrderItemList" runat="server" AutoGenerateColumns="False" GridLines="Both" CellPadding="10" Width="500" BorderColor="#efeeef" BorderWidth="33">
                        <Columns>
                            <asp:BoundField DataField="Livro.ID" HeaderText="ID" SortExpression="Livro.ID" />
                            <asp:BoundField DataField="Livro.Titulo" HeaderText="Título" />
                            <asp:BoundField DataField="ValorUnit" HeaderText="Valor Unit." />
                            <asp:BoundField DataField="Quantidade" HeaderText="Quantidade" />
                        </Columns>
                    </asp:GridView>


                    <asp:DetailsView ID="ShipInfo" runat="server" AutoGenerateRows="false" GridLines="None" CellPadding="10" BorderStyle="None" CommandRowStyle-BorderStyle="None">
                        <Fields>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <h3>Enviar para:</h3>
                                    <br />
                                    <asp:Label ID="Destinatario" runat="server" Text='<%#: Eval("EnderecoEntrega.Destinatario") %>'></asp:Label>
                                    <asp:Label ID="TipoLogradouro" runat="server" Text='<%#: Eval("EnderecoEntrega.TipoLogradouro.Nome") %>'></asp:Label>
                                    <asp:Label ID="Rua" runat="server" Text='<%#: Eval("EnderecoEntrega.Rua") %>'></asp:Label>
                                    <br />
                                    <asp:Label ID="Numero" runat="server" Text='<%#: Eval("EnderecoEntrega.Numero") %>'></asp:Label>
                                    <br />
                                    <asp:Label ID="Bairro" runat="server" Text='<%#: Eval("EnderecoEntrega.Bairro") %>'></asp:Label>
                                    <asp:Label ID="Cidade" runat="server" Text='<%#: Eval("EnderecoEntrega.Cidade.Nome") %>'></asp:Label>
                                    <asp:Label ID="Estado" runat="server" Text='<%#: Eval("EnderecoEntrega.Cidade.Estado.Sigla") %>'></asp:Label>
                                    <asp:Label ID="CEP" runat="server" Text='<%#: Eval("EnderecoEntrega.CEP") %>'></asp:Label>
                                    <asp:Label ID="Observacao" runat="server" Text='<%#: Eval("EnderecoEntrega.Observacao") %>'></asp:Label>
                                    <p></p>
                                    <h3>Order Total:</h3>
                                    <br />
                                    <asp:Label ID="Total" runat="server" Text='<%#: Eval("Total", "{0:C}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                        </Fields>
                    </asp:DetailsView>
                    <p></p>
                    <hr />
                    <asp:Button ID="CheckoutConfirm" runat="server" Text="Complete Order" OnClick="CheckoutConfirm_Click" />

                </div>


            </div>
        </div>
    </div>
</asp:Content>
