namespace SalernoServer.Models.ItemModels
{
    // Hide what client shouldn't see
    public class ItemDTO
    {
        public long Id { get; set; }
        public string? ItemUUID { get; set; }
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
        public int Quantity { get; set; } = 0;
        public int ReorderTrigger { get; set; } = 0;
        public int RecommendedOrder { get; set; } = 0;
        public DateTime LastSoldDate { get; set; }
        public string? Supplier { get; set; }
        public bool LiabilityItem { get; set; } = false;
        public string? LiabilityRedemptionTender { get; set; }
        public string? TaxGroupOrRate { get; set; }
    }
}
