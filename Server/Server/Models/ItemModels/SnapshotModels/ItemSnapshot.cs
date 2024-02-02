using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SalernoServer.Models.ItemModels;
using Server.Models.ItemModels;
using Server.Models.ItemModels.SnapshotModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.ItemModels.SnapshotModels
{
    public class ItemSnapshot : BaseModel
    {
		public long ItemSnapshotId { get; set; }
		public string ItemId { get; set; }
		[ForeignKey("ItemId")]
		[JsonIgnore]
		public Item Item { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Department { get; set; }
		public CategorySnapshot Category { get; set; }
		public string UPC { get; set; }
		public string SKU { get; set; }
		[Precision(18, 2)]
		public decimal Price { get; set; }
		public bool Discountable { get; set; }
		public bool Taxable { get; set; } = false;
		public bool TrackingInventory { get; set; }
		[Precision(18, 2)]
		public decimal Cost { get; set; }
		[Precision(18, 2)]
		public decimal AssignedCost { get; set; }
		public int Quantity { get; set; }
		public int ReorderTrigger { get; set; }
		public int RecommendedOrder { get; set; }
		public DateTime LastSoldDate { get; set; }
		public string Supplier { get; set; }
		public bool LiabilityItem { get; set; }
		public string LiabilityRedemptionTender { get; set; }
		public string TaxGroupOrRate { get; set; }
		public bool IsEnabled { get; set; }
		[JsonIgnore]
        public ModifierSnapshot Modifier { get; set; } = new();

    }
}
