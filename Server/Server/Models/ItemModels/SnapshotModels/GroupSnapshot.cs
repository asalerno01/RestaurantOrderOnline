using Newtonsoft.Json;
using SalernoServer.Models.ItemModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.ItemModels.SnapshotModels
{
	public class GroupSnapshot : BaseModel
	{
		public long GroupSnapshotId { get; set; }
		public long GroupId { get; set; }
		[ForeignKey("GroupId")]
		[JsonIgnore]
		public Group Group { get; set; }
		public string Name { get; set; }
	}
}
