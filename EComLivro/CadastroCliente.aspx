<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageAdm.Master" AutoEventWireup="true" CodeBehind="~/CadastroCliente.aspx.cs" Inherits="EComLivro.CadastroCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>ÉComLivro - Cadastro de Clientes</title>

    <%-- Script para clonagem dos campos para endereço --%>
    <%--<script>
        //$(document).ready(function () {
        //    $('#btnAddEndereco').click(function () {
        //        $('#idEnderecoCompleto').clone(true).appendTo('#formsEndereco');
        //    })
        //})
    </script>--%>

    <%--<script>
        // clonagem ocorrendo corretamente, mas na hora da atribuição dando erro, 
        // por isso será tirado e outra abordagem será feita
        //$(document).ready(function () {
        //    $('#btnAddEndereco').click(function () {
        //        var index = $("#formsEndereco select").length + 1;

        //        $('#idEnderecoCabecalho').clone(true).appendTo('#formsEndereco');

        //        //Clone the DropDownList Tipo Residencia
        //        var ddl = $("[id$=dropIdTipoResidencia").clone();

        //        //Set the ID and Name
        //        ddl.attr("id", "dropIdTipoResidencia_" + index);
        //        ddl.attr("name", "dropIdTipoResidencia_" + index);

        //        $('#formsEndereco').append(ddl);

        //        //Clone the DropDownList Tipo Logradouro
        //        ddl = $("[id$=dropIdTipoLogradouro").clone();

        //        //Set the ID and Name
        //        ddl.attr("id", "dropIdTipoLogradouro_" + index);
        //        ddl.attr("name", "dropIdTipoLogradouro_" + index);

        //        $('#formsEndereco').append(ddl);
        //        $('#idEnderecoDados').clone(true).appendTo('#formsEndereco');

        //        //Clone the DropDownList Pais
        //        ddl = $("[id$=dropIdPais").clone();

        //        //Set the ID and Name
        //        ddl.attr("id", "dropIdPais_" + index);
        //        ddl.attr("name", "dropIdPais_" + index);

        //        $('#formsEndereco').append(ddl);

        //        //Clone the DropDownList Estado
        //        ddl = $("[id$=dropIdEstado").clone();

        //        //Set the ID and Name
        //        ddl.attr("id", "dropIdEstado_" + index);
        //        ddl.attr("name", "dropIdEstado_" + index);

        //        $('#formsEndereco').append(ddl);

        //        //Clone the DropDownList Cidade
        //        ddl = $("[id$=dropIdCidade").clone();

        //        //Set the ID and Name
        //        ddl.attr("id", "dropIdCidade_" + index);
        //        ddl.attr("name", "dropIdCidade_" + index);

        //        $('#formsEndereco').append(ddl);
        //    })
        //})
    </script>--%>

    <%-- Script para clonagem dos campos para cartão --%>
    <%--<script>
        //$(document).ready(function () {
        //    $('#btnAddCartao').click(function () {
        //        var index = $("#formsCartao select").length + 1;
        //        //Clone the DropDownList
        //        var ddl = $("[id$=dropIdBandeira]").clone();

        //        //Set the ID and Name
        //        ddl.attr("id", "dropIdBandeira_" + index);
        //        ddl.attr("name", "dropIdBandeira_" + index);

        //        $('#idCartaoCompleto').clone(true).appendTo('#formsCartao');
        //        $('#formsCartao').append(ddl);
        //    })
        //})
    </script>--%>
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
                <a href="ListaCliente.aspx">Lista de Clientes</a>
            </li>
            <li id="idBreadCrumb" runat="server" class="breadcrumb-item active">Cadastro de Cliente</li>
        </ol>

        <div class="text-center">
            <h1 id="idTitle" runat="server" class="h4 text-gray-900 mb-4">Cadastro de Cliente</h1>
        </div>
        <hr class="sidebar-divider" />
        <div class="sidebar-heading mb-3">Dados Pessoais</div>

        <div class="form-group row">
            <div class="col-sm-6 mb-3 mb-sm-0">
                <asp:Label ID="idLinhaCodigo" runat="server" Visible="false">ID: </asp:Label>
            </div>
            <div class="col-sm-6">
                <asp:TextBox ID="txtIdClientePF" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
            </div>
        </div>


        <div class="form-group row">
            <div class="col-sm-6 mb-3 mb-sm-0">
                <%--<input type="text" class="form-control form-control-user" id="primeiroNome" placeholder="Primeiro Nome">--%>
                <asp:TextBox ID="txtNome" runat="server" CssClass="form-control form-control-user" Placeholder="Nome"></asp:TextBox>
            </div>
            <div class="col-sm-6">
                <%--<input type="text" class="form-control form-control-user" id="ultimoNome" placeholder="Último Nome">--%>
                <asp:TextBox ID="txtSobrenome" runat="server" CssClass="form-control form-control-user" Placeholder="Sobrenome"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <asp:TextBox ID="txtCPF" runat="server" CssClass="form-control form-control-user" Placeholder="CPF"></asp:TextBox>
        </div>
        <div class="form-group row">
            <div class="col-sm-6 mb-3 mb-sm-0">
                <div class="input-group-append">
                    <asp:TextBox ID="txtDtNascimento" type="date" runat="server" CssClass="form-control form-control-user" Placeholder="Data de Nascimento"></asp:TextBox>
                </div>
            </div>
            <div class="col-sm-6">
                <asp:DropDownList ID="dropIdGenero" CssClass="form-control form-control-user" runat="server" Enabled="true"></asp:DropDownList>
            </div>
        </div>

        <hr class="sidebar-divider" />
        <div class="sidebar-heading mb-3">Dados de Contato</div>

        <div class="form-group">
            <%--<input type="email" class="form-control form-control-user" id="exampleInputEmail" placeholder="Email Address">--%>
            <asp:TextBox ID="txtEmail" type="email" runat="server" CssClass="form-control form-control-user" Placeholder="E-mail"></asp:TextBox>
        </div>


        <asp:TextBox ID="txtIdTelefone" type="hidden" runat="server" CssClass="form-control form-control-user" Placeholder="ID Telefone"></asp:TextBox>
        <div class="form-group row">
            <div class="col-sm-4 mb-3 mb-sm-0">
                <asp:DropDownList ID="dropIdTipoTelefone" CssClass="form-control form-control-user" runat="server" Enabled="true"></asp:DropDownList>
            </div>
            <div class="col-sm-4 mb-3 mb-sm-0">
                <div class="input-group-append">
                    <asp:TextBox ID="txtDDD" runat="server" CssClass="form-control form-control-user" Placeholder="DDD"></asp:TextBox>
                </div>
            </div>
            <div class="col-sm-4">
                <div class="input-group-append">
                    <asp:TextBox ID="txtTelefone" runat="server" CssClass="form-control form-control-user" Placeholder="Nº do Telefone"></asp:TextBox>
                </div>
            </div>
        </div>

        <%-- div necessária para fazer a clonagem dos campos de endereço --%>
        <div id="idEnderecoCabecalho">
            <hr class="sidebar-divider" />
            <div class="sidebar-heading mb-3">
                Dados de Endereço

                    <!-- Botão para adição de endereço -->
                <%-- <span id="btnAddEndereco" class="btn btn-primary fa-pull-right btn-sm" style="cursor: pointer;">
                    <i class="fas fa-fw fa-plus"></i>
                </span> --%>
            </div>

            <asp:TextBox ID="txtIdEndereco" type="hidden" runat="server" CssClass="form-control form-control-user" Placeholder="ID Endereco"></asp:TextBox>
            <div class="form-group">
                <asp:TextBox ID="txtNomeEndereco" runat="server" CssClass="form-control form-control-user" Placeholder="Atribua um nome para o endereço"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:TextBox ID="txtNomeDestinatario" runat="server" CssClass="form-control form-control-user" Placeholder="Nome do destinatário"></asp:TextBox>
            </div>
        </div>

        <div class="form-group">
            <asp:DropDownList ID="dropIdTipoResidencia" CssClass="form-control form-control-user" runat="server" Enabled="true"></asp:DropDownList>
        </div>
        <div class="form-group">
            <asp:DropDownList ID="dropIdTipoLogradouro" CssClass="form-control form-control-user" runat="server" Enabled="true"></asp:DropDownList>
        </div>

        <div id="idEnderecoDados">
            <div class="form-group row">
                <div class="col-sm-6 mb-3 mb-sm-0">
                    <div class="input-group-append">
                        <asp:TextBox ID="txtRua" runat="server" CssClass="form-control form-control-user" Placeholder="Rua"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-6 mb-3 mb-sm-0">
                    <div class="input-group-append">
                        <asp:TextBox ID="txtNumero" runat="server" CssClass="form-control form-control-user" Placeholder="Número"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="form-group row">
                <div class="col-sm-6 mb-3 mb-sm-0">
                    <div class="input-group-append">
                        <asp:TextBox ID="txtBairro" runat="server" CssClass="form-control form-control-user" Placeholder="Bairro"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="input-group-append">
                        <asp:TextBox ID="txtCEP" runat="server" CssClass="form-control form-control-user" Placeholder="CEP"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <asp:TextBox ID="txtObservacao" runat="server" CssClass="form-control form-control-user" Placeholder="Observação"></asp:TextBox>
            </div>
        </div>

        <asp:UpdatePanel ID="upDados" runat="server">
            <ContentTemplate>
                <div class="form-group row">
                    <div class="col-sm-4 mb-3 mb-sm-0">
                        <asp:DropDownList AutoPostBack="true" ID="dropIdPais" CssClass="form-control form-control-user" runat="server" Enabled="true" OnSelectedIndexChanged="dropIdPais_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-sm-4 mb-3 mb-sm-0">
                        <asp:DropDownList AutoPostBack="true" ID="dropIdEstado" CssClass="form-control form-control-user" runat="server" Enabled="false" OnSelectedIndexChanged="dropIdEstado_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-sm-4 mb-3 mb-sm-0">
                        <asp:DropDownList AutoPostBack="true" ID="dropIdCidade" CssClass="form-control form-control-user" runat="server" Enabled="false"></asp:DropDownList>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div id="formsEndereco" class="form-group">
        </div>

        <%-- div necessária para fazer a clonagem dos campos de cartão --%>
        <%--<div id="idCartaoCompleto">
            <hr class="sidebar-divider" />
            <div class="sidebar-heading mb-3">
                Dados de Cartão de Crédito

                    <!-- Botão para adição de Cartão -->
                <span id="btnAddCartao" class="btn btn-primary fa-pull-right btn-sm" style="cursor: pointer;">
                    <i class="fas fa-fw fa-plus"></i>
                </span>
            </div>
            <p class="text-xs font-weight-bold text-danger">Dados do cartão de crédito NÃO são campos de preenchimento obrigatório!</p>
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
        </div>

        <div class="form-group">
            <asp:DropDownList ID="dropIdBandeira" type="text" CssClass="form-control form-control-user" runat="server" Enabled="true"></asp:DropDownList>
        </div>

        <div id="formsCartao" class="form-group">
        </div>--%>

        <%--        <hr class="sidebar-divider" />
        <div class="sidebar-heading mb-3">Dados de Acesso</div>

        <div class="form-group row">
            <div class="col-sm-6 mb-3 mb-sm-0">
                <input type="password" class="form-control form-control-user" id="txtSenha" placeholder="Senha">
            </div>
            <div class="col-sm-6">
                <input type="password" class="form-control form-control-user" id="txtConfirmaSenha" placeholder="Confirmar senha">
            </div>
        </div>--%>

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
