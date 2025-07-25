<%@ Page Title="Select Distributor" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SelectDistributor.aspx.cs" Inherits="GadgetHub.Web.SelectDistributor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="mb-3">🛍️ Choose Distributors for Each Product</h2>

    <asp:PlaceHolder ID="phQuotations" runat="server" />

    <asp:Button ID="btnContinue" runat="server" Text="Continue to Checkout" CssClass="btn btn-primary mt-3" OnClick="btnContinue_Click" />

</asp:Content>
