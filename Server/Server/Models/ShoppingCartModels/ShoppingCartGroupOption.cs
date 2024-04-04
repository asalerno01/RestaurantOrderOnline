using SalernoServer.Models.ItemModels;
using Server.Models.ItemModels.SnapshotModels;

namespace Server.Models.ShoppingCartModels
{
	public class ShoppingCartGroupOption : BaseModel
	{
		public long ShoppingCartGroupOptionId { get; set; }
		public GroupOption GroupOption { get; set; }
		public ShoppingCartItem ShoppingCartItem { get; set; }
	}
}
