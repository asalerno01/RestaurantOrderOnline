using Server.Models.Authentication;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.ShoppingCartModels
{
	public class ShoppingCart : BaseModel
	{
		public long ShoppingCartId { get; set; }
		//public string RefreshToken { get; set; }
		public long? AccountId { get; set; }
		//[ForeignKey("AccountId")]
		//public Account? Account { get; set; }
		public List<ShoppingCartItem> ShoppingCartItems { get; set; } = new();
	}
}
