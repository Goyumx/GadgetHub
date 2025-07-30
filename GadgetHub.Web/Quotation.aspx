<%@ Page Title="Request Quotation" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Quotation.aspx.cs" Inherits="GadgetHub.Web.Quotation" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>📨 Request Quotations</h3>

    <asp:Button ID="btnRequest" runat="server" Text="Send Quotation Request" CssClass="btn btn-primary mb-3" OnClick="btnRequest_Click" />

    <asp:GridView ID="gvQuotations" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered">
        <Columns>
            <asp:BoundField DataField="ProductName" HeaderText="Product" />
            <asp:BoundField DataField="DistributorName" HeaderText="Distributor" />
            <asp:BoundField DataField="PricePerUnit" HeaderText="Price" DataFormatString="{0:C}" />
            <asp:BoundField DataField="AvailableQuantity" HeaderText="Available Qty" />
            <asp:BoundField DataField="EstimatedDeliveryDate" HeaderText="Delivery Date" DataFormatString="{0:yyyy-MM-dd}" />
        </Columns>
    </asp:GridView>

    <asp:Button ID="btnContinueToDistributor" runat="server" Text="Continue to Select Distributor" CssClass="btn btn-success mt-3" OnClick="btnContinueToDistributor_Click" Visible="false" />

    <asp:Label ID="lblMessage" runat="server" CssClass="text-success mt-2" />
</asp:Content>
