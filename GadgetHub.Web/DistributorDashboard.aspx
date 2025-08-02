<%@ Page Title="Distributor Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DistributorDashboard.aspx.cs" Inherits="GadgetHub.Web.DistributorDashboard" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h3 class="text-center mb-4">📦 Welcome, <%= Session["DistributorName"] %>!</h3>

        <h5>📝 Pending Quotations</h5>
        <asp:GridView ID="gvQuotations" runat="server" AutoGenerateColumns="false" DataKeyNames="QuotationId" CssClass="table">

            <Columns>
                <asp:BoundField DataField="QuotationId" HeaderText="Quotation ID" />
                <asp:BoundField DataField="ProductName" HeaderText="Product" />
                <asp:TemplateField HeaderText="Price Per Unit">
                    <ItemTemplate>
                        <asp:TextBox ID="txtPrice" runat="server" Text='<%# Eval("PricePerUnit") %>' CssClass="form-control" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Available Quantity">
                    <ItemTemplate>
                        <asp:TextBox ID="txtQuantity" runat="server" Text='<%# Eval("AvailableQuantity") %>' CssClass="form-control" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delivery Date">
                    <ItemTemplate>
                        <asp:TextBox ID="txtDate" runat="server" Text='<%# Eval("EstimatedDeliveryDate", "{0:yyyy-MM-dd}") %>' CssClass="form-control" TextMode="Date" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <asp:Button ID="btnSubmitResponse" runat="server" Text="Submit Responses" CssClass="btn btn-success" OnClick="btnSubmitResponse_Click" />
        <hr />

        <h5>📬 Orders Assigned to You</h5>
        <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="false" DataKeyNames="OrderId" CssClass="table table-bordered">
            <Columns>
                <asp:TemplateField HeaderText="Select">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkDeliver" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="OrderId" HeaderText="Order ID" />
                <asp:BoundField DataField="ProductName" HeaderText="Product" />
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                <asp:BoundField DataField="AgreedPrice" HeaderText="Agreed Price" DataFormatString="{0:C}" />
                <asp:BoundField DataField="CustomerName" HeaderText="Customer" />
                <asp:CheckBoxField DataField="IsDelivered" HeaderText="Delivered" ReadOnly="true" />
            </Columns>
        </asp:GridView>
        <asp:Button ID="btnMarkDelivered" runat="server" Text="Mark Selected Orders Delivered" CssClass="btn btn-warning" OnClick="btnMarkDelivered_Click" />
        <br /><br />

        <asp:Label ID="lblMessage" runat="server" CssClass="text-success" />
    </div>
</asp:Content>
