using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace FinalApplication
{
    public partial class EditCustomer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string customerId = Request.QueryString["CustomerId"];
                if (!string.IsNullOrEmpty(customerId))
                {
                    LoadCustomerData(customerId);
                }
            }
        }

        private void LoadCustomerData(string customerId)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Customer WHERE CustomerId = @CustomerId", con))
                {
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtFirstName.Text = reader["FirstName"].ToString();
                        txtLastName.Text = reader["LastName"].ToString();
                        txtPhoneNumber.Text = reader["PhoneNumber"].ToString();
                        txtEmail.Text = reader["Email"].ToString();
                        txtAddress.Text = reader["Address"].ToString();
                        txtCity.Text = reader["City"].ToString();
                        txtState.Text = reader["State"].ToString();
                        txtPostalCode.Text = reader["PostalCode"].ToString();
                        txtCountry.Text = reader["Country"].ToString();
                        txtCustomerNotes.Text = reader["CustomerNotes"].ToString();
                    }
                    reader.Close();
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE Customer SET FirstName = @FirstName, LastName = @LastName, PhoneNumber = @PhoneNumber, Email = @Email, Address = @Address, City = @City, State = @State, PostalCode = @PostalCode, Country = @Country, CustomerNotes = @CustomerNotes WHERE CustomerId = @CustomerId", con))
                {
                    cmd.Parameters.AddWithValue("@CustomerId", Request.QueryString["CustomerId"]);
                    cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                    cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
                    cmd.Parameters.AddWithValue("@PhoneNumber", txtPhoneNumber.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@City", txtCity.Text);
                    cmd.Parameters.AddWithValue("@State", txtState.Text);
                    cmd.Parameters.AddWithValue("@PostalCode", txtPostalCode.Text);
                    cmd.Parameters.AddWithValue("@Country", txtCountry.Text);
                    cmd.Parameters.AddWithValue("@CustomerNotes", txtCustomerNotes.Text);

                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        Response.Redirect("CustomerDetails.aspx?CustomerId=" + Request.QueryString["CustomerId"]);
                    }
                    else
                    {
                        Response.Write("Update failed.");
                    }
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            // Redirect back without making any changes
            Response.Redirect("CustomerDetails.aspx?CustomerId=" + Request.QueryString["CustomerId"]);
        }
    }
}
