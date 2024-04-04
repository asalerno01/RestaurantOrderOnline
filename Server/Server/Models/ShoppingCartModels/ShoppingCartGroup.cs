using SalernoServer.Models.ItemModels;
using Server.Models.ItemModels.SnapshotModels;

namespace Server.Models.ShoppingCartModels
{
	public class ShoppingCartGroup : BaseModel
	{
		public long ShoppingCartGroupId { get; set; }
		public Group Group { get; set; }
		public GroupOption GroupOption { get; set; }
		public ShoppingCartItem ShoppingCartItem { get; set; }
	}
}
