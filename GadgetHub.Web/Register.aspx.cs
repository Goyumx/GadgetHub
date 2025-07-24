using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GadgetHub.Web
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected async void btnRegister_Click(object sender, EventArgs e)
        {
            string apiUrl = "https://localhost:7165/api/customers/register"; // Update with your port

            var data = new
            {
                Username = txtUsername.Text,
                Password = txtPassword.Text,
                Name = txtName.Text,
                Phone = txtPhone.Text,
                Address = txtAddress.Text
            };

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var json = JsonConvert.SerializeObject(data);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        lblMessage.Text = "Registration successful. You can now login.";
                    }
                    else
                    {
                        lblMessage.Text = "Registration failed. Try a different username.";
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Error: " + ex.Message;
                }
            }
        }
    }
}
