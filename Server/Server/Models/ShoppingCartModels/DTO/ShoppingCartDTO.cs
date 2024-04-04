using Server.Models.Authentication;
using System.ComponentModel.DataAnnotations.Schema;
using Server.Models.ShoppingCartModels;

namespace Server.Models.ShoppingCartModels.DTO
{
	public class ShoppingCartDTO
	{
		public List<ShoppingCartItemDTO> ShoppingCartItems { get; set; }

		public ShoppingCartDTO(ShoppingCart shoppingCart)
		{
			ShoppingCartItems = shoppingCart.ShoppingCartItems.Select(x => new ShoppingCartItemDTO(x)).ToList();
		}
	}
}
