using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Web.UI.WebControls;
using GadgetHub.Web.Models;
using Newtonsoft.Json;

namespace GadgetHub.Web
{
    public partial class RespondQuotation : System.Web.UI.Page
    {
        private const string apiBase = "https://localhost:7165/api/Distributors";

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                await LoadQuotationsAsync();
        }

        private async System.Threading.Tasks.Task LoadQuotationsAsync()
        {
            if (Session["DistributorId"] == null)
            {
                Response.Redirect("DistributorLogin.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            int distributorId = (int)Session["DistributorId"];
            string url = $"{apiBase}/{distributorId}/quotations";

            using (HttpClient client = new HttpClient())
            {
                var res = await client.GetAsync(url);
                if (res.IsSuccessStatusCode)
                {
                    var json = await res.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<QuotationDisplayDto>>(json);
                    gvQuotations.DataSource = data;
                    gvQuotations.DataBind();
                }
            }
        }

        protected async void gvQuotations_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Submit")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvQuotations.Rows[rowIndex];

                string productName = row.Cells[0].Text;
                int quotationId = GetQuotationId(productName); // You must map ProductName ➝ quotationId via Session or add hidden ID

                TextBox txtPrice = (TextBox)row.FindControl("txtPrice");
                TextBox txtQty = (TextBox)row.FindControl("txtQty");
                TextBox txtDate = (TextBox)row.FindControl("txtDate");

                var dto = new QuotationUpdateDto
                {
                    QuotationId = quotationId,
                    PricePerUnit = decimal.Parse(txtPrice.Text),
                    AvailableQuantity = int.Parse(txtQty.Text),
                    EstimatedDeliveryDate = DateTime.Parse(txtDate.Text)
                };

                using (HttpClient client = new HttpClient())
                {
                    string json = JsonConvert.SerializeObject(dto);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var result = await client.PutAsync($"{apiBase}/respond", content);
                    if (result.IsSuccessStatusCode)
                        lblMessage.Text = "Response submitted successfully ✅";
                    else
                        lblMessage.Text = "Error submitting response.";
                }

                await LoadQuotationsAsync();
            }
        }

        private int GetQuotationId(string productName)
        {
            var quotations = Session["Quotations"] as List<QuotationDisplayDto>;
            var quote = quotations?.Find(q => q.ProductName == productName);
            return quote?.QuotationId ?? 0;
        }
    }
}
