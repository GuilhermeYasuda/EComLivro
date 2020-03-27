<%@ Page Title="" Language="C#" MasterPageFile="~/Adm/MasterPageAdm.Master" AutoEventWireup="true" CodeBehind="ListaLivro.aspx.cs" Inherits="EComLivro.Adm.ListaLivro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>ÉComLivro - Lista de Livros</title>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">

    <!-- Begin Page Content -->
    <div class="container-fluid">

        <!-- Breadcrumbs-->
        <ol class="breadcrumb">
            <li class="breadcrumb-item">
                <a href="Dashboard.aspx">Dashboard</a>
            </li>
            <li class="breadcrumb-item active">Lista de Livros</li>
        </ol>

        <!-- Page Heading -->
        <h1 class="h3 mb-2 text-gray-800">Lista de Livros</h1>

        <!-- DataTales Example -->
        <div class="card shadow mb-4">
            <div class="card-header py-3">
                <h6 class="m-0 font-weight-bold text-primary">Lista de Livros
                    
                    <!-- Só é feita a consulta e alteração do status do livro -->
                    <%-- <!-- Botão para adição de livro -->
                    <a class="btn btn-primary fa-pull-right" href="CadastroLivro.aspx" title="Novo Livro">
                        <div class="fas fa-fw fa-plus"></div>
                    </a>--%>

                </h6>
            </div>
            <div class="card-body">
                <div class="table-responsive">
                    <asp:Panel runat="server" GroupingText="Filtro" >
                        Ativo/Inativo: <asp:DropDownList AutoPostBack="true" ID="dropAtivo" DataTextField="Name" DataValueField="ID" CssClass="form-control" runat="server" OnSelectedIndexChanged="dropAtivo_SelectedIndexChanged"></asp:DropDownList>
                        Categoria Ativação/Inativação: <asp:DropDownList AutoPostBack="true" ID="dropIdCategoriaMotivo" DataTextField="Name" DataValueField="ID" CssClass="form-control" runat="server" OnSelectedIndexChanged="dropIdCategoriaMotivo_SelectedIndexChanged" Enabled="false"></asp:DropDownList>
                    </asp:Panel>
                    
                    <br />
                    <div id="divTable" class="table table-bordered" runat="server" >
                        
                    </div>
                    <asp:GridView runat="server" CssClass="display" ID="GridViewGeral" EnableModelValidation="True" Width="204px" >
                        <HeaderStyle Font-Bold="true" />
                    </asp:GridView >

                </div>
                <asp:Label id="lblResultado" runat="server" Visible="false"></asp:Label>
            </div>
            
            <div class="card-footer small text-muted" id="lblRodaPeTabela" runat="server"></div>
        </div>

    </div>
    <!-- /.container-fluid -->

</asp:Content>
