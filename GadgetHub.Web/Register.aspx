<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="GadgetHub.Web.Register" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>📝 Customer Registration</h3>

    <div class="mb-3">
        <label>Username</label>
        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" />
    </div>
    <div class="mb-3">
        <label>Password</label>
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" />
    </div>
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
        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" />
    </div>

    <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn btn-success" OnClick="btnRegister_Click" />
    <asp:Label ID="lblMessage" runat="server" CssClass="text-danger mt-2" />
</asp:Content>
