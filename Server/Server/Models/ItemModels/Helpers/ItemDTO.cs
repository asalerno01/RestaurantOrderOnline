using SalernoServer.Models.ItemModels;

namespace Server.Models.ItemModels.Helpers
{
    public class ItemDTO
    {
        public ItemDTO(Item item)
        {
            ItemId = item.ItemId;
            Name = item.Name;
            Description = item.Description;
            Department = item.Department;
            CategoryName = item.Category.Name;
            UPC = item.UPC;
            SKU = item.SKU;
            Price = item.Price;
            Discountable = item.Discountable;
            Taxable = item.Taxable;
            TrackingInventory = item.TrackingInventory;
            Cost = item.Cost;
            AssignedCost = item.AssignedCost;
            Quantity = item.Quantity;
            ReorderTrigger = item.ReorderTrigger;
            RecommendedOrder = item.RecommendedOrder;
            LastSoldDate = item.LastSoldDate;
            Supplier = item.Supplier;
            LiabilityItem = item.LiabilityItem;
            LiabilityRedemptionTender = item.LiabilityRedemptionTender;
            TaxGroupOrRate = item.TaxGroupOrRate;
            Modifier = item.Modifier;
        }
        public string ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Department { get; set; }
        public String CategoryName { get; set; }
        public string UPC { get; set; }
        public string SKU { get; set; }
        public decimal Price { get; set; }
        public bool Discountable { get; set; } = false;
        public bool Taxable { get; set; } = false;
        public bool TrackingInventory { get; set; } = false;
        public decimal Cost { get; set; }
        public decimal AssignedCost { get; set; }
        public int Quantity { get; set; }
        public int ReorderTrigger { get; set; }
        public int RecommendedOrder { get; set; }
        public DateTime LastSoldDate { get; set; } = DateTime.Now;
        public string Supplier { get; set; }
        public bool LiabilityItem { get; set; } = false;
        public string LiabilityRedemptionTender { get; set; }
        public string TaxGroupOrRate { get; set; }
        public virtual Modifier Modifier { get; set; } = new();

        private static CategoryHelper CategoryToCategoryHelper(Category category)
        {
            var categoryHelper = new CategoryHelper()
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Description = category.Description
            };
            return categoryHelper;
        }
    }
}
