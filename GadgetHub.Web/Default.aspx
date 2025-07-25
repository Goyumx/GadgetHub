<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GadgetHub.Web._Default" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <!-- 🔍 Search Bar -->
    <div class="row mb-3">
        <div class="col-md-6">
            <input type="text" class="form-control" placeholder="Search for gadgets..." />
        </div>
        <div class="col-md-4">
            <select class="form-select">
                <option>All Categories</option>
                <option>Phone</option>
                <option>Tablet</option>
                <option>Laptop</option>
            </select>
        </div>
        <div class="col-md-2">
            <button class="btn btn-primary w-100">Search</button>
        </div>
    </div>

    <!-- 🖼 Bootstrap Carousel -->
    <div id="carouselExampleIndicators" class="carousel slide mb-4" data-bs-ride="carousel">
        <div class="carousel-inner rounded shadow">
            <div class="carousel-item active">
                <img src="https://via.placeholder.com/1000x300?text=Welcome+to+Gadget+Hub" class="d-block w-100" />
            </div>
            <div class="carousel-item">
                <img src="https://via.placeholder.com/1000x300?text=Latest+Phones" class="d-block w-100" />
            </div>
            <div class="carousel-item">
                <img src="https://via.placeholder.com/1000x300?text=Top+Deals+on+Laptops" class="d-block w-100" />
            </div>
        </div>
        <button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide="prev">
            <span class="carousel-control-prev-icon"></span>
        </button>
        <button class="carousel-control-next" type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide="next">
            <span class="carousel-control-next-icon"></span>
        </button>
    </div>

    <!-- 🧱 Product Grid -->
    <div class="row row-cols-1 row-cols-md-4 g-4">
        <asp:Repeater ID="rptProducts" runat="server" OnItemCommand="rptProducts_ItemCommand">
            <ItemTemplate>
                <div class="col">
                    <div class="card h-100 shadow-sm">
                        <img src='<%# !string.IsNullOrEmpty(Eval("ImageUrl") as string) ? Eval("ImageUrl") : "https://via.placeholder.com/200x150" %>' class="card-img-top" />
                        <div class="card-body">
                            <h5 class="card-title"><%# Eval("Name") %></h5>
                            <p class="card-text">Category: <%# Eval("Category") %></p>
                            <p class="card-text"><%# Eval("Description") %></p>

                            <asp:Button ID="btnAddToCart" runat="server"
                                Text="Add to Cart"
                                CssClass="btn btn-outline-success w-100"
                                CommandName="AddToCart"
                                CommandArgument='<%# Eval("Id") %>'
                                UseSubmitBehavior="false" />
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

</asp:Content>
