using SalernoServer.Models.ItemModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Server.Models.ItemModels.SnapshotModels
{
	public class ModifierSnapshot : BaseModel
	{
		public long ModifierSnapshotId { get; set; }
		public long ModifierId { get; set; }
		[ForeignKey("ModifierId")]
		[JsonIgnore]
		public Modifier Modifier { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string ItemId { get; set; }
		public long ItemSnapshotId { get; set; }
		[ForeignKey("ItemSnapshotId")]
		public ItemSnapshot Item { get; set; }
		public List<AddonSnapshot> Addons { get; set; } = new();
		public List<NoOptionSnapshot> NoOptions { get; set; } = new();
		public List<GroupSnapshot> Groups { get; set; } = new();
	}
}
