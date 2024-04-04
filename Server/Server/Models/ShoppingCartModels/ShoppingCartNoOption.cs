using SalernoServer.Models.ItemModels;
using Server.Models.ItemModels.SnapshotModels;

namespace Server.Models.ShoppingCartModels
{
	public class ShoppingCartNoOption : BaseModel
	{
		public long ShoppingCartNoOptionId { get; set; }
		public NoOption NoOption { get; set; }
		public ShoppingCartItem ShoppingCartItem { get; set; }
	}
}
