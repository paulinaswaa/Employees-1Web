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


            if (employeesInfo.Email.Length == 0 || employeesInfo.Phone.Length == 0 ||
                employeesInfo.LastName.Length == 0 || employeesInfo.LastName.Length == 0)
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
                    String sql = "INSERT INTO Employee(FirstName, LastName, Email, Phone) VALUES (@firstName, @lastName, @email, @phone)";

                    using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue(@"firstName", employeesInfo.FirstName);
                        sqlCommand.Parameters.AddWithValue(@"lastName", employeesInfo.LastName);
                        sqlCommand.Parameters.AddWithValue(@"email", employeesInfo.Email);
                        sqlCommand.Parameters.AddWithValue(@"phone", employeesInfo.Phone);
                        sqlCommand.ExecuteNonQuery();


                    }

                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            employeesInfo.FirstName = "";
            employeesInfo.LastName = "";
            employeesInfo.Email = "";
            employeesInfo.Phone = "";
            correctMessage = "Added correctly!";

            Response.Redirect("/Employees/Index");
        }


            
            
    }
}
