using SalernoServer.Models.ItemModels;
using Server.Models.ItemModels.SnapshotModels;

namespace Server.Models.ItemModels.ModelDTO
{
	public class GroupDTO
	{
		public long GroupId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public List<GroupOptionDTO> GroupOptions { get; set; }

		public GroupDTO(Group group)
		{
			GroupId = group.GroupId;
			Name = group.Name;
			Description = group.Description;
			GroupOptions = group.GroupOptions.Select(g => new GroupOptionDTO(g)).ToList();
		}
	}
}
