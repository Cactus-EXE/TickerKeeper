using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace FinalApplication
{
    public partial class CustomerList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCustomerGridView();
            }
        }

        protected void GridViewCustomers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Optional: You can modify the GridViewRow here if needed
        }

        private void BindCustomerGridView()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT CustomerId, FirstName, LastName, Email, City FROM Customer", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        GridViewCustomers.DataSource = dt;
                        GridViewCustomers.DataBind();
                    }
                }
            }
        }
    }
}
