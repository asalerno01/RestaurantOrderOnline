using SalernoServer.Models.ItemModels;

namespace Server.Models.ItemModels
{
    public class ItemModifierDTO
    {
        public string GUID { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string? Department { get; set; }
        public string? Category { get; set; }
        public string? UPC { get; set; }
        public string? SKU { get; set; }
        public decimal Price { get; set; }
        public bool Discountable { get; set; } = false;
        public bool Taxable { get; set; } = false;
        public bool TrackingInventory { get; set; } = false;
        public decimal Cost { get; set; }
        public decimal AssignedCost { get; set; }
        public int Quantity { get; set; }
        public int ReorderTrigger { get; set; }
        public int RecommendedOrder { get; set; }
        public DateTime? LastSoldDate { get; set; }
        public string? Supplier { get; set; }
        public bool LiabilityItem { get; set; } = false;
        public string? LiabilityRedemptionTender { get; set; }
        public string? TaxGroupOrRate { get; set; }
        public List<Modifier> Modifiers { get; set; } = new();
        // public string? Name means can be null
        public string? Secret { get; set; }
    }
}
