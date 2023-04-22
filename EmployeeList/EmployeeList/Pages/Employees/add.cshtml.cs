using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace EmployeeList.Pages.Employees
{
    public class addModel : PageModel
    {
        public EmployeesInfo employeesInfo = new EmployeesInfo();
        public string errorMessage = ""; 
        public string correctMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
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
                string connectionString = "Server=DESKTOP-HR20LU4; Database=ListOfEmployees; Trusted_Connection=true;";
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    string sql = "INSERT INTO TEST3(FirstName, LastName, Email, Phone) VALUES (@firstName, @lastName, @email, @phone)";
                    using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@firstName", employeesInfo.FirstName);
                        sqlCommand.Parameters.AddWithValue("@lastName", employeesInfo.LastName);
                        sqlCommand.Parameters.AddWithValue("@email", employeesInfo.Email);
                        sqlCommand.Parameters.AddWithValue("@phone", employeesInfo.Phone);
                        sqlCommand.ExecuteNonQuery();
                    }
                }

                employeesInfo.FirstName = "";
                employeesInfo.LastName = "";
                employeesInfo.Email = "";
                employeesInfo.Phone = "";
                correctMessage = "Added correctly!";
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
