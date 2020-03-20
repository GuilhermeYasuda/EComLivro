<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageAdm.Master" AutoEventWireup="true" CodeBehind="ListaLivro.aspx.cs" Inherits="EComLivro.ListaLivro" %>

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
        <h1 class="h3 mb-2 text-gray-800">Lista de Clientes</h1>

        <!-- DataTales Example -->
        <div class="card shadow mb-4">
            <div class="card-header py-3">
                <h6 class="m-0 font-weight-bold text-primary">Lista de Livros
                
                    <!-- Botão para adição de produtor -->
                    <a class="btn btn-primary fa-pull-right" href="CadastroLivro.aspx" title="Novo Livro">
                        <div class="fas fa-fw fa-plus"></div>
                    </a>

                </h6>
            </div>
            <div class="card-body">
                <div class="table-responsive">
                    <table class="table table-bordered" id="dataTable" width="100%" cellspacing="0">
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Título</th>
                                <th>Autor</th>
                                <th>Categoria</th>
                                <th>Nº Paginas</th>
                                <th>Sinópse</th>
                                <th>ISBN</th>
                                <th>Cód. Barras</th>
                                <th>Editora</th>
                                <th>Edição</th>
                                <th>Ano</th>
                                <th>Altura</th>
                                <th>Largura</th>
                                <th>Profundidade</th>
                                <th>Peso</th>
                                <th>Grupo de Precificação</th>
                                <th>Operações</th>
                            </tr>
                        </thead>
                        <tfoot>
                            <tr>
                                <th>ID</th>
                                <th>Título</th>
                                <th>Autor</th>
                                <th>Categoria</th>
                                <th>Nº Paginas</th>
                                <th>Sinópse</th>
                                <th>ISBN</th>
                                <th>Cód. Barras</th>
                                <th>Editora</th>
                                <th>Edição</th>
                                <th>Ano</th>
                                <th>Altura</th>
                                <th>Largura</th>
                                <th>Profundidade</th>
                                <th>Peso</th>
                                <th>Grupo de Precificação</th>
                                <th>Operações</th>
                            </tr>
                        </tfoot>
                        <tbody>
                            <tr>
                                <td>1</td>
                                <td>Título 1</td>
                                <td>Autor 1</td>
                                <td>Categoria 1</td>
                                <td>123</td>
                                <td>Sinópse de um livro Título 1</td>
                                <td>1234567890123</td>
                                <td>1234567890123</td>
                                <td>Editora 1</td>
                                <td>1</td>
                                <td>1111</td>
                                <td>11.1</td>
                                <td>11.1</td>
                                <td>11.1</td>
                                <td>11.11</td>
                                <td>Grupo de Precificação 1</td>
                                <td style='text-align-last: center;'>
                                    <a class="btn btn-warning" href="CadastroLivro.aspx" title="Editar">
                                        <div class="fas fa-edit"></div>
                                    </a>
                                    <a class="btn btn-danger" href="ListaLivro.aspx" title="Apagar">
                                        <div class="fas fa-trash-alt"></div>
                                    </a>
                                </td>
                            </tr>
                            <tr>
                                <td>2</td>
                                <td>Título 2</td>
                                <td>Autor 1
                                    Autor 2
                                </td>
                                <td>Categoria 1
                                    Categoria 2
                                </td>
                                <td>321</td>
                                <td>Sinópse de um livro Título 2</td>
                                <td>9876543210987</td>
                                <td>9876543210987</td>
                                <td>Editora 2</td>
                                <td>2</td>
                                <td>2222</td>
                                <td>22.2</td>
                                <td>22.2</td>
                                <td>22.2</td>
                                <td>22.22</td>
                                <td>Grupo de Precificação 2</td>
                                <td style='text-align-last: center;'>
                                    <a class="btn btn-warning" href="CadastroLivro.aspx" title="Editar">
                                        <div class="fas fa-edit"></div>
                                    </a>
                                    <a class="btn btn-danger" href="ListaLivro.aspx" title="Apagar">
                                        <div class="fas fa-trash-alt"></div>
                                    </a>
                                </td>
                            </tr>
                            <tr>
                                <td>3</td>
                                <td>Título 3</td>
                                <td>Autor 1
Autor 2
Autor 3
                                </td>
                                <td>Categoria 1
Categoria 2
Categoria 3

                                </td>
                                <td>456</td>
                                <td>Sinópse de um livro Título 3</td>
                                <td>3216549870321</td>
                                <td>3216549870321</td>
                                <td>Editora 3</td>
                                <td>3</td>
                                <td>3333</td>
                                <td>33.3</td>
                                <td>33.3</td>
                                <td>33.3</td>
                                <td>33.33</td>
                                <td>Grupo de Precificação 3</td>
                                <td style='text-align-last: center;'>
                                    <a class="btn btn-warning" href="CadastroLivro.aspx" title="Editar">
                                        <div class="fas fa-edit"></div>
                                    </a>
                                    <a class="btn btn-danger" href="ListaLivro.aspx" title="Apagar">
                                        <div class="fas fa-trash-alt"></div>
                                    </a>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>

    </div>
    <!-- /.container-fluid -->

</asp:Content>
