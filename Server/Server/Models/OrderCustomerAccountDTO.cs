using SalernoServer.Models;

namespace Server.Models
{
    public class OrderAccountDTO
    {
        public long? AccountId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
