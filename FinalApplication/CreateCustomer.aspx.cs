using System;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace FinalApplication
{
    public partial class CreateCustomer : System.Web.UI.Page
    {
        protected void ButtonCreateCustomer_Click(object sender, EventArgs e)
        {
            // Your existing connection string from Web.config
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            // Sanitize user input
            string firstName = TextBoxFirstName.Text.Trim();
            string lastName = TextBoxLastName.Text.Trim();
            string phoneNumber = TextBoxPhoneNumber.Text.Trim();
            string email = TextBoxEmail.Text.Trim();
            string address = TextBoxAddress.Text.Trim();
            string city = TextBoxCity.Text.Trim();
            string state = TextBoxState.Text.Trim();
            string postalCode = TextBoxPostalCode.Text.Trim();
            string country = TextBoxCountry.Text.Trim();
            string customerNotes = TextBoxCustomerNotes.Text.Trim();

            // SQL injection prevention with parameterized query
            string query = @"INSERT INTO Customer (FirstName, LastName, PhoneNumber, Email, Address, City, State, PostalCode, Country, CustomerNotes) 
                             VALUES (@FirstName, @LastName, @PhoneNumber, @Email, @Address, @City, @State, @PostalCode, @Country, @CustomerNotes);
                             SELECT SCOPE_IDENTITY();"; // Fetch the CustomerId of the newly created record

            int newCustomerId = 0;

            // Execute the query and fetch the new CustomerId
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@FirstName", firstName);
                    cmd.Parameters.AddWithValue("@LastName", lastName);
                    cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.Parameters.AddWithValue("@City", city);
                    cmd.Parameters.AddWithValue("@State", state);
                    cmd.Parameters.AddWithValue("@PostalCode", postalCode);
                    cmd.Parameters.AddWithValue("@Country", country);
                    cmd.Parameters.AddWithValue("@CustomerNotes", customerNotes);

                    con.Open();
                    newCustomerId = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }

            // Redirect to the CustomerDetails page with the newly created CustomerId
            Response.Redirect($"CustomerDetails.aspx?CustomerId={newCustomerId}");
        }
    }
}
