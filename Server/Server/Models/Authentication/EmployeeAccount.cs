using Newtonsoft.Json;
using Server.Models.Authentication;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using JsonIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;

namespace SalernoServer.Models.Authentication
{
    public class EmployeeAccount
    {
        public long EmployeeAccountId { get; set; }
        public Employee? Employee { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public string RefreshToken { get; set; } = "";
    }
}
