using SalernoServer.Models.ItemModels;
using Server.Models.ItemModels.SnapshotModels;

namespace Server.Models.ShoppingCartModels.DTO
{
	public class ShoppingCartNoOptionDTO
	{
		public long NoOptionId { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }

		public ShoppingCartNoOptionDTO(NoOption noOption)
		{
			NoOptionId = noOption.NoOptionId;
			Name = noOption.Name;
			Price = noOption.Price;
		}
	}
}
