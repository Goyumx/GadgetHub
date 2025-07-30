using GadgetHub.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace GadgetHub.Web
{
    public partial class Checkout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if user is logged in
            var customer = Session["Customer"] as AuthResponseDto;
            if (customer == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                var selections = Session["Selections"] as List<SelectedDistributorDto>;
                if (selections != null)
                {
                    gvSummary.DataSource = selections;
                    gvSummary.DataBind();
                } 
                else
                {
                    // Redirect back to distributor selection if no selections
                    Response.Redirect("SelectDistributor.aspx");
                }
            }
        }

        protected void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            var selections = Session["Selections"] as List<SelectedDistributorDto>;
            var customer = Session["Customer"] as AuthResponseDto;

            // Debug information
            if (selections == null)
            {
                lblMessage.Text = "❌ Selections session is null. Please go back and select distributors.";
                return;
            }

            if (customer == null)
            {
                lblMessage.Text = "❌ Customer session is null. Please log in again.";
                return;
            }

            var orderRequest = new
            {
                CustomerId = customer.CustomerId,
                CustomerName = txtName.Text,
                Phone = txtPhone.Text,
                Address = txtAddress.Text,
                Items = selections
            };

            var json = JsonConvert.SerializeObject(orderRequest);
            var apiUrl = "https://localhost:7165/api/orders"; // Change if needed

            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = client.PostAsync(apiUrl, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    lblMessage.Text = "✅ Order placed successfully!";
                    Session.Remove("Cart");
                    Session.Remove("Selections");
                }
                else
                {
                    lblMessage.Text = "❌ Failed to place order.";
                }
            }
        }
    }
}
