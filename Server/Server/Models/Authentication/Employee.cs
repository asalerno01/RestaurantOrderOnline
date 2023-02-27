using System.ComponentModel.DataAnnotations;

namespace SalernoServer.Models.Authentication
{
    public class Employee
    {
        public long EmployeeId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public bool? BackOfficeAccess { get; set; } = false;
        [Required]
        public int RegisterCode { get; set; }
        [Required]
        public string EmployeeRole { get; set; } = "Cashier";

    }
}
