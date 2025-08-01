using GadgetHub.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GadgetHub.Web
{
    public partial class AdminDashboard : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                await LoadDistributorsAsync();
                await LoadSummaryAsync();
            }
        }

        private async Task LoadDistributorsAsync()
        {
            string apiUrl = "https://localhost:7165/api/Admin/summary"; // Corrected API endpoint
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var distributors = JsonConvert.DeserializeObject<List<AdminSummaryDto>>(json);
                    gvDistributors.DataSource = distributors;
                    gvDistributors.DataBind();
                }
            }
        }

        protected async void btnAddDistributor_Click(object sender, EventArgs e)
        {
            string apiUrl = "https://localhost:7165/api/Admin/add-distributor"; // Corrected POST endpoint
            var distributorData = new
            {
                Username = txtUsername.Text,
                Password = txtPassword.Text
            };

            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(distributorData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    lblMessage.Text = "Distributor added successfully ✅";
                    await LoadDistributorsAsync();
                }
                else
                {
                    lblMessage.Text = "Failed to add distributor ❌";
                }
            }
        }

        private async Task LoadSummaryAsync()
        {
            // This is just here if you want a separate summary table, optional for dashboard
        }
    }
}
