using SalernoServer.Models.ItemModels;
using Server.Models.ItemModels.Helpers;
using System.Text.Json.Serialization;

namespace Server.Models.ItemModels.ModelDTO
{
	public class SimpleItemDTO
	{
		public string ItemId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public long CategoryId { get; set; }
		//public CategoryHelper Category { get; set; }
		public decimal Price { get; set; }
		[JsonIgnore]
		public bool IsEnabled { get; set; }
		public ModifierDTO Modifier { get; set; }

		public SimpleItemDTO(Item item)
		{
			ItemId = item.ItemId;
			Name = item.Name;
			Description = item.Description;
			CategoryId = item.CategoryId;
			Price = item.Price;
			Modifier = new ModifierDTO(item.Modifier);
		}
	}
}
