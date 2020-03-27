<%@ Page Title="" Language="C#" MasterPageFile="~/Adm/MasterPageAdm.Master" AutoEventWireup="true" CodeBehind="ListaPedidos.aspx.cs" Inherits="EComLivro.Adm.ListaPedidos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>ÉComLivro - Lista de Pedidos</title>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">

    <!-- Begin Page Content -->
    <div class="container-fluid">

        <!-- Page Heading -->
        <h1 class="h3 mb-2 text-gray-800">Pedidos</h1>
        <%--          <p class="mb-4">DataTables is a third party plugin that is used to generate the demo table below. For more information about DataTables, please visit the <a target="_blank" href="https://datatables.net">official DataTables documentation</a>.</p>--%>

        <!-- DataTales Example -->
        <div class="card shadow mb-4">
            <div class="card-header py-3">
                <h6 class="m-0 font-weight-bold text-primary">Lista de Pedidos</h6>
            </div>
            <div class="card-body">
                <div class="table-responsive">
                    <table class="table table-bordered" id="dataTable" width="100%" cellspacing="0">
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Quantidade de Itens</th>
                                <th>Método de Pagamento</th>
                                <th>Método de Envio</th>
                                <th>Cadastro</th>
                                <th>Status</th>
                                <th>Operações</th>
                            </tr>
                        </thead>
                        <tfoot>
                            <tr>
                                <th>ID</th>
                                <th>Quantidade de Itens</th>
                                <th>Método de Pagamento</th>
                                <th>Método de Envio</th>
                                <th>Cadastro</th>
                                <th>Status</th>
                                <th>Operações</th>
                            </tr>
                        </tfoot>
                        <tbody>
                            <tr>
                                <td>1</td>
                                <td>1</td>
                                <td>Cartão de crédito</td>
                                <td>PAC</td>
                                <td>20/02/2020</td>
                                <td>EM TRANSPORTE</td>
                                <td style='text-align-last: center;'>
                                    <a class="btn btn-info" href="CadastroLivro.aspx" title="Vizualizar">
                                        <div class="fas fa-eye"></div>
                                    </a>
                                    <a class="btn btn-success" href="ListaLivro.aspx" title="Ok">
                                        <div class="fas fa-check"></div>
                                    </a>
                                </td>
                            </tr>
                            <tr>
                                <td>2</td>
                                <td>2</td>
                                <td>Cartão de crédito</td>
                                <td>SEDEX</td>
                                <td>20/02/2020</td>
                                <td>EM PROCESSAMENTO</td>
                                <td style='text-align-last: center;'>
                                    <a class="btn btn-info" href="CadastroLivro.aspx" title="Vizualizar">
                                        <div class="fas fa-eye"></div>
                                    </a>
                                    <a class="btn btn-success" href="ListaLivro.aspx" title="Ok">
                                        <div class="fas fa-check"></div>
                                    </a>
                                </td>
                            </tr>
                            <tr>
                                <td>3</td>
                                <td>4</td>
                                <td>Cartão de crédito
                                    CUPOM
                                </td>
                                <td>PAC</td>
                                <td>20/02/2020</td>
                                <td>ENTREGUE</td>
                                <td style='text-align-last: center;'>
                                    <a class="btn btn-info" href="CadastroLivro.aspx" title="Vizualizar">
                                        <div class="fas fa-eye"></div>
                                    </a>
                                    <a class="btn btn-success" href="ListaLivro.aspx" title="Ok">
                                        <div class="fas fa-check"></div>
                                    </a>
                                </td>
                            </tr>
                            <tr>
                                <td>4</td>
                                <td>4</td>
                                <td>CUPOM</td>
                                <td>SEDEX</td>
                                <td>20/02/2020</td>
                                <td>EM TRANSPORTE</td>
                                <td style='text-align-last: center;'>
                                    <a class="btn btn-info" href="CadastroLivro.aspx" title="Vizualizar">
                                        <div class="fas fa-eye"></div>
                                    </a>
                                    <a class="btn btn-success" href="ListaLivro.aspx" title="Ok">
                                        <div class="fas fa-check"></div>
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
