using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.UI;
using Newtonsoft.Json;

namespace GadgetHub.Web
{
    public partial class AdminSummary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if admin is logged in
            if (Session["Admin"] == null || !(bool)Session["Admin"])
            {
                Response.Redirect("AdminLogin.aspx");
                return;
            }

     
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(LoadSummaryDataAsync));
        }

        private async Task LoadSummaryDataAsync()
        {
            await LoadSummaryData();
        }

        private async Task LoadSummaryData()
        {
            try
            {
                lblMessage.Text = "";
                var summaryData = await GetSummaryFromAPI();

                if (summaryData != null)
                {
                    BindSummaryCards(summaryData);
                    BindGridView(summaryData);
                }
                else
                {
                    lblMessage.Text = "Failed to load summary data.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error loading data: {ex.Message}";
            }
        }

        private async Task<List<DistributorSummary>> GetSummaryFromAPI()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Set timeout
                    client.Timeout = TimeSpan.FromSeconds(30);

                    // If you need authentication headers, add them here
                    // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "your-token");

                    string apiUrl = "https://localhost:7165/api/Admin/summary";
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();
                        var summaryData = JsonConvert.DeserializeObject<List<DistributorSummary>>(jsonContent);
                        return summaryData;
                    }
                    else
                    {
                        lblMessage.Text = $"API Error: {response.StatusCode} - {response.ReasonPhrase}";
                        return null;
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                lblMessage.Text = $"Network Error: {ex.Message}";
                return null;
            }
            catch (TaskCanceledException ex)
            {
                lblMessage.Text = "Request timeout. Please try again.";
                return null;
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error: {ex.Message}";
                return null;
            }
        }

        private void BindSummaryCards(List<DistributorSummary> data)
        {
            int totalDistributors = data.Count;
            int activeDistributors = 0;
            int totalOrders = 0;
            decimal totalEarnings = 0;

            foreach (var item in data)
            {
                if (item.ordersHandled > 0)
                    activeDistributors++;

                totalOrders += item.ordersHandled;
                totalEarnings += item.totalEarnings;
            }

            lblTotalDistributors.Text = totalDistributors.ToString();
            lblActiveDistributors.Text = activeDistributors.ToString();
            lblTotalOrders.Text = totalOrders.ToString();
            lblTotalEarnings.Text = totalEarnings.ToString("F2");
        }

        private void BindGridView(List<DistributorSummary> data)
        {
            gvSummary.DataSource = data;
            gvSummary.DataBind();
        }
    }

    // Model class to match your API response
    public class DistributorSummary
    {
        public string distributor { get; set; }
        public int ordersHandled { get; set; }
        public decimal totalEarnings { get; set; }
    }
}