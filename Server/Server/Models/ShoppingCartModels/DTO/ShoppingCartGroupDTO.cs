using SalernoServer.Models.ItemModels;
using Server.Models.ItemModels.SnapshotModels;

namespace Server.Models.ShoppingCartModels.DTO
{
	public class ShoppingCartGroupDTO
	{
		public long GroupId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public ShoppingCartGroupOptionDTO GroupOption { get; set; }

		public ShoppingCartGroupDTO(Group group, GroupOption groupOption)
		{
			GroupId = group.GroupId;
			Name = group.Name;
			Description = group.Description;
			GroupOption = new ShoppingCartGroupOptionDTO(groupOption);
		}
	}
}
