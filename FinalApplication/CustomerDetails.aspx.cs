using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI;

namespace FinalApplication
{
    public partial class CustomerDetails : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieve the CustomerId from the query string
                string customerId = Request.QueryString["CustomerId"];
                if (!string.IsNullOrEmpty(customerId))
                {
                    LoadCustomerDetails(customerId);
                    LoadCustomerRepairs(customerId); // Load repairs
                }
                else
                {
                    // Handle the case where no CustomerId is provided
                    Response.Write("No customer ID provided.");
                }
            }
        }

        // Loads customer data from the database and updates UI labels
        private void LoadCustomerDetails(string customerId)
        {
            DataTable customerDetails = GetCustomerDetailsFromDatabase(customerId);
            if (customerDetails.Rows.Count > 0)
            {
                DataRow row = customerDetails.Rows[0];
                lblCustomerId.Text = row["CustomerId"].ToString();
                lblFirstName.Text = row["FirstName"].ToString();
                lblLastName.Text = row["LastName"].ToString();
                lblPhoneNumber.Text = row["PhoneNumber"].ToString();
                lblEmail.Text = row["Email"].ToString();
                lblAddress.Text = row["Address"].ToString();
                lblCity.Text = row["City"].ToString();
                lblState.Text = row["State"].ToString();
                lblPostalCode.Text = row["PostalCode"].ToString();
                lblCountry.Text = row["Country"].ToString();
                lblCustomerNotes.Text = row["CustomerNotes"].ToString();
                lblCustomerCreation.Text = row["CreationDate"].ToString();
            }
            else
            {
                // Handle the case where the customer is not found
                Response.Write("Customer not found.");
            }
        }

        // Retrieves customer data from the database by CustomerId
        private DataTable GetCustomerDetailsFromDatabase(string customerId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Query to select customer data by CustomerId
                string sqlQuery = "SELECT * FROM Customer WHERE CustomerId = @CustomerId";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@CustomerId", customerId);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dataTable.Load(reader);
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Handle any SQL errors
                        Response.Write("Database error: " + ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            return dataTable;
        }

        // Loads customer's previous repairs into the GridView
        private void LoadCustomerRepairs(string customerId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            DataTable repairsTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // SQL query to include JOINs with DeviceTypes, Makes, and Models tables
                string sqlQuery = @"
                SELECT R.RepairId, DT.TypeName, M.MakeName, Mo.ModelName, R.ProblemDescription
                FROM Repairs R
                INNER JOIN DeviceTypes DT ON R.TypeId = DT.TypeId
                INNER JOIN Makes M ON R.MakeId = M.MakeId
                INNER JOIN Models Mo ON R.ModelId = Mo.ModelId
                WHERE R.CustomerId = @CustomerId";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@CustomerId", customerId);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            repairsTable.Load(reader);
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Handle any SQL errors
                        Response.Write("Database error: " + ex.Message);
                    }
                }
            }

            // Bind the retrieved data to the GridView
            gridViewRepairs.DataSource = repairsTable;
            gridViewRepairs.DataBind();
        }

        // Button click event for editing a customer
        protected void btnEditCustomer_Click(object sender, EventArgs e)
        {
            // Redirect to an edit page with the customer ID
            Response.Redirect($"EditCustomer.aspx?CustomerId={lblCustomerId.Text}");
        }

        // Button click event for deleting a customer
        protected void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // SQL command to delete the customer by CustomerId
                string query = "DELETE FROM Customer WHERE CustomerId = @CustomerId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@CustomerId", lblCustomerId.Text);
                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                    con.Close();
                    if (result > 0)
                    {
                        Response.Write("Customer deleted successfully.");
                    }
                    else
                    {
                        Response.Write("Error deleting customer.");
                    }
                }
            }
        }

        // Button click event for creating a ticket linked to the current customer
        protected void btnCreateTicket_Click(object sender, EventArgs e)
        {
            // Redirect to the RepairCheckIn page, passing the CustomerId in the query string
            Response.Redirect($"RepairCheckIn.aspx?CustomerId={lblCustomerId.Text}");
        }
    }
}
