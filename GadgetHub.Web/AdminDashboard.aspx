<%@ Page Title="Admin Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="GadgetHub.Web.AdminDashboard" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="mb-4">🛠️ Admin Dashboard</h2>

    <!-- Add Distributor Form -->
    <div class="card mb-4">
        <div class="card-header bg-primary text-white">Add New Distributor</div>
        <div class="card-body">
            <div class="row mb-2">
                <div class="col-md-4">
                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Username" />
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Password" />
                </div>
                <div class="col-md-4">
                    <asp:Button ID="btnAddDistributor" runat="server" Text="Add Distributor" OnClick="btnAddDistributor_Click" CssClass="btn btn-success w-100" />
                </div>
            </div>
            <asp:Label ID="lblMessage" runat="server" ForeColor="Green" CssClass="fw-bold" />
        </div>
    </div>

    <!-- Distributor Summary Table -->
    <div class="card">
        <div class="card-header bg-secondary text-white">📊 Distributor Summary</div>
        <div class="card-body">
            <asp:GridView ID="gvDistributors" runat="server" AutoGenerateColumns="true" CssClass="table table-striped table-bordered" />
        </div>
    </div>

</asp:Content>
