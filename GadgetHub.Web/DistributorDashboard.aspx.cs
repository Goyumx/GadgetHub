using GadgetHub.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace GadgetHub.Web
{
    public partial class DistributorDashboard : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            // Check if distributor is logged in
            if (Session["DistributorId"] == null || Session["DistributorName"] == null)
            {
                Response.Redirect("DistributorLogin.aspx");
                return;
            }

            if (!IsPostBack)
            {
                await LoadQuotationsAsync();
                await LoadOrdersAsync();
            }
        }

        private async Task LoadQuotationsAsync()
        {
            try
            {
                int distributorId = (int)Session["DistributorId"];
                string apiUrl = $"https://localhost:7165/api/Distributors/{distributorId}/quotations";

                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        var quotations = JsonConvert.DeserializeObject<List<QuotationDisplayDto>>(result);

                        gvQuotations.DataKeyNames = new[] { "QuotationId" };
                        gvQuotations.DataSource = quotations;
                        gvQuotations.DataBind();
                    }
                    else
                    {
                        lblMessage.Text = "Failed to load quotations. Please try again.";
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error loading quotations: {ex.Message}";
            }
        }

        protected async void btnSubmitResponse_Click(object sender, EventArgs e)
        {
            try
            {
                var updates = new List<QuotationUpdateDto>();

                foreach (GridViewRow row in gvQuotations.Rows)
                {
                    int quotationId = Convert.ToInt32(gvQuotations.DataKeys[row.RowIndex].Value);
                    
                    // Get the textbox controls with null checks
                    var txtPrice = row.FindControl("txtPrice") as TextBox;
                    var txtQuantity = row.FindControl("txtQuantity") as TextBox;
                    var txtDate = row.FindControl("txtDate") as TextBox;

                    if (txtPrice != null && txtQuantity != null && txtDate != null)
                    {
                        if (decimal.TryParse(txtPrice.Text, out decimal price) &&
                            int.TryParse(txtQuantity.Text, out int quantity) &&
                            DateTime.TryParse(txtDate.Text, out DateTime date))
                        {
                            updates.Add(new QuotationUpdateDto
                            {
                                QuotationId = quotationId,
                                PricePerUnit = price,
                                AvailableQuantity = quantity,
                                EstimatedDeliveryDate = date
                            });
                        }
                    }
                }

                if (updates.Count > 0)
                {
                    string apiUrl = "https://localhost:7165/api/Distributors/respond";
                    using (HttpClient client = new HttpClient())
                    {
                        var json = JsonConvert.SerializeObject(updates);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");

                        var response = await client.PutAsync(apiUrl, content);
                        lblMessage.Text = response.IsSuccessStatusCode
                            ? "Responses submitted successfully ✅"
                            : "Failed to submit responses. Please try again.";
                    }
                }
                else
                {
                    lblMessage.Text = "No valid responses to submit.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error submitting responses: {ex.Message}";
            }
        }

        private async Task LoadOrdersAsync()
        {
            try
            {
                int distributorId = (int)Session["DistributorId"];
                string apiUrl = $"https://localhost:7165/api/Distributors/{distributorId}/orders";

                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        var orders = JsonConvert.DeserializeObject<List<OrderDto>>(result);
                        gvOrders.DataKeyNames = new[] { "OrderId" };
                        gvOrders.DataSource = orders;
                        gvOrders.DataBind();
                    }
                    else
                    {
                        lblMessage.Text = "Failed to load orders. Please try again.";
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error loading orders: {ex.Message}";
            }
        }

        protected async void btnMarkDelivered_Click(object sender, EventArgs e)
        {
            try
            {
                bool anyMarked = false;
                foreach (GridViewRow row in gvOrders.Rows)
                {
                    CheckBox chk = row.FindControl("chkDeliver") as CheckBox;
                    if (chk != null && chk.Checked)
                    {
                        int orderId = Convert.ToInt32(gvOrders.DataKeys[row.RowIndex].Value);
                        string apiUrl = $"https://localhost:7165/api/Distributors/mark-delivered/{orderId}";

                        using (HttpClient client = new HttpClient())
                        {
                            var response = await client.PutAsync(apiUrl, null);
                            if (response.IsSuccessStatusCode)
                            {
                                anyMarked = true;
                            }
                        }
                    }
                }

                if (anyMarked)
                {
                    lblMessage.Text = "Orders marked as delivered successfully ✅";
                    await LoadOrdersAsync(); // Refresh the orders list
                }
                else
                {
                    lblMessage.Text = "No orders were selected or marked as delivered.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error marking orders as delivered: {ex.Message}";
            }
        }
    }
}
