using System;

namespace GadgetHub.Web
{
    public partial class AdminLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) { }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // You can also query DB here instead of hardcoding
            if (username == "admin" && password == "1234")
            {
                Session["Admin"] = true;
                Response.Redirect("AdminDashboard.aspx");
            }
            else
            {
                lblMessage.Text = "Invalid admin credentials.";
            }
        }
    }
}
