<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageAdm.Master" AutoEventWireup="true" CodeBehind="~/Views/Cliente/ListaCliente.aspx.cs" Inherits="EComLivro.Views.Cliente.ListaCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>ÉComLivro - Lista de Clientes</title>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">

    <!-- Begin Page Content -->
    <div class="container-fluid">

        <!-- Breadcrumbs-->
        <ol class="breadcrumb">
            <li class="breadcrumb-item">
                <a href="Dashboard.aspx">Dashboard</a>
            </li>
            <li class="breadcrumb-item active">Lista de Clientes</li>
        </ol>

        <!-- Page Heading -->
        <h1 class="h3 mb-2 text-gray-800">Lista de Clientes</h1>

        <!-- DataTales Example -->
        <div class="card shadow mb-4">
            <div class="card-header py-3">
                <h6 class="m-0 font-weight-bold text-primary">Lista de Clientes
                
                    <!-- Botão para adição de produtor -->
                    <a class="btn btn-primary fa-pull-right" href="CadastroCliente.aspx" title="Novo Cliente">
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
                                <th>Nome</th>
                                <th>Sobrenome</th>
                                <th>CPF</th>
                                <th>Data de Nascimento</th>
                                <th>Telefone</th>
                                <th>E-mail</th>
                                <th>Operações</th>
                            </tr>
                        </thead>
                        <tfoot>
                            <tr>
                                <th>ID</th>
                                <th>Nome</th>
                                <th>Sobrenome</th>
                                <th>CPF</th>
                                <th>Data de Nascimento</th>
                                <th>Telefone</th>
                                <th>E-mail</th>
                                <th>Operações</th>
                            </tr>
                        </tfoot>
                        <tbody>
                            <tr>
                                <td>1</td>
                                <td>Fulano</td>
                                <td>Silva</td>
                                <td>123.456.789-01</td>
                                <td>01/01/1990</td>
                                <td>(00)0000-0000</td>
                                <td>fulano.silva@qualquerprovedor.com.br</td>
                                <td style='text-align-last: center;'>
                                    <a class="btn btn-warning" href="CadastroCliente.aspx" title="Editar">
                                        <div class="fas fa-edit"></div>
                                    </a>
                                    <a class="btn btn-danger" href="ListaCliente.aspx" title="Apagar">
                                        <div class="fas fa-trash-alt"></div>
                                    </a>
                                </td>
                            </tr>
                            <tr>
                                <td>2</td>
                                <td>Sem Vergonha</td>
                                <td>Na Cara</td>
                                <td>321.654.987-10</td>
                                <td>01/01/1990</td>
                                <td>(00)0000-0000</td>
                                <td>sem.cara@qualquerprovedor.com.br</td>
                                <td style='text-align-last: center;'>
                                    <a class="btn btn-warning" href="CadastroCliente.aspx" title="Editar">
                                        <div class="fas fa-edit"></div>
                                    </a>
                                    <a class="btn btn-danger" href="ListaCliente.aspx" title="Apagar">
                                        <div class="fas fa-trash-alt"></div>
                                    </a>
                                </td>
                            </tr>
                            <tr>
                                <td>3</td>
                                <td>Cicrano</td>
                                <td>Santos</td>
                                <td>987.654.321-09</td>
                                <td>01/01/1990</td>
                                <td>(00)0000-0000</td>
                                <td>cicrano.santos@qualquerprovedor.com.br</td>
                                <td style='text-align-last: center;'>
                                    <a class="btn btn-warning" href="CadastroCliente.aspx" title="Editar">
                                        <div class="fas fa-edit"></div>
                                    </a>
                                    <a class="btn btn-danger" href="ListaCliente.aspx" title="Apagar">
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
