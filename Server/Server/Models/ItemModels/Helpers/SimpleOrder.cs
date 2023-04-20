using SalernoServer.Models;
using Server.Models.Authentication;

namespace Server.Models.ItemModels.Helpers
{
    public class SimpleOrder
    {
        public long OrderId { get; set; }
        public string Status { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime OrderDate { get; set; }
        public SimpleOrder(Order order)
        {
            OrderId = order.OrderId;
            Status = order.Status;
            FirstName = (order.Account is null) ? "New" : order.Account.FirstName;
            LastName = (order.Account is null) ? "Customer" : order.Account.LastName;
            OrderDate = order.OrderDate;
        }
    }
}
