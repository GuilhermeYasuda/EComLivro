<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageAdm.Master" AutoEventWireup="true" CodeBehind="~/CadastroEndereco.aspx.cs" Inherits="EComLivro.CadastroEndereco" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>ÉComLivro - Cadastro de Endereço</title>

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
            <li id="idBreadCrumb" runat="server" class="breadcrumb-item active">Cadastro de Endereço</li>
        </ol>

        <div class="text-center">
            <h1 id="idTitle" runat="server" class="h4 text-gray-900 mb-4">Cadastro de Endereço</h1>
        </div>

        <hr class="sidebar-divider" />
        <div class="sidebar-heading mb-3">
            Dados de Endereço
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
                <asp:Label ID="idLinhaCodigo" runat="server" Visible="false">ID do Endereço: </asp:Label>
            </div>
            <div class="col-sm-6">
                <asp:TextBox ID="txtIdEndereco" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <asp:TextBox ID="txtNomeEndereco" runat="server" CssClass="form-control form-control-user" Placeholder="Atribua um nome para o endereço"></asp:TextBox>
        </div>
        <div class="form-group">
            <asp:TextBox ID="txtNomeDestinatario" runat="server" CssClass="form-control form-control-user" Placeholder="Nome do destinatário"></asp:TextBox>
        </div>

        <div class="form-group">
            <asp:DropDownList ID="dropIdTipoResidencia" CssClass="form-control form-control-user" runat="server" Enabled="true"></asp:DropDownList>
        </div>
        <div class="form-group">
            <asp:DropDownList ID="dropIdTipoLogradouro" CssClass="form-control form-control-user" runat="server" Enabled="true"></asp:DropDownList>
        </div>

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
