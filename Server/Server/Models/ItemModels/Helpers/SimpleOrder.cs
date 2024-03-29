﻿using Server.Models.Authentication;
using Server.Models.OrderModels;

namespace Server.Models.ItemModels.Helpers
{
    public class SimpleOrder
    {
        public long OrderId { get; set; }
        public string Status { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OrderDate { get; set; }
        public string OrderTime { get; set; }
        public SimpleOrder(Order order)
        {
            OrderId = order.OrderId;
            Status = order.Status;
            FirstName = (order.Account is null) ? "New" : order.Account.FirstName;
            LastName = (order.Account is null) ? "Customer" : order.Account.LastName;
            OrderDate = order.OrderDate.ToShortDateString();
            OrderTime = order.OrderDate.ToShortTimeString();
        }
    }
}
