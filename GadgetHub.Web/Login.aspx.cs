using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using GadgetHub.Web.Models;

namespace GadgetHub.Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) { }

        protected async void btnLogin_Click(object sender, EventArgs e)
        {
            string apiUrl = "https://localhost:7165/api/customers/login"; // Your actual port

            var loginData = new
            {
                Username = txtUsername.Text,
                Password = txtPassword.Text
            };

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var json = JsonConvert.SerializeObject(loginData);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        var user = JsonConvert.DeserializeObject<LoginResponseDto>(result);

                        Session["Customer"] = new AuthResponseDto
                        {
                            CustomerId = user.CustomerId,
                            Username = user.Username,
                            Name = user.Name
                        };

                        Response.Redirect("Default.aspx");
                    }

                    else
                    {
                        lblMessage.Text = "Invalid username or password.";
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
