﻿using Microsoft.EntityFrameworkCore;

namespace Server.Models.OrderModels
{
    public class OrderItemDTO
    {
        public long OrderItemId { get; set; }
        public long OrderId { get; set; }
        public string ItemId { get; set; }
        public string Name { get; set; }
        [Precision(18, 2)]
        public decimal Price { get; set; }
        public int Count { get; set; }
        public List<OrderItemAddon> Addons { get; set; }
        public List<OrderItemNoOption> NoOptions { get; set; }
        public List<OrderItemGroup> Groups { get; set; }
        public OrderItemDTO(OrderItem orderItem)
        {
            OrderItemId = orderItem.OrderItemId;
            OrderId = orderItem.Order.OrderId;
            ItemId = orderItem.Item.ItemId;
            Count = orderItem.Count;
            Addons = orderItem.Addons;
            NoOptions = orderItem.NoOptions;
            Groups = orderItem.Groups;
        }
    }
}
