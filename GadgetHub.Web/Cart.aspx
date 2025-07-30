<%@ Page Title="Cart" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="GadgetHub.Web.Cart" Async="true" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <h3>🛒 Your Cart</h3>

    <asp:GridView ID="gvCart" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered" OnRowCommand="gvCart_RowCommand">
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Product" />
            <asp:BoundField DataField="Category" HeaderText="Category" />
            <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:C}" />
            <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
            <asp:TemplateField HeaderText="Actions">
                <ItemTemplate>
                    <asp:Button ID="btnRemove" runat="server" Text="Remove" CommandName="RemoveItem" CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-danger btn-sm" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <asp:Label ID="lblTotal" runat="server" CssClass="fw-bold fs-5"></asp:Label>

    <div class="mt-3">
        <asp:Button ID="btnRequestQuotations" runat="server" Text="Request Quotations" CssClass="btn btn-primary" OnClick="btnRequestQuotations_Click" />
    </div>
</asp:Content>

