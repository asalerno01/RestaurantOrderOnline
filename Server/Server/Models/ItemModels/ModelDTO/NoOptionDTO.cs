using SalernoServer.Models.ItemModels;
using Server.Models.ItemModels.SnapshotModels;

namespace Server.Models.ItemModels.ModelDTO
{
	public class NoOptionDTO
	{
		public long NoOptionId { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }

		public NoOptionDTO(NoOption noOption)
		{
			NoOptionId = noOption.NoOptionId;
			Name = noOption.Name;
			Price = noOption.Price;
		}
	}
}
