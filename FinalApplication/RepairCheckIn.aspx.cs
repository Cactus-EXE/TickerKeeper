using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalApplication
{
    public partial class RepairCheckIn : Page
    {
        // This is used to store the Customer ID between postbacks.
        protected int customerId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string customerIdString = Request.QueryString["CustomerId"];
                if (!string.IsNullOrEmpty(customerIdString) && int.TryParse(customerIdString, out customerId))
                {
                    // Store customerId in a hidden field to persist across postbacks
                    HiddenFieldCustomerId.Value = customerId.ToString();

                    string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    bool isCustomerValid = false;

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("SELECT COUNT(1) FROM Customer WHERE CustomerId = @CustomerId", con))
                        {
                            cmd.Parameters.AddWithValue("@CustomerId", customerId);

                            con.Open();
                            isCustomerValid = (int)cmd.ExecuteScalar() > 0;
                        }
                    }

                    if (isCustomerValid)
                    {
                        PopulateTypesDropdown();
                    }
                    else
                    {
                        Response.Write("Invalid Customer ID. Please select a valid customer.");
                    }
                }
                else
                {
                    Response.Write("No valid Customer ID provided.");
                }
            }
            else
            {
                // On postback, retrieve Customer ID from the hidden field
                if (int.TryParse(HiddenFieldCustomerId.Value, out customerId) == false)
                {
                    Response.Write("Invalid Customer ID on postback.");
                }
            }
        }




        protected void DropDownListType_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateMakesDropdown(DropDownListType.SelectedValue);
            DropDownListMake.Enabled = true;
            DropDownListModel.Items.Clear();
            DropDownListModel.Items.Insert(0, new ListItem("--Select Model--", ""));
            DropDownListModel.Enabled = false;
        }

        protected void DropDownListMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateModelsDropdown(DropDownListMake.SelectedValue);
            DropDownListModel.Enabled = true;
        }

        private void PopulateTypesDropdown()
        {
            DropDownListType.DataSource = GetTypesFromDatabase();
            DropDownListType.DataTextField = "TypeName";
            DropDownListType.DataValueField = "TypeId";
            DropDownListType.DataBind();
            DropDownListType.Items.Insert(0, new ListItem("--Select Type--", ""));
        }

        private void PopulateMakesDropdown(string typeId)
        {
            DropDownListMake.DataSource = GetMakesFromDatabase(typeId);
            DropDownListMake.DataTextField = "MakeName";
            DropDownListMake.DataValueField = "MakeId";
            DropDownListMake.DataBind();
            DropDownListMake.Items.Insert(0, new ListItem("--Select Make--", ""));
        }

        private void PopulateModelsDropdown(string makeId)
        {
            DropDownListModel.DataSource = GetModelsFromDatabase(makeId);
            DropDownListModel.DataTextField = "ModelName";
            DropDownListModel.DataValueField = "ModelId";
            DropDownListModel.DataBind();
            DropDownListModel.Items.Insert(0, new ListItem("--Select Model--", ""));
        }

        private DataTable GetTypesFromDatabase()
        {
            DataTable dtTypes = new DataTable();
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT TypeId, TypeName FROM DeviceTypes", con))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dtTypes);
                }
            }
            return dtTypes;
        }

        private DataTable GetMakesFromDatabase(string typeId)
        {
            DataTable dtMakes = new DataTable();
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT MakeId, MakeName FROM Makes WHERE TypeId = @TypeId", con))
                {
                    cmd.Parameters.AddWithValue("@TypeId", typeId);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dtMakes);
                }
            }
            return dtMakes;
        }

        private DataTable GetModelsFromDatabase(string makeId)
        {
            DataTable dtModels = new DataTable();
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT ModelId, ModelName FROM Models WHERE MakeId = @MakeId", con))
                {
                    cmd.Parameters.AddWithValue("@MakeId", makeId);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dtModels);
                }
            }
            return dtModels;
        }

        protected void SubmitForm_Click(object sender, EventArgs e)
        {
            // Retrieve Customer ID directly from the hidden field
            if (!int.TryParse(HiddenFieldCustomerId.Value, out customerId))
            {
                Response.Write("Invalid Customer ID. Please select a valid customer.");
                return;
            }

            // Retrieve customer information
            string firstName = string.Empty;
            string lastName = string.Empty;

            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string customerQuery = "SELECT FirstName, LastName FROM Customer WHERE CustomerId = @CustomerId";
                using (SqlCommand cmd = new SqlCommand(customerQuery, con))
                {
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            firstName = reader["FirstName"].ToString();
                            lastName = reader["LastName"].ToString();
                        }
                        else
                        {
                            Response.Write("Customer not found.");
                            return;
                        }
                    }
                }
            }

            // Retrieve form values
            int typeId = Convert.ToInt32(DropDownListType.SelectedValue);
            int makeId = Convert.ToInt32(DropDownListMake.SelectedValue);
            int modelId = Convert.ToInt32(DropDownListModel.SelectedValue);
            string problemDescription = description.Text.Trim();

            // Insert into the Repairs table and return the newly created RepairId
            int newRepairId;
            string query = @"
        INSERT INTO Repairs (CustomerId, FirstName, LastName, TypeId, MakeId, ModelId, ProblemDescription, DateCreated, DateUpdated) 
        VALUES (@CustomerId, @FirstName, @LastName, @TypeId, @MakeId, @ModelId, @ProblemDescription, GETDATE(), GETDATE());
        SELECT SCOPE_IDENTITY();";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);
                    cmd.Parameters.AddWithValue("@FirstName", firstName);
                    cmd.Parameters.AddWithValue("@LastName", lastName);
                    cmd.Parameters.AddWithValue("@TypeId", typeId);
                    cmd.Parameters.AddWithValue("@MakeId", makeId);
                    cmd.Parameters.AddWithValue("@ModelId", modelId);
                    cmd.Parameters.AddWithValue("@ProblemDescription", problemDescription);

                    con.Open();
                    newRepairId = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }

            // Redirect to the TicketDetails page with the newly created RepairId
            Response.Redirect($"TicketDetails.aspx?RepairId={newRepairId}");
        }

      
    }
}
