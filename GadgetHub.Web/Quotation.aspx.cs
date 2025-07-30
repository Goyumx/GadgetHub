using GadgetHub.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GadgetHub.Web
{
    public partial class Quotation : System.Web.UI.Page
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

            // Check if cart has items
            var cart = Session["Cart"] as List<CartItem>;
            if (cart == null || cart.Count == 0)
            {
                Response.Redirect("Default.aspx");
                return;
            }
        }

        protected async void btnRequest_Click(object sender, EventArgs e)
        {
            var cart = Session["Cart"] as List<CartItem>;
            if (cart == null || cart.Count == 0)
            {
                lblMessage.Text = "Your cart is empty.";
                return;
            }

            var apiUrl = "https://localhost:7165/api/quotations";
            var allQuotations = new List<QuotationResponseDto>();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    foreach (var item in cart)
                    {
                        var singleRequest = new
                        {
                            ProductId = item.ProductId,
                            Quantity = item.Quantity
                        };

                        var json = JsonConvert.SerializeObject(singleRequest);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                        var response = await client.PostAsync(apiUrl, content);
                        if (response.IsSuccessStatusCode)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            var quotations = JsonConvert.DeserializeObject<List<QuotationDto>>(result);

                            // Map to QuotationResponseDto
                            foreach (var quote in quotations)
                            {
                                allQuotations.Add(new QuotationResponseDto
                                {
                                    ProductName = quote.Product.Name,
                                    DistributorName = quote.Distributor.Name,
                                    PricePerUnit = quote.PricePerUnit,
                                    AvailableQuantity = quote.AvailableQuantity,
                                    EstimatedDeliveryDate = quote.EstimatedDeliveryDate
                                });
                            }
                        }
                    }

                    Session["Quotations"] = allQuotations;

                    gvQuotations.DataSource = allQuotations;
                    gvQuotations.DataBind();
                    lblMessage.Text = "Quotations retrieved successfully ✅";
                    
                    // Show the continue button when quotations are loaded
                    if (btnContinueToDistributor != null)
                    {
                        btnContinueToDistributor.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Error: " + ex.Message;
                }
            }
        }

        protected void btnContinueToDistributor_Click(object sender, EventArgs e)
        {
            Response.Redirect("SelectDistributor.aspx");
        }
    }
}
