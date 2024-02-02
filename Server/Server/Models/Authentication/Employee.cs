using Server.Models;
using System.ComponentModel.DataAnnotations;

namespace SalernoServer.Models.Authentication
{
    public class Employee : BaseModel
	{
        public long EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; } = "";
        public string Email { get; set; } = "";
        public bool BackOfficeAccess { get; set; } = false;
        public int RegisterCode { get; set; }
        public string EmployeeRole { get; set; } = "Cashier";

    }
}
