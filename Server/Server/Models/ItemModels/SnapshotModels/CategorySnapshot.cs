using SalernoServer.Models.ItemModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.ItemModels.SnapshotModels
{
	public class CategorySnapshot : BaseModel
	{
		public long CategorySnapshotId { get; set; }
		public long CategoryId { get; set; }
		[ForeignKey("CategoryId")]
		public Category Category { get; set; }
		public string Name { get; set; }
		public List<ItemSnapshot> Items { get; set; } = new();
	}
}
