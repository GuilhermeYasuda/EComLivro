<%@ Page Title="" Language="C#" MasterPageFile="~/Loja/MastePageLoja.Master" AutoEventWireup="true" CodeBehind="ProductDetails.aspx.cs" Inherits="EComLivro.Loja.ProductDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
        <!-- Product Details Area Start -->

        <div class="single-product-area section-padding-100 clearfix">
            <div class="container-fluid">

                <div class="row">
                    <div class="col-12">
                        <nav aria-label="breadcrumb">
                            <ol class="breadcrumb mt-50">
                                <li class="breadcrumb-item"><a href="Index.aspx">Home</a></li>
                                <li class="breadcrumb-item"><a href="Shop.aspx">Shop</a></li>
                                <li class="breadcrumb-item active" aria-current="page" id="txtBreadCrumb" runat="server"></li>
                            </ol>
                        </nav>
                    </div>
                </div>

                <!-- Para conter o ID do Livro -->
                <asp:TextBox ID="txtIdLivro" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>

                <div class="row">
                    <div class="col-12 col-lg-7">
                        <div class="single_product_thumb">
                            <div id="product_details_slider" class="carousel slide" data-ride="carousel">
                                <ol class="carousel-indicators">
                                    <li class="active" data-target="#product_details_slider" data-slide-to="0" style="background-image: url(../img/SEM-IMAGEM.jpg);">
                                    </li>
                                    <%--<li class="active" data-target="#product_details_slider" data-slide-to="0" style="background-image: url(../img/product-img/pro-big-1.jpg);">
                                    </li>
                                    <li data-target="#product_details_slider" data-slide-to="1" style="background-image: url(../img/product-img/pro-big-2.jpg);">
                                    </li>
                                    <li data-target="#product_details_slider" data-slide-to="2" style="background-image: url(../img/product-img/pro-big-3.jpg);">
                                    </li>
                                    <li data-target="#product_details_slider" data-slide-to="3" style="background-image: url(../img/product-img/pro-big-4.jpg);">
                                    </li>--%>
                                </ol>
                                <div class="carousel-inner">
                                    <div class="carousel-item active">
                                        <a class="gallery_img" href="../img/SEM-IMAGEM.jpg">
                                            <img class="d-block w-100" src="../img/SEM-IMAGEM.jpg" alt="First slide">
                                        </a>
                                    </div>
                                    <%--<div class="carousel-item active">
                                        <a class="gallery_img" href="../img/product-img/pro-big-1.jpg">
                                            <img class="d-block w-100" src="../img/product-img/pro-big-1.jpg" alt="First slide">
                                        </a>
                                    </div>
                                    <div class="carousel-item">
                                        <a class="gallery_img" href="../img/product-img/pro-big-2.jpg">
                                            <img class="d-block w-100" src="../img/product-img/pro-big-2.jpg" alt="Second slide">
                                        </a>
                                    </div>
                                    <div class="carousel-item">
                                        <a class="gallery_img" href="../img/product-img/pro-big-3.jpg">
                                            <img class="d-block w-100" src="../img/product-img/pro-big-3.jpg" alt="Third slide">
                                        </a>
                                    </div>
                                    <div class="carousel-item">
                                        <a class="gallery_img" href="../img/product-img/pro-big-4.jpg">
                                            <img class="d-block w-100" src="../img/product-img/pro-big-4.jpg" alt="Fourth slide">
                                        </a>
                                    </div>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-lg-5">
                        <div class="single_product_desc">
                            <!-- Product Meta Data -->
                            <div class="product-meta-data">
                                <div class="line"></div>
                                <p class="product-price" id="txtPrecoLivro" runat="server"></p>
                                <a>
                                    <h6 id="txtTituloLivro" runat="server"></h6>
                                </a>
                                
                                <!-- Removido, possível adição posterior -->
                                <!-- Ratings & Review -->
                                <%--<div class="ratings-review mb-15 d-flex align-items-center justify-content-between">
                                    <div class="ratings">
                                        <i class="fa fa-star" aria-hidden="true"></i>
                                        <i class="fa fa-star" aria-hidden="true"></i>
                                        <i class="fa fa-star" aria-hidden="true"></i>
                                        <i class="fa fa-star" aria-hidden="true"></i>
                                        <i class="fa fa-star" aria-hidden="true"></i>
                                    </div>
                                    <div class="review">
                                        <a href="#">Write A Review</a>
                                    </div>
                                </div>--%>
                                <!-- Avaiable -->
                                <p class="avaibility"><i class="fa fa-circle"></i> In Stock</p>
                            </div>

                            <div class="short_overview my-5">
                                <p id="txtDescricao" runat="server"></p>
                            </div>

                            <!-- Add to Cart Form -->
                            <form class="cart clearfix" method="post">
                                <div class="cart-btn d-flex mb-50">
                                    <p>Qtde</p>
                                    <div class="quantity">
                                        <span class="qty-minus" onclick="var effect = document.getElementById('qty'); var qty = effect.value; if( !isNaN( qty ) &amp;&amp; qty &gt; 1 ) effect.value--;return false;"><i class="fa fa-caret-down" aria-hidden="true"></i></span>
                                        <input type="number" class="qty-text" id="qty" step="1" min="1" max="300" name="quantity" value="1">
                                        <span class="qty-plus" onclick="var effect = document.getElementById('qty'); var qty = effect.value; if( !isNaN( qty )) effect.value++;return false;"><i class="fa fa-caret-up" aria-hidden="true"></i></span>
                                    </div>
                                </div>
                                <div id="btnAddToCart" runat="server"></div>
                            </form>

                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- Product Details Area End -->

</asp:Content>
