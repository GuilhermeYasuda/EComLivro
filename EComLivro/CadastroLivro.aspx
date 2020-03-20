<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageAdm.Master" AutoEventWireup="true" CodeBehind="CadastroLivro.aspx.cs" Inherits="EComLivro.CadastroLivro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>ÉComLivro - Cadastro de Livro</title>

    <script>
        $(document).ready(function () {
            $('#btnAddAutor').click(function () {
                $('#idAutor').clone(true).appendTo('#formsAutor');
            })
        })
    </script>

    <script>
        $(document).ready(function () {
            $('#btnAddCategoria').click(function () {
                $('#idCategoria').clone(true).appendTo('#formsCategoria');
            })
        })
    </script>

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
                <a href="ListaLivro.aspx">Lista de Livros</a>
            </li>
            <li class="breadcrumb-item active">Cadastro de Livro</li>
        </ol>

        <div class="text-center">
            <h1 class="h4 text-gray-900 mb-4">Cadastro de Livro</h1>
        </div>
        <form class="user" runat="server">

            <hr class="sidebar-divider" />
            <div class="sidebar-heading mb-3">
                Autor

                <!-- Botão para adição de autor -->
                <span id="btnAddAutor" class="btn btn-primary fa-pull-right btn-sm" style="cursor: pointer;">
                    <i class="fas fa-fw fa-plus"></i>
                </span>
            </div>

            <%-- div necessária para fazer a clonagem dos campos de endereço --%>
            <div id="idAutor">
                <div class="form-group">
                    <asp:DropDownList AutoPostBack="true" ID="dropIdAutor" CssClass="form-control form-control-user" runat="server" Enabled="true">
                        <asp:ListItem Text="Autor" Value="0" Selected="True" />
                        <asp:ListItem Text="Autor 1" Value="1" />
                        <asp:ListItem Text="Autor 2" Value="2" />
                        <asp:ListItem Text="Autor 3" Value="3" />
                    </asp:DropDownList>
                </div>
            </div>

            <div id="formsAutor">
            </div>

            <hr class="sidebar-divider" />
            <div class="sidebar-heading mb-3">
                Categoria

                <!-- Botão para adição de autor -->
                <span id="btnAddCategoria" class="btn btn-primary fa-pull-right btn-sm" style="cursor: pointer;">
                    <i class="fas fa-fw fa-plus"></i>
                </span>
            </div>

            <%-- div necessária para fazer a clonagem dos campos de endereço --%>
            <div id="idCategoria">
                <div class="form-group">
                    <asp:DropDownList AutoPostBack="true" ID="dropIdCategoria" CssClass="form-control form-control-user" runat="server" Enabled="true">
                        <asp:ListItem Text="Categoria" Value="0" Selected="True" />
                        <asp:ListItem Text="Categoria 1" Value="1" />
                        <asp:ListItem Text="Categoria 2" Value="2" />
                        <asp:ListItem Text="Categoria 3" Value="3" />
                    </asp:DropDownList>
                </div>
            </div>

            <div id="formsCategoria">
            </div>

            <hr class="sidebar-divider" />
            <div class="sidebar-heading mb-3">Dados Pessoais</div>

            <div class="form-group">
                <asp:TextBox ID="txtTitulo" runat="server" CssClass="form-control form-control-user" Placeholder="Título"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:TextBox ID="txtNumeroPaginas" runat="server" CssClass="form-control form-control-user" Placeholder="Número de Páginas"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:TextBox ID="txtSinopse" runat="server" CssClass="form-control form-control-user" Placeholder="Sinópse"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:TextBox ID="txtISBN" runat="server" CssClass="form-control form-control-user" Placeholder="ISBN"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:TextBox ID="txtCodigoBarras" runat="server" CssClass="form-control form-control-user" Placeholder="Código de Barras"></asp:TextBox>
            </div>
            <div class="form-group row">
                <div class="col-sm-4 mb-3 mb-sm-0">
                    <asp:DropDownList AutoPostBack="true" ID="dropIdEditora" CssClass="form-control form-control-user" runat="server" Enabled="true">
                        <asp:ListItem Text="Editora" Value="0" Selected="True" />
                        <asp:ListItem Text="Editora 1" Value="1" />
                        <asp:ListItem Text="Editora 2" Value="2" />
                        <asp:ListItem Text="Editora 3" Value="3" />
                    </asp:DropDownList>
                </div>
                <div class="col-sm-4 mb-3 mb-sm-0">
                    <div class="input-group-append">
                        <asp:TextBox ID="txtEdição" runat="server" CssClass="form-control form-control-user" Placeholder="Edição"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-4">
                    <div class="input-group-append">
                        <asp:TextBox ID="txtAno" runat="server" CssClass="form-control form-control-user" Placeholder="Ano da edição"></asp:TextBox>
                    </div>
                </div>
            </div>

            <hr class="sidebar-divider" />
            <div class="sidebar-heading mb-3">
                Dimensões
            </div>

            <div class="form-group row">
                <div class="col-sm-3 mb-3 mb-sm-0">
                    <div class="input-group-append">
                        <asp:TextBox ID="txtAltura" runat="server" CssClass="form-control form-control-user" Placeholder="Altura"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-3 mb-3 mb-sm-0">
                    <div class="input-group-append">
                        <asp:TextBox ID="txtLargura" runat="server" CssClass="form-control form-control-user" Placeholder="Largura"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-3 mb-3 mb-sm-0">
                    <div class="input-group-append">
                        <asp:TextBox ID="txtProfundidade" runat="server" CssClass="form-control form-control-user" Placeholder="Profundidade"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-3">
                    <div class="input-group-append">
                        <asp:TextBox ID="txtPeso" runat="server" CssClass="form-control form-control-user" Placeholder="Peso"></asp:TextBox>
                    </div>
                </div>
            </div>


            <hr class="sidebar-divider" />
            <div class="sidebar-heading mb-3">
                Grupo de Precificação
            </div>

            <div class="form-group">
                <asp:DropDownList AutoPostBack="true" ID="dropIdGrupoPrecificacao" CssClass="form-control form-control-user" runat="server" Enabled="true">
                    <asp:ListItem Text="Grupo de Precificação" Value="0" Selected="True" />
                    <asp:ListItem Text="Grupo de Precificação 1" Value="1" />
                    <asp:ListItem Text="Grupo de Precificação 2" Value="2" />
                    <asp:ListItem Text="Grupo de Precificação 3" Value="3" />
                </asp:DropDownList>
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
        </form>
    </div>
</asp:Content>
