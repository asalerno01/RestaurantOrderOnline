using SalernoServer.Models.ItemModels;

namespace Server.Models.ItemModels.ModelDTO
{
	public class AddonDTO
	{
		public long AddonId { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }

		public AddonDTO(Addon addon)
		{
			AddonId = addon.AddonId;
			Name = addon.Name;
			Price = addon.Price;
		}
	}
}
