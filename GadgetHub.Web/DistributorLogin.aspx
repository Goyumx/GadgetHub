<%@ Page Title="Distributor Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DistributorLogin.aspx.cs" Inherits="GadgetHub.Web.DistributorLogin" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row justify-content-center mt-5">
        <div class="col-md-6">
            <div class="card shadow p-4">
                <h3 class="text-center mb-4">🔐 Distributor Login</h3>
                <div class="mb-3">
                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Username" />
                </div>
                <div class="mb-3">
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Password" />
                </div>
                <div class="d-grid">
                    <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" CssClass="btn btn-primary" />
                </div>
                <asp:Label ID="lblMessage" runat="server" CssClass="text-danger mt-2 d-block" />
            </div>
        </div>
    </div>
</asp:Content>
