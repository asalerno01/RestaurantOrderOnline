﻿using SalernoServer.Models.ItemModels;
using Server.Models.ItemModels.Helpers;
using Server.Models.ItemModels.SnapshotModels;

namespace Server.Models.ItemModels.ModelDTO
{
    public class FullItemDTO
    {
        public string ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Department { get; set; }
        //public long CategoryId { get; set; }
        public CategoryHelper Category { get; set; }
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
        public string Supplier { get; set; }
        public bool LiabilityItem { get; set; } = false;
        public string LiabilityRedemptionTender { get; set; }
        public string TaxGroupOrRate { get; set; }
        public bool IsEnabled { get; set; }
        public List<AddonDTO> Addons { get; set; }
        public List<NoOptionDTO> NoOptions { get; set; }
        public List<GroupDTO> Groups { get; set; }

        private static CategoryHelper CategoryToCategoryHelper(Category category)
        {
            var categoryHelper = new CategoryHelper()
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
            };
            return categoryHelper;
        }

		public FullItemDTO(Item item)
		{
			ItemId = item.ItemId;
			Name = item.Name;
			Description = item.Description;
			Department = item.Department;
			//CategoryId = item.CategoryId;
            Category = CategoryToCategoryHelper(item.Category);
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
			Supplier = item.Supplier;
			LiabilityItem = item.LiabilityItem;
			LiabilityRedemptionTender = item.LiabilityRedemptionTender;
			TaxGroupOrRate = item.TaxGroupOrRate;
			IsEnabled = item.IsEnabled;

            Addons = item.Addons.Select(addon => new AddonDTO(addon)).ToList();
            NoOptions = item.NoOptions.Select(noOption =>  new NoOptionDTO(noOption)).ToList();
            Groups = item.Groups.Select(group => new GroupDTO(group)).ToList();
		}
	}
}
