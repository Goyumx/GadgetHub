<%@ Page Title="Admin Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminLogin.aspx.cs" Inherits="GadgetHub.Web.AdminLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3 class="mb-3">🔐 Admin Login</h3>

    <div class="mb-3">
        <label>Username</label>
        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" />
    </div>

    <div class="mb-3">
        <label>Password</label>
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" />
    </div>

    <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary" OnClick="btnLogin_Click" />
    <asp:Label ID="lblMessage" runat="server" CssClass="text-danger mt-2 d-block" />
</asp:Content>
