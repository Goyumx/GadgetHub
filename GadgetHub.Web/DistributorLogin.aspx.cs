using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using GadgetHub.Web.Models;
using Newtonsoft.Json;

namespace GadgetHub.Web
{
    public partial class DistributorLogin : Page
    {
        protected void Page_Load(object sender, EventArgs e) { }

        protected async void btnLogin_Click(object sender, EventArgs e)
        {
            string apiUrl = "https://localhost:7165/api/Distributors/login";

            var loginRequest = new
            {
                Username = txtUsername.Text,
                Password = txtPassword.Text
            };

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string json = JsonConvert.SerializeObject(loginRequest);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(apiUrl, content);
                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        var user = JsonConvert.DeserializeObject<DistributorLoginResponseDto>(result);

                        // Save distributor ID & name to session
                        Session["DistributorId"] = user.DistributorId;
                        Session["DistributorName"] = user.Name;

                        Response.Redirect("DistributorDashboard.aspx");
                    }
                    else
                    {
                        lblMessage.Text = "Invalid credentials.";
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Login failed: " + ex.Message;
                }
            }
        }
    }
}
