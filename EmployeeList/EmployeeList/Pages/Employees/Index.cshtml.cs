using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;
using System;

namespace EmployeeList.Pages.Employees
{
    public class IndexModel : PageModel
    {
        public List<EmployeesInfo> ListEmployees = new List<EmployeesInfo>();

        public void OnGet()
        {
            try
            {
                //SqlConnection connectionString = new SqlConnection(@"Data Source=DESKTOP-HR20LU4;Initial Catalog=ListOfEmployees;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                //String ConnectionString = "Data Source=DESKTOP-HR20LU4;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

                String ConnectionString = "Server=DESKTOP-HR20LU4; Database=ListOfEmployees; Trusted_Connection=true;";


                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    sqlConnection.Open();
                    String sql = "SELECT * FROM Employee";
                    using (SqlCommand cmd = new SqlCommand(sql, sqlConnection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                EmployeesInfo info = new EmployeesInfo();
                                info.id = reader.GetInt32(0).ToString();
                                info.FirstName = reader.GetString(1);
                                info.LastName = reader.GetString(2);
                                info.Email = reader.GetString(3);
                                info.Phone = reader.GetString(4);

                                ListEmployees.Add(info);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception: " + ex.ToString());
            }
        }

        public class EmployeesInfo
        {
            public string id;
            public string FirstName;
            public string LastName;
            public string Email;
            public string Phone;
        }
    }
}
