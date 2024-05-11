using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalApplication
{
    public partial class RepairList : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        protected void ViewDetails_Click(object sender, EventArgs e)
        {
            // Cast the sender to a LinkButton
            LinkButton lb = (LinkButton)sender;

            // Retrieve the CommandArgument which contains the RepairId
            string repairId = lb.CommandArgument;

            // Redirect to the TicketDetails page with the RepairId as a query string parameter
            Response.Redirect($"TicketDetails.aspx?RepairId={repairId}");
        }

        private void BindGrid()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Repairs", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        GridViewRepairs.DataSource = dt;
                        GridViewRepairs.DataBind();
                    }
                }
            }
        }
    }
}
