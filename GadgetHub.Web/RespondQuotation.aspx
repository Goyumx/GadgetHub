<%@ Page Title="Respond to Quotations" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RespondQuotation.aspx.cs" Inherits="GadgetHub.Web.RespondQuotation" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="mb-4">📦 Respond to Quotations</h2>

    <asp:GridView ID="gvQuotations" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered"
        OnRowCommand="gvQuotations_RowCommand">
        <Columns>
            <asp:BoundField DataField="ProductName" HeaderText="Product" />
            <asp:BoundField DataField="RequestedQuantity" HeaderText="Requested Qty" />

            <asp:TemplateField HeaderText="Price Per Unit">
                <ItemTemplate>
                    <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Available Qty">
                <ItemTemplate>
                    <asp:TextBox ID="txtQty" runat="server" CssClass="form-control" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Delivery Date">
                <ItemTemplate>
                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" TextMode="Date" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:ButtonField CommandName="Submit" Text="Submit" ButtonType="Button" ControlStyle-CssClass="btn btn-primary" />
        </Columns>
    </asp:GridView>

    <asp:Label ID="lblMessage" runat="server" CssClass="text-success mt-3" />
</asp:Content>
