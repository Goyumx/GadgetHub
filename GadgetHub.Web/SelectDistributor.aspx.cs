using GadgetHub.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace GadgetHub.Web
{
    public partial class SelectDistributor : System.Web.UI.Page
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

            // Check if quotations exist
            var quotations = Session["Quotations"] as List<QuotationResponseDto>;
            if (quotations == null || quotations.Count == 0)
            {
                Response.Redirect("Quotation.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadQuotationOptions();
            }
        }

        private void LoadQuotationOptions()
        {
            var quotations = Session["Quotations"] as List<QuotationResponseDto>;
            if (quotations == null || quotations.Count == 0)
            {
                phQuotations.Controls.Add(new Literal { Text = "<p>No quotations available.</p>" });
                btnContinue.Visible = false;
                return;
            }

            var grouped = quotations.GroupBy(q => q.ProductName);
            foreach (var group in grouped)
            {
                var lblProduct = new Label
                {
                    Text = $"<h4>{group.Key}</h4>",
                    CssClass = "mt-4"
                };
                phQuotations.Controls.Add(lblProduct);

                var radioList = new RadioButtonList
                {
                    ID = "rb_" + group.Key.Replace(" ", "_"),
                    RepeatDirection = RepeatDirection.Vertical,
                    CssClass = "mb-3"
                };

                foreach (var quote in group)
                {
                    radioList.Items.Add(new ListItem(
                        $"{quote.DistributorName} - {quote.PricePerUnit:C} - {quote.AvailableQuantity} available - Deliver by {quote.EstimatedDeliveryDate.ToShortDateString()}",
                        quote.DistributorName
                    ));
                }

                phQuotations.Controls.Add(radioList);
            }
        }

        protected void btnContinue_Click(object sender, EventArgs e)
        {
            var quotations = Session["Quotations"] as List<QuotationResponseDto>;
            if (quotations == null) return;

            var grouped = quotations.GroupBy(q => q.ProductName);
            var selections = new List<SelectedDistributorDto>();

            foreach (var group in grouped)
            {
                var controlId = "rb_" + group.Key.Replace(" ", "_");
                var radioList = phQuotations.FindControl(controlId) as RadioButtonList;

                if (radioList != null && !string.IsNullOrEmpty(radioList.SelectedValue))
                {
                    var selected = group.First(q => q.DistributorName == radioList.SelectedValue);

                    selections.Add(new SelectedDistributorDto
                    {
                        ProductName = selected.ProductName,
                        DistributorName = selected.DistributorName,
                        PricePerUnit = selected.PricePerUnit,
                        EstimatedDeliveryDate = selected.EstimatedDeliveryDate
                    });
                }
            }

            Session["Selections"] = selections;
            Response.Redirect("Checkout.aspx");
        }
    }
}
