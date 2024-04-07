using SalernoServer.Models.Authentication;
using System.Text.Json.Serialization;
using Server.Models.OrderModels;
using Server.Old;

namespace Server.Models.Authentication
{
    public class Account : BaseModel
	{
        public long AccountId { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsVerified { get; set; } = false;
        public string RefreshToken { get; set; } = "";
        [JsonIgnore]
        public List<Order> Orders { get; set; }
    }
}
