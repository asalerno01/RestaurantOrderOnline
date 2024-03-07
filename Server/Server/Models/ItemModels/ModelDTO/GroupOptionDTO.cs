using SalernoServer.Models.ItemModels;

namespace Server.Models.ItemModels.ModelDTO
{
	public class GroupOptionDTO
	{
		public long GroupOptionId { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public bool IsDefault { get; set; }

		public GroupOptionDTO(GroupOption groupOption)
		{
			GroupOptionId = groupOption.GroupOptionId;
			Name = groupOption.Name;
			Price = groupOption.Price;
			IsDefault = groupOption.IsDefault;
		}
	}
}
