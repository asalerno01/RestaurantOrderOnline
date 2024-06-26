﻿using SalernoServer.Models.ItemModels;

namespace Server.Models.ItemModels.Helpers
{
    public class ItemHelper
    {
        public string ItemId { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Description { get; set; } = "";
        public string Department { get; set; }
        public CategoryHelper Category { get; set; }
        public string UPC { get; set; }
        public string SKU { get; set; }
        public decimal Price { get; set; }
        public bool Discountable { get; set; }
        public bool Taxable { get; set; }
        public bool TrackingInventory { get; set; }
        public decimal Cost { get; set; }
        public decimal AssignedCost { get; set; }
        public int Quantity { get; set; }
        public int ReorderTrigger { get; set; }
        public int RecommendedOrder { get; set; }
        public string Supplier { get; set; }
        public bool LiabilityItem { get; set; }
        public string LiabilityRedemptionTender { get; set; }
        public string TaxGroupOrRate { get; set; }
        public bool IsEnabled { get; set; } = true;

        public List<AddonHelper> Addons { get; set; }
        public List<NoOptionHelper> NoOptions { get; set; }
        public List<GroupHelper> Groups { get; set; }
    }
}
