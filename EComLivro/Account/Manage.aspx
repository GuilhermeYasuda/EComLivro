<%@ Page Title="Gerenciar conta" Language="C#" MasterPageFile="~/Loja/MastePageLoja.Master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="EComLivro.Account.Manage" %>

<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="cart-table-area section-padding-100">
        <div class="container-fluid">
            <div class="row">
                <div class="col-12 col-lg-8">

                    <div class="cart-title mt-50">
                        <h2><%: Title %>.</h2>
                    </div>

                    <div>
                        <asp:PlaceHolder runat="server" ID="successMessage" Visible="false" ViewStateMode="Disabled">
                            <p class="text-success"><%: SuccessMessage %></p>
                        </asp:PlaceHolder>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-horizontal">
                                <h4>Alterar as configurações de conta</h4>
                                <hr />
                                <dl class="dl-horizontal">
                                    <dt>Senha:</dt>
                                    <dd>
                                        <asp:HyperLink NavigateUrl="/Account/ManagePassword" Text="[Alterar]" Visible="false" ID="ChangePassword" runat="server" />
                                        <asp:HyperLink NavigateUrl="/Account/ManagePassword" Text="[Criar]" Visible="false" ID="CreatePassword" runat="server" />
                                    </dd>
                                    <dt>Dados Cadastrais:</dt>
                                    <dd>
                                        <asp:HyperLink NavigateUrl="/Account/ManageAccount" Text="[Alterar]" Visible="true" ID="AlterarDadosCadastrais" runat="server" />
                                    </dd>
                                    <dt>Endereços:</dt>
                                    <dd>
                                        <asp:HyperLink NavigateUrl="/Account/ManageAccount" Text="[Gerenciar]" Visible="true" ID="GerenciarEnderecos" runat="server" />
                                    </dd>
                                    <dt>Cartões de Crédito:</dt>
                                    <dd>
                                        <asp:HyperLink NavigateUrl="/Account/ManageAccount" Text="[Gerenciar]" Visible="true" ID="GerenciarCartoes" runat="server" />
                                    </dd>
                                    <%--<dt>Logins externos:</dt>
                                    <dd><%: LoginsCount %>
                                        <asp:HyperLink NavigateUrl="/Account/ManageLogins" Text="[Manage]" runat="server" />

                                    </dd>--%>
                                    
                                </dl>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
