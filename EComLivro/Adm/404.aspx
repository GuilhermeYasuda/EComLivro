<%@ Page Title="" Language="C#" MasterPageFile="~/Adm/MasterPageAdm.Master" AutoEventWireup="true" CodeBehind="404.aspx.cs" Inherits="EComLivro.Adm._404" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>ÉComLivro - 404</title>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">

    <!-- Begin Page Content -->
        <div class="container-fluid">

          <!-- 404 Error Text -->
          <div class="text-center">
            <div class="error mx-auto" data-text="404">404</div>
            <p class="lead text-gray-800 mb-5">Page Not Found</p>
            <p class="text-gray-500 mb-0">It looks like you found a glitch in the matrix...</p>
            <a href="index.html">&larr; Back to Dashboard</a>
          </div>

        </div>
        <!-- /.container-fluid -->

</asp:Content>
