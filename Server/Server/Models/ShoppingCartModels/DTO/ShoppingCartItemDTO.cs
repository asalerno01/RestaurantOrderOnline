using Server.Models.ItemModels.ModelDTO;
using Server.Models.ItemModels.SnapshotModels;

namespace Server.Models.ShoppingCartModels.DTO
{
	public class ShoppingCartItemDTO
	{
		public long ShoppingCartItemId { get; set; }
		public int Count { get; set; }
		public SimpleItemDTO Item { get; set; }
		public List<ShoppingCartAddonDTO> Addons { get; set; }
		public List<ShoppingCartGroupDTO> Groups { get; set; }
		public List<ShoppingCartNoOptionDTO> NoOptions { get; set; }

		public ShoppingCartItemDTO(ShoppingCartItem shoppingCartItem)
		{
			ShoppingCartItemId = shoppingCartItem.ShoppingCartItemId;
			Count = shoppingCartItem.Count;
			Item = new SimpleItemDTO(shoppingCartItem.Item);
			Addons = shoppingCartItem.Addons.Select(x => new ShoppingCartAddonDTO(x.Addon)).ToList();
			NoOptions = shoppingCartItem.NoOptions.Select(x => new ShoppingCartNoOptionDTO(x.NoOption)).ToList();
			Groups = shoppingCartItem.Groups.Select(x => new ShoppingCartGroupDTO(x.Group, x.GroupOption)).ToList();
		}
	}
}
