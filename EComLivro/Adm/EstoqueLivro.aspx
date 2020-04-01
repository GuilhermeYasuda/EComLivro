<%@ Page Title="" Language="C#" MasterPageFile="~/Adm/MasterPageAdm.Master" AutoEventWireup="true" CodeBehind="EstoqueLivro.aspx.cs" Inherits="EComLivro.Adm.EstoqueLivro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>ÉComLivro - Entrada Estoque</title>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">


    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <!-- Begin Page Content -->
    <div class="container-fluid">

        <!-- Breadcrumbs-->
        <ol class="breadcrumb">
            <li class="breadcrumb-item">
                <a href="Dashboard.aspx">Dashboard</a>
            </li>
            <li class="breadcrumb-item">
                <a href="ListaEstoque.aspx">Lista de Livros</a>
            </li>
            <li class="breadcrumb-item active">Entrada no Estoque</li>
        </ol>

        <div class="text-center">
            <h1 class="h4 text-gray-900 mb-4">Entrada no Estoque</h1>
        </div>

        <hr class="sidebar-divider" />
        <div class="sidebar-heading mb-3">
            Dados para Entrada no Estoque
        </div>
        <div class="form-group row">
            <div class="col-sm-6 mb-3 mb-sm-0">
                <asp:Label ID="idLivro" runat="server" Visible="true">ID do Livro: </asp:Label>
            </div>
            <div class="col-sm-6">
                <asp:TextBox ID="txtIdLivro" runat="server" CssClass="form-control" Visible="true"></asp:TextBox>
            </div>
        </div>
        
        <div class="form-group row">
            <div class="col-sm-6 mb-3 mb-sm-0">
                <asp:TextBox ID="txtQtde" type="number" runat="server" CssClass="form-control form-control-user" Placeholder="Quantidade"></asp:TextBox>
            </div>
            <div class="col-sm-6">
                <asp:TextBox ID="txtCusto" type="number" step="0.01" runat="server" CssClass="form-control form-control-user" Placeholder="Custo Unitário"></asp:TextBox>
            </div>
        </div>

        <div class="form-group">
            <asp:DropDownList ID="dropIdFornecedor" type="text" CssClass="form-control form-control-user" runat="server" Enabled="true"></asp:DropDownList>
        </div>

        <div class="form-group row">
            <div class="col-sm-4 mb-3 mb-sm-0">
                <asp:Button runat="server" Text="Cancelar" Visible="true" ID="btnCancelar" CssClass="btn btn-danger btn-user btn-block" OnClick="btnCancelar_Click" />
            </div>
            <div class="col-sm-4 mb-3 mb-sm-0">
                <asp:Button runat="server" Text="Resetar" Visible="true" ID="btnResetar" CssClass="btn btn-warning btn-user btn-block" OnClick="btnResetar_Click" />
            </div>
            <div class="col-sm-4">
                <asp:Button runat="server" Text="Dar Entrada" Visible="true" ID="btnDarEntrada" CssClass="btn btn-success btn-user btn-block" OnClick="btnDarEntrada_Click" />
            </div>
        </div>
        <asp:Label ID="lblResultado" CssClass="text-danger" runat="server" Visible="false"></asp:Label>
    </div>
</asp:Content>
