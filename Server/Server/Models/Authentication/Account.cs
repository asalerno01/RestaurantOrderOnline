using SalernoServer.Models.Authentication;
using SalernoServer.Models;
using System.Text.Json.Serialization;

namespace Server.Models.Authentication
{
    public class Account
    {
        public long AccountId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string RefreshToken { get; set; } = "";
        [JsonIgnore]
        public List<Order> Orders { get; set; }
        [JsonIgnore]
        public List<Review> Reviews { get; set; }
        [JsonIgnore]
        public List<SavedOrder> SavedOrders { get; set; }
    }
}
