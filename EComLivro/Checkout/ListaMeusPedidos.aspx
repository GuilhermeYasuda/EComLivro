<%@ Page Title="" Language="C#" MasterPageFile="~/Loja/MastePageLoja.Master" AutoEventWireup="true" CodeBehind="ListaMeusPedidos.aspx.cs" Inherits="EComLivro.Checkout.ListaMeusPedidos" %>

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
                    <h3 style="padding-left: 33px">Meus Pedidos:</h3>
                    <asp:GridView ID="OrderList" runat="server" AutoGenerateColumns="False" GridLines="Both" CellPadding="10" Width="500" BorderColor="#efeeef" BorderWidth="33">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                            <asp:BoundField DataField="Status.Nome" HeaderText="Status" />
                            <asp:BoundField DataField="Total" HeaderText="Total" />
                            <asp:BoundField DataField="DataCadastro" HeaderText="Data de Cadastro" />
                            <asp:TemplateField HeaderText="Visualizar" HeaderStyle-Width="20%">
                                <ItemTemplate>
                                    <%--<div class="btn btn-success">
                                    <asp:Button ID="Visualizar" runat="server" CssClass="fas fa-eye"/>
                                        </div>--%>
                                    <a class='btn btn-success' href='CheckoutReview.aspx?idPedido=<%#: Eval("ID") %>' title='Visualizar'>
                                    <div class='fas fa-eye'></div></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>


                    <p></p>
                    <hr />
                    <asp:Button ID="Confirm" runat="server" Text="Continuar" OnClick="Confirm_Click" />

                </div>


            </div>
        </div>
    </div>

</asp:Content>
