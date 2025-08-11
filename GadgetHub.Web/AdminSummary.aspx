<%@ Page Title="Admin Summary" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminSummary.aspx.cs" Inherits="GadgetHub.Web.AdminSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="row mb-4">
            <div class="col-12">
                <h3 class="mb-3">📊 Distributor Summary</h3>
                <asp:Button ID="btnRefresh" runat="server" Text="Refresh Data" CssClass="btn btn-primary mb-3" OnClick="btnRefresh_Click" />
                <asp:Label ID="lblMessage" runat="server" CssClass="text-danger d-block mb-3" />
            </div>
        </div>
        
        <!-- Summary Cards -->
        <div class="row mb-4">
            <div class="col-md-3">
                <div class="card bg-primary text-white">
                    <div class="card-body">
                        <h5 class="card-title">Total Distributors</h5>
                        <h3><asp:Label ID="lblTotalDistributors" runat="server" Text="0" /></h3>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="card bg-success text-white">
                    <div class="card-body">
                        <h5 class="card-title">Active Distributors</h5>
                        <h3><asp:Label ID="lblActiveDistributors" runat="server" Text="0" /></h3>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="card bg-info text-white">
                    <div class="card-body">
                        <h5 class="card-title">Total Orders</h5>
                        <h3><asp:Label ID="lblTotalOrders" runat="server" Text="0" /></h3>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="card bg-warning text-white">
                    <div class="card-body">
                        <h5 class="card-title">Total Earnings</h5>
                        <h3>$<asp:Label ID="lblTotalEarnings" runat="server" Text="0.00" /></h3>
                    </div>
                </div>
            </div>
        </div>
        
        <!-- Data Table -->
        <div class="row">
            <div class="col-12">
                <div class="card">
                    <div class="card-header">
                        <h5 class="mb-0">Distributor Details</h5>
                    </div>
                    <div class="card-body">
                        <asp:GridView ID="gvSummary" runat="server" 
                            CssClass="table table-striped table-hover" 
                            AutoGenerateColumns="false"
                            EmptyDataText="No distributor data available.">
                            <Columns>
                                <asp:BoundField DataField="distributor" HeaderText="Distributor Name" 
                                    HeaderStyle-CssClass="bg-light" />
                                <asp:BoundField DataField="ordersHandled" HeaderText="Orders Handled" 
                                    HeaderStyle-CssClass="bg-light" ItemStyle-CssClass="text-center" />
                                <asp:BoundField DataField="totalEarnings" HeaderText="Total Earnings" 
                                    HeaderStyle-CssClass="bg-light" DataFormatString="{0:C}" 
                                    ItemStyle-CssClass="text-end" />
                                <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="bg-light" 
                                    ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" 
                                            Text='<%# Convert.ToInt32(Eval("ordersHandled")) > 0 ? "Active" : "Inactive" %>'
                                            CssClass='<%# Convert.ToInt32(Eval("ordersHandled")) > 0 ? "badge bg-success" : "badge bg-secondary" %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>