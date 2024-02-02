using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SalernoServer.Models.ItemModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.ItemModels.SnapshotModels
{
	public class GroupOptionSnapshot : BaseModel
	{
		public long GroupOptionSnapshotId { get; set; }
		public long GroupOptionId { get; set; }
		[ForeignKey("GroupOptionId")]
		[JsonIgnore]
		public GroupOption GroupOption { get; set; }
		public string Name { get; set; }
		[Precision(18, 2)]
		public decimal Price { get; set; }
		public bool IsDefault { get; set; }
		[JsonIgnore]
		public GroupSnapshot Group { get; set; }
	}
}
