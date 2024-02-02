using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SalernoServer.Models.ItemModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.ItemModels.SnapshotModels
{
	public class NoOptionSnapshot : BaseModel
	{
		public long NoOptionSnapshotId { get; set; }
		public long NoOptionId { get; set; }
		[ForeignKey("NoOptionId")]
		[JsonIgnore]
		public NoOption NoOption { get; set; }
		public string Name { get; set; }
		[Precision(18, 2)]
		public decimal Price { get; set; }
		[JsonIgnore]
		public ModifierSnapshot Modifier { get; set; }
	}
}
