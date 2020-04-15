<%@ Page Title="" Language="C#" MasterPageFile="~/Loja/MastePageLoja.Master" AutoEventWireup="true" CodeBehind="TrocaPedido.aspx.cs" Inherits="EComLivro.Checkout.TrocaPedido" %>

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
                            <asp:TemplateField HeaderText="Trocar" HeaderStyle-Width="20%">
                                <ItemTemplate>
                                    <%--<div class="btn btn-success">
                                    <asp:Button ID="Visualizar" runat="server" CssClass="fas fa-eye"/>
                                        </div>--%>
                                    <a class='btn btn-warning' href='GerarPedidoTroca.aspx?idPedido=<%#: Eval("IdPedido") %>&idLivro=<%#: Eval("Livro.ID") %>' title='Trocar Livro'>
                                        <div class='fas fa-undo'></div>
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                    <br />
                    
                    <div class="form-group row mb-3">
                        <h3>Trocar Pedido Todo: &nbsp;</h3>
                        <a id="TrocaPedidoTodo" runat="server" class='btn btn-warning' title='Trocar'>
                            <div class='fas fa-undo'></div>
                        </a>
                    </div>
                    
                    <p></p>
                    <hr />
                    
                    <asp:Button ID="CheckoutConfirm" runat="server" Text="Continuar" OnClick="CheckoutConfirm_Click" />

                </div>


            </div>
        </div>
    </div>
</asp:Content>
