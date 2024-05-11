using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;

namespace FinalApplication
{
    public class SearchResult
    {
        public string Type { get; set; }
        public int Id { get; set; }
        public string DisplayName { get; set; }
    }

    [RoutePrefix("api/search")]
    public class SearchController : ApiController
    {
        [HttpGet]
        [Route("query")]
        public IHttpActionResult Search([FromUri] string q)
        {
            if (string.IsNullOrEmpty(q))
            {
                return Ok(new List<SearchResult>());
            }

            string connectionString = "DefaultConnection";
            string searchSql = @"
                SELECT 'Customer' AS Type, CustomerId AS Id, CONCAT(FirstName, ' ', LastName) AS DisplayName
                FROM Customer
                WHERE FirstName LIKE '%' + @Query + '%' OR LastName LIKE '%' + @Query + '%'
                UNION ALL
                SELECT 'Repair' AS Type, RepairId AS Id, CONCAT(FirstName, ' ', LastName, ' - ', ProblemDescription) AS DisplayName
                FROM Repairs
                WHERE RepairId LIKE '%' + @Query + '%'
                    OR FirstName LIKE '%' + @Query + '%'
                    OR LastName LIKE '%' + @Query + '%'
            ";

            var results = new List<SearchResult>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(searchSql, conn);
                cmd.Parameters.AddWithValue("@Query", q);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    results.Add(new SearchResult
                    {
                        Type = reader["Type"].ToString(),
                        Id = Convert.ToInt32(reader["Id"]),
                        DisplayName = reader["DisplayName"].ToString()
                    });
                }
            }

            return Ok(results);
        }
    }
}
