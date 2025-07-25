using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using GadgetHub.Web.Models;
using Newtonsoft.Json;

namespace GadgetHub.Web
{
    public partial class _Default : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                await LoadProductsAsync();
            }
        }

        private async Task LoadProductsAsync()
        {
            string apiUrl = "https://localhost:7165/api/products"; // Replace with actual port

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var products = JsonConvert.DeserializeObject<List<ProductDto>>(json);

                        rptProducts.DataSource = products;
                        rptProducts.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    // Optionally handle/log error
                }
            }
        }

        protected async void rptProducts_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "AddToCart")
            {
                int productId = Convert.ToInt32(e.CommandArgument);
                await AddProductToCart(productId);
            }
        }

        private async Task AddProductToCart(int productId)
        {
            string apiUrl = $"https://localhost:7165/api/products/{productId}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var product = JsonConvert.DeserializeObject<ProductDto>(json);

                        // Get or create cart
                        List<CartItem> cart;
                        if (Session["Cart"] == null)
                            cart = new List<CartItem>();
                        else
                            cart = (List<CartItem>)Session["Cart"];

                        var existingItem = cart.FirstOrDefault(i => i.ProductId == product.Id);
                        if (existingItem != null)
                        {
                            existingItem.Quantity++;
                        }
                        else
                        {
                            cart.Add(new CartItem
                            {
                                ProductId = product.Id,
                                Name = product.Name,
                                Category = product.Category,
                                Price = product.Price,
                                Quantity = 1
                            });
                        }

                        // Save session and only then redirect
                        Session["Cart"] = cart;
                        Context.ApplicationInstance.CompleteRequest(); // cleaner than Response.Redirect for async
                        Response.Redirect("Cart.aspx", false); // false = don't abort thread
                    }
                }
                catch (Exception ex)
                {
                    // Optionally show error
                }
            }
        }

    }
}
