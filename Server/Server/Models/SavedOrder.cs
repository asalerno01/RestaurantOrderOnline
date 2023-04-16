using SalernoServer.Models;
using Server.Models.Authentication;

namespace Server.Models
{
    public class SavedOrder
    {
        public string SavedOrderName { get; set; }
        public CustomerAccount CustomerAccount { get; set; }
        public Order Order { get; set; }
    }
}
