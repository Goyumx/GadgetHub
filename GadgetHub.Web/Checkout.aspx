<%@ Page Title="Checkout" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="GadgetHub.Web.Checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h3 class="mb-3">📦 Checkout</h3>

    <asp:GridView ID="gvSummary" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered">
        <Columns>
            <asp:BoundField DataField="ProductName" HeaderText="Product" />
            <asp:BoundField DataField="DistributorName" HeaderText="Distributor" />
            <asp:BoundField DataField="PricePerUnit" HeaderText="Price" DataFormatString="{0:C}" />
            <asp:BoundField DataField="EstimatedDeliveryDate" HeaderText="Delivery Date" DataFormatString="{0:yyyy-MM-dd}" />
        </Columns>
    </asp:GridView>

    <hr />

    <h4>📝 Customer Details</h4>

    <div class="mb-3">
        <label>Name</label>
        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" />
    </div>

    <div class="mb-3">
        <label>Phone</label>
        <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" />
    </div>

    <div class="mb-3">
        <label>Address</label>
        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
    </div>

    <asp:Button ID="btnPlaceOrder" runat="server" Text="Place Order" CssClass="btn btn-success" OnClick="btnPlaceOrder_Click" />

    <asp:Label ID="lblMessage" runat="server" CssClass="text-success mt-3 d-block" />

</asp:Content>
