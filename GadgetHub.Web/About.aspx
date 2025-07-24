<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="GadgetHub.Web.About" %>

<asp:Repeater ID="rptProducts" runat="server">
    <ItemTemplate>
        <div class="col">
            <div class="card h-100 shadow-sm">
                <img src='<%# Eval("ImageUrl") %>' class="card-img-top" style="height: 150px; object-fit: cover;" />
                <div class="card-body">
                    <h5 class="card-title"><%# Eval("Name") %></h5>
                    <p class="card-text">Category: <%# Eval("Category") %></p>
                    <p class="card-text"><%# Eval("Description") %></p>
                    <button class="btn btn-outline-success w-100">Add to Cart</button>
                </div>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>

