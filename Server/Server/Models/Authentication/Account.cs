using Newtonsoft.Json;
using Server.Models.Authentication;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SalernoServer.Models.Authentication
{
    public class Account
    {
        public long AccountId { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public Employee? Employee { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string RefreshToken { get; set; } = "";
    }
}
