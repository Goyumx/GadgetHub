using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
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
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.GetAsync("");
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var products = JsonConvert.DeserializeObject<List<ProductDto>>(json);
                        rptProducts.DataSource = products;
                        rptProducts.DataBind();
                    }
                    else
                    {
                        // Handle error
                    }
                }
                catch (Exception ex)
                {
                    // Log or display error
                }
            }
        }
    }
}
