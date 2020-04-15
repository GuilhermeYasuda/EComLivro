<%@ Page Title="" Language="C#" MasterPageFile="~/Adm/MasterPageAdm.Master" AutoEventWireup="true" CodeBehind="CadastroCC.aspx.cs" Inherits="EComLivro.Adm.CadastroCC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>ÉComLivro - Cadastro de Cartão de Crédito</title>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">

    <!-- Begin Page Content -->
    <div class="container-fluid">

        <!-- Breadcrumbs-->
        <ol class="breadcrumb">
            <li class="breadcrumb-item">
                <a href="Dashboard.aspx">Dashboard</a>
            </li>
            <li class="breadcrumb-item">
                <a href="ListaCliente.aspx">Lista de Clientes</a>
            </li>
            <li id="idBreadCrumb" runat="server" class="breadcrumb-item active">Cadastro de Cartão de Crédito</li>
        </ol>

        <div class="text-center">
            <h1 id="idTitle" runat="server" class="h4 text-gray-900 mb-4">Cadastro de Cartão de Crédito</h1>
        </div>

        <hr class="sidebar-divider" />
        <div class="sidebar-heading mb-3">
            Dados de Cartão de Crédito
        </div>
        <div class="form-group row">
            <div class="col-sm-6 mb-3 mb-sm-0">
                <asp:Label ID="idLinhaCodigoClientePF" runat="server" Visible="true">ID do Cliente: </asp:Label>
            </div>
            <div class="col-sm-6">
                <asp:TextBox ID="txtIdClientePF" runat="server" CssClass="form-control" Visible="true"></asp:TextBox>
            </div>
        </div>

        <div class="form-group row">
            <div class="col-sm-6 mb-3 mb-sm-0">
                <asp:Label ID="idLinhaCodigo" runat="server" Visible="false">ID do Cartão de Crédito: </asp:Label>
            </div>
            <div class="col-sm-6">
                <asp:TextBox ID="txtIdCC" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
            </div>
        </div>
        
        <div class="form-group">
            <asp:TextBox ID="txtNomeImpresso" runat="server" CssClass="form-control form-control-user" Placeholder="Nome Impresso no Cartão"></asp:TextBox>
        </div>
        <div class="form-group row">
            <div class="col-sm-6 mb-3 mb-sm-0">
                <asp:TextBox ID="txtNumeroCC" runat="server" CssClass="form-control form-control-user" Placeholder="Número do Cartão"></asp:TextBox>
            </div>
            <div class="col-sm-6 mb-3 mb-sm-0">
                <div class="input-group-append">
                    <asp:TextBox ID="txtCodigoSeguranca" runat="server" CssClass="form-control form-control-user" Placeholder="Código de Segurança"></asp:TextBox>
                </div>
            </div>
        </div>

        <div class="form-group">
            <asp:DropDownList ID="dropIdBandeira" type="text" CssClass="form-control form-control-user" runat="server" Enabled="true"></asp:DropDownList>
        </div>

        <div class="form-group row">
            <div class="col-sm-4 mb-3 mb-sm-0">
                <asp:Button runat="server" Text="Cancelar" Visible="true" ID="btnCancelar" CssClass="btn btn-danger btn-user btn-block" OnClick="btnCancelar_Click" />
            </div>
            <div class="col-sm-4 mb-3 mb-sm-0">
                <asp:Button runat="server" Text="Resetar" Visible="true" ID="btnResetar" CssClass="btn btn-warning btn-user btn-block" OnClick="btnResetar_Click" />
            </div>
            <div class="col-sm-4">
                <asp:Button runat="server" Text="Cadastrar" Visible="true" ID="btnCadastrar" CssClass="btn btn-success btn-user btn-block" OnClick="btnCadastrar_Click" />
                <asp:Button runat="server" Text="Alterar" Visible="false" ID="btnAlterar" CssClass="btn btn-success btn-user btn-block" OnClick="btnAlterar_Click" />
            </div>
        </div>
        <asp:Label ID="lblResultado" CssClass="text-danger" runat="server" Visible="false"></asp:Label>
    </div>
</asp:Content>
