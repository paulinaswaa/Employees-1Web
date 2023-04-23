using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace EmployeeList.Pages.Employees
{
    public class editModel : PageModel
    {
        public EmployeesInfo employeesInfo = new EmployeesInfo();
        public String errorMessage = "";
        public String correctMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String ConnectionString = "Server=DESKTOP-HR20LU4; Database=ListOfEmployees; Trusted_Connection=true;";


                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    sqlConnection.Open();
                    String sql = "SELECT * FROM TEST3 WHERE EmployeeID=@id";
                    using (SqlCommand cmd = new SqlCommand(sql, sqlConnection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                //EmployeesInfo info = new EmployeesInfo();
                                employeesInfo.id = "" + reader.GetInt32(0);
                                employeesInfo.FirstName = reader.GetString(1);
                                employeesInfo.LastName = reader.GetString(2);
                                employeesInfo.Email = reader.GetString(3);
                                employeesInfo.Phone = reader.GetString(4);

                               // ListEmployees.Add(info);
                            }
                        }
                    }
                }

            }

            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            employeesInfo.id = Request.Form["id"];
            employeesInfo.FirstName = Request.Form["FirstName"];
            employeesInfo.LastName = Request.Form["LastName"];
            employeesInfo.Email = Request.Form["Email"];
            employeesInfo.Phone = Request.Form["Phone"];

            if (string.IsNullOrEmpty(employeesInfo.FirstName) || string.IsNullOrEmpty(employeesInfo.LastName) ||
                string.IsNullOrEmpty(employeesInfo.Email) || string.IsNullOrEmpty(employeesInfo.Phone))
            {
                errorMessage = "Complete the data!";
                return;
            }

            try
            {
                String ConnectionString = "Server=DESKTOP-HR20LU4; Database=ListOfEmployees; Trusted_Connection=true;";


                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    sqlConnection.Open();
                    String sql = "UPDATE TEST3 SET FirstName=@firstName, LastName=@lastName, Email=@email, Phone=@phone WHERE EmployeeID=@id";
                    using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@firstName", employeesInfo.FirstName);
                        sqlCommand.Parameters.AddWithValue("@lastName", employeesInfo.LastName);
                        sqlCommand.Parameters.AddWithValue("@email", employeesInfo.Email);
                        sqlCommand.Parameters.AddWithValue("@phone", employeesInfo.Phone);
                        sqlCommand.Parameters.AddWithValue("@id", employeesInfo.id);
                        sqlCommand.ExecuteNonQuery();

                    }
                }

            }

            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Employees/Index");

        }
    }
}
