using System;
using System.Collections.Generic;
using System.Linq;
using GadgetHub.Web.Models;

namespace GadgetHub.Web
{
    public partial class Cart : System.Web.UI.Page
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
                LoadCart();
            }
        }

        private void LoadCart()
        {

            var cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();
            gvCart.DataSource = cart;
            gvCart.DataBind();

            decimal total = cart.Sum(item => item.Price * item.Quantity);
            lblTotal.Text = $"Total: {total:C}";
        }

        protected void gvCart_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "RemoveItem")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                var cart = Session["Cart"] as List<CartItem>;
                if (cart != null && cart.Count > index)
                {
                    cart.RemoveAt(index);
                    Session["Cart"] = cart;
                    LoadCart();
                }
            }
        }

        protected void btnRequestQuotations_Click(object sender, EventArgs e)
        {
            Response.Redirect("Quotation.aspx");
        }
    }
}

