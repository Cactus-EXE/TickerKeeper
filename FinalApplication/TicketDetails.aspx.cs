using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace FinalApplication
{
    public partial class TicketDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string repairId = Request.QueryString["RepairId"];
                if (!string.IsNullOrEmpty(repairId))
                {
                    LoadTicketDetails(repairId);
                }
                else
                {
                    // Handle the case where no RepairId is provided
                    // You could redirect back or show a message
                }
            }
        }
       

        private void LoadTicketDetails(string repairId)
        {
            DataTable ticketDetails = GetTicketDetailsFromDatabase(repairId);
            if (ticketDetails.Rows.Count > 0)
            {
                DataRow row = ticketDetails.Rows[0];
                lblRepairId.Text = row["RepairId"].ToString();
                lblFirstName.Text = row["FirstName"].ToString();
                lblLastName.Text = row["LastName"].ToString();
                lblMake.Text = row["MakeName"].ToString();
                lblModel.Text = row["ModelName"].ToString();
                lblProblemDescription.Text = row["ProblemDescription"].ToString();
                // Set other details similarly
            }
            else
            {
                // Handle the case where the ticket is not found
                // You could redirect back or show a message
            }
        }

        private DataTable GetTicketDetailsFromDatabase(string repairId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Updated SQL query to include JOINs with Makes and Models tables
                string sqlQuery = @"
            SELECT R.RepairId, R.FirstName, R.LastName, R.ProblemDescription,
                   M.MakeName AS MakeName, 
                   Mo.ModelName AS ModelName
            FROM Repairs R
            INNER JOIN Makes M ON R.MakeId = M.MakeId
            INNER JOIN Models Mo ON R.ModelId = Mo.ModelId
            WHERE R.RepairId = @RepairId";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@RepairId", repairId);

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
                        // Handle the exception
                        // Log the error and/or show an error message
                    }
                }
            }

            return dataTable;
        }


    }
}
