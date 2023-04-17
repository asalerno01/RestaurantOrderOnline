﻿using SalernoServer.Models;

namespace Server.Models
{
    public class SavedOrderOrderItemDTO
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public int Count { get; set; }
        public List<OrderItemAddon> Addons { get; set; } = new();
        public List<OrderItemNoOption> NoOptions { get; set; } = new();
        public List<OrderItemGroup> Groups { get; set; } = new();
    }
}