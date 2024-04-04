using SalernoServer.Models.ItemModels;
using Server.Models.ItemModels.SnapshotModels;

namespace Server.Models.ShoppingCartModels
{
	public class ShoppingCartAddon : BaseModel
	{
		public long ShoppingCartAddonId { get; set; }
		public Addon Addon { get; set; }
		public ShoppingCartItem ShoppingCartItem { get; set; }
	}
}
