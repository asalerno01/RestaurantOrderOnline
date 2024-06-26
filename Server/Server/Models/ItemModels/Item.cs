﻿using Microsoft.EntityFrameworkCore;
using Server.Models;
using Server.Models.ItemModels;
using Server.Models.ItemModels.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SalernoServer.Models.ItemModels
{
    public class Item : BaseModel
    {
        public string ItemId { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Description { get; set; } = "";
        public string Department { get; set; }
        public long CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [JsonIgnore]
        public Category Category { get; set; }
        public string UPC { get; set; }
        public string SKU { get; set; }
		[Precision(18, 2)]
		public decimal Price { get; set; }
        public bool Discountable { get; set; } = false;
        public bool Taxable { get; set; } = false;
        public bool TrackingInventory { get; set; } = false;
		[Precision(18, 2)]
		public decimal Cost { get; set; }
		[Precision(18, 2)]
		public decimal AssignedCost { get; set; }
        public int Quantity { get; set; }
        public int ReorderTrigger { get; set; }
        public int RecommendedOrder { get; set; }
        public string Supplier { get; set; }
        public bool LiabilityItem { get; set; } = false;
        public string LiabilityRedemptionTender { get; set; }
        public string TaxGroupOrRate { get; set; }
        public bool IsEnabled { get; set; } = true;

		public List<Group> Groups { get; set; } = new();
		public List<Addon> Addons { get; set; } = new();
		public List<NoOption> NoOptions { get; set; } = new();

	}
}
