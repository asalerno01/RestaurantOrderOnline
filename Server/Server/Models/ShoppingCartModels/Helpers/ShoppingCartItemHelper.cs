namespace Server.Models.ShoppingCartModels.Helpers
{
	public class ShoppingCartItemHelper
	{
		public long ShoppingCartItemId { get; set; }
		public string ItemId { get; set; }
		public int Count { get; set; }
		public List<ShoppingCartAddonHelper> Addons { get; set; }
		public List<ShoppingCartNoOptionHelper> NoOptions { get; set; }
		public List<ShoppingCartGroupHelper> Groups { get; set; }
	}
}
