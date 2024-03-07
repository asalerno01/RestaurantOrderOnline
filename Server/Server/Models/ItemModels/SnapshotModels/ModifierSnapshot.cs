using SalernoServer.Models.ItemModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.ServiceModel.Description;
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
	}
}
