<%@ Page Title="" Language="C#" MasterPageFile="~/Loja/MastePageLoja.Master" AutoEventWireup="true" CodeBehind="CheckoutComplete.aspx.cs" Inherits="EComLivro.Checkout.CheckoutComplete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="cart-table-area section-padding-100">
        <div class="container-fluid">
            <div class="row">
                <div class="col-12 col-lg-8">
                    <div class="cart-title mt-50">
                        <h1>Checkout Completo!</h1>
                        <%--<h2 id="ShoppingCartTitle" runat="server">Carrinho</h2>--%>
                    </div>
                    <p></p>
                    <div class="row">
                    <h3>ID do Pedido:</h3>
                    <asp:Label ID="OrderId" runat="server"></asp:Label>
                    <p></p></div>
                    <h3>Obrigado!</h3>
                    <p></p>
                    <hr />
                    <asp:Button ID="Continue" runat="server" Text="Continue Shopping" OnClick="Continue_Click" />

                </div>

            </div>

        </div>

    </div>
</asp:Content>
