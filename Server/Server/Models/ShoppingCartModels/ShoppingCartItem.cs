using SalernoServer.Models.ItemModels;
using Server.Models.ItemModels.SnapshotModels;

namespace Server.Models.ShoppingCartModels
{
	public class ShoppingCartItem : BaseModel
	{
		public long ShoppingCartItemId { get; set; }
		public long ShoppingCartId { get; set; }
		public int Count { get; set; }
		public Item Item { get; set; }
		public List<ShoppingCartAddon> Addons { get; set; } = new();
		public List<ShoppingCartGroup> Groups { get; set; } = new();
		public List<ShoppingCartNoOption> NoOptions { get; set; } = new();
	}
}
