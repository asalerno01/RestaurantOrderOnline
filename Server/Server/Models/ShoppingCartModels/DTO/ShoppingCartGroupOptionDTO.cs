using SalernoServer.Models.ItemModels;
using Server.Models.ItemModels.SnapshotModels;

namespace Server.Models.ShoppingCartModels.DTO
{
	public class ShoppingCartGroupOptionDTO
	{
		public long GroupOptionId { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }

		public ShoppingCartGroupOptionDTO(GroupOption groupOption)
		{
			GroupOptionId = groupOption.GroupOptionId;
			Name = groupOption.Name;
			Price = groupOption.Price;
		}
	}
}
