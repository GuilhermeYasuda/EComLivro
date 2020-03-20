<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageAdm.Master" AutoEventWireup="true" CodeBehind="Analise.aspx.cs" Inherits="EComLivro.Analise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>ÉComLivro - Análise</title>

    <script>
        // Area Chart Example
        document.addEventListener('DOMContentLoaded', function () {
            var myChart = Highcharts.chart('container', {

                title: {
                    text: 'Venda de Livros por Categoria'
                },

                subtitle: {
                    text: 'Fonte: ÉComLivro'
                },

                yAxis: {
                    title: {
                        text: 'Número de Vendas'
                    }
                },

                xAxis: {
                    accessibility: {
                        rangeDescription: 'Range: 2010 to 2017'
                    }
                },

                legend: {
                    layout: 'vertical',
                    align: 'right',
                    verticalAlign: 'middle'
                },

                plotOptions: {
                    series: {
                        label: {
                            connectorAllowed: false
                        },
                        pointStart: 2010
                    }
                },

                series: [{
                    name: 'Categoria 1',
                    data: [500, 1500, 1000, 2000, 1500, 2500, 1000, 1500]
                }, {
                    name: 'Categoria 2',
                    data: [1500, 1000, 2500, 1500, 2000, 1000, 1500, 500]
                }, {
                    name: 'Categoria 3',
                    data: [1000, 1000, 1000, 1000, 1000, 1000, 1000, 1000]
                }, {
                    name: 'Categoria 4',
                    data: [null, null, 500, 1000, 1500, 2000, 2500, 3000]
                }, {
                    name: 'Categoria 5',
                    data: [null, 5, 10, 30, 90, 270, 810, 2430]
                }],

                responsive: {
                    rules: [{
                        condition: {
                            maxWidth: 500
                        },
                        chartOptions: {
                            legend: {
                                layout: 'horizontal',
                                align: 'center',
                                verticalAlign: 'bottom'
                            }
                        }
                    }]
                }

            });
        });

    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">

    <!-- Begin Page Content -->
    <div class="container-fluid">

        <!-- Page Heading -->
        <h1 class="h3 mb-2 text-gray-800">Análise</h1>

        <!-- Content Row -->
        <div class="row">

            <div class="col-xl-12 col-lg-12">

                <!-- Area Chart -->
                <div class="card shadow mb-4">
                    <div class="card-header py-3">
                        <%--                        <h6 class="m-0 font-weight-bold text-primary">Area Chart</h6>--%>
                        <h6 class="m-0 font-weight-bold text-primary">Gráfico de Vendas por Categoria</h6>
                    </div>
                    <div class="card-body">

                        <form class="user" runat="server">
                            <div class="form-group row">
                                <div class="col-sm-5 mb-3 mb-sm-0">
                                    <div class="input-group-append">
                                        <asp:Label ID="lblDtInicio" runat="server" Text="Data Início:"></asp:Label>
                                        <asp:TextBox ID="txtDtInicio" type="date" runat="server" CssClass="form-control form-control-user" Placeholder="Data de Nascimento"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-5 mb-3 mb-sm-0">
                                    <div class="input-group-append">
                                        <asp:Label ID="lblDtFim" runat="server" Text="Data Fim:"></asp:Label>
                                        <asp:TextBox ID="txtDtFim" type="date" runat="server" CssClass="form-control form-control-user" Placeholder="Data de Nascimento"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="input-group-append">
<%--                                        <a href="#" class="btn btn-primary btn-circle">
                                            <i class="fas fa-fw fa-redo-alt"></i>
                                        </a>--%>
                                        <asp:LinkButton ID="btnFiltraGrafico" CssClass="btn btn-primary btn-circle" runat="server">
                                            <i class="fas fa-fw fa-redo-alt"></i>
                                        </asp:LinkButton>
                                        <%--<asp:Button ID="btnFiltraGrafico" CssClass="btn btn-primary btn-circle" runat="server" />--%>
                                    </div>
                                </div>
                            </div>
                        </form>
                        <%--<div class="chart-area">
                            <canvas id="myAreaChart"></canvas>
                        </div>
                        <hr>
                        Styling for the area chart can be found in the <code>/js/demo/chart-area-demo.js</code> file.
                        --%>
                        <figure class="highcharts-figure">
                            <div id="container"></div>
                            <%--<p class="highcharts-description">
                                Basic line chart showing trends in a dataset. This chart includes the
                                <code>series-label</code> module, which adds a label to each line for
                                enhanced readability.
                            </p>--%>
                        </figure>
                    </div>
                </div>

                <!-- Bar Chart -->
                <%--<div class="card shadow mb-4">
                    <div class="card-header py-3">
                        <h6 class="m-0 font-weight-bold text-primary">Bar Chart</h6>
                    </div>
                    <div class="card-body">
                        <div class="chart-bar">
                            <canvas id="myBarChart"></canvas>
                        </div>
                        <hr>
                        Styling for the bar chart can be found in the <code>/js/demo/chart-bar-demo.js</code> file.
               
                    </div>
                </div>--%>
            </div>

            <!-- Donut Chart -->
            <%--<div class="col-xl-4 col-lg-5">
                <div class="card shadow mb-4">
                    <!-- Card Header - Dropdown -->
                    <div class="card-header py-3">
                        <h6 class="m-0 font-weight-bold text-primary">Donut Chart</h6>
                    </div>
                    <!-- Card Body -->
                    <div class="card-body">
                        <div class="chart-pie pt-4">
                            <canvas id="myPieChart"></canvas>
                        </div>
                        <hr>
                        Styling for the donut chart can be found in the <code>/js/demo/chart-pie-demo.js</code> file.
               
                    </div>
                </div>
            </div>--%>
        </div>

    </div>
    <!-- /.container-fluid -->

</asp:Content>
