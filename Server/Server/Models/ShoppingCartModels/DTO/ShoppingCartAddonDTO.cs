using SalernoServer.Models.ItemModels;
using Server.Models.ItemModels.SnapshotModels;

namespace Server.Models.ShoppingCartModels.DTO
{
	public class ShoppingCartAddonDTO
	{
		public long AddonId { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }

		public ShoppingCartAddonDTO(Addon addon)
		{
			AddonId = addon.AddonId;
			Name = addon.Name;
			Price = addon.Price;
		}
	}
}
