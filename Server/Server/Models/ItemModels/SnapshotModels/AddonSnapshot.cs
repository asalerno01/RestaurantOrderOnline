using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SalernoServer.Models.ItemModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.ItemModels.SnapshotModels
{
	public class AddonSnapshot : BaseModel
	{
		public long AddonSnapshotId { get; set; }
		public long AddonId { get; set; }
		[ForeignKey("AddonId")]
		[JsonIgnore]
		public Addon Addon { get; set; }
		public string Name { get; set; }
		[Precision(18, 2)]
		public decimal Price { get; set; }
		[JsonIgnore]
		public ModifierSnapshot Modifier { get; set; }
	}
}
