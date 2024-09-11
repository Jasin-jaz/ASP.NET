using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace crmapp.Pages.Customers
{
    public class Edit : PageModel
    {
        [BindProperty]
        public int Id { get; set; }
        [BindProperty, Required(ErrorMessage = "The First Name is required")]
        public string Firstname { get; set; } = "";
        [BindProperty, Required(ErrorMessage = "The Last Name is required")]
        public string Lastname { get; set; } = "";
        [BindProperty, Required, EmailAddress]
        public string Email { get; set; } = "";
        [BindProperty, Phone]
        public string? Phone { get; set; } = "";
        [BindProperty]
        public string? Address { get; set; } = "";
        [BindProperty, Required]
        public string Company { get; set; } = "";
        [BindProperty]
        public string? Notes { get; set; } = "";

        public string ErrorMessage { get; set; } = "";
        public void OnGet(int id)
        {
            try
            {
                string connectionString = "Server=.\\sqlexpress;Database=crmdb;Trusted_Connection=True;TrustServerCertificate=True;";
                using (SqlConnection connection = new SqlConnection(connectionString)) { 
                    connection.Open();

                    string sql = "SELECT * FROM customers WHERE id=@id;";
                    using (SqlCommand command = new SqlCommand(sql, connection)){
                        command.Parameters.AddWithValue("@id", id);

                        using(SqlDataReader reader = command.ExecuteReader()) {
                            if (reader.Read())
                            {
                                Id = reader.GetInt32(0);
                                Firstname = reader.GetString(1);
                                Lastname = reader.GetString(2);
                                Email = reader.GetString(3);
                                Phone = reader.GetString(4);
                                Address = reader.GetString(5);
                                Company = reader.GetString(6);
                                Notes = reader.GetString(7);
                            }
                            else
                            {
                                Response.Redirect("/Customers/Index");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               ErrorMessage = ex.Message;
            }
        }
    }
}