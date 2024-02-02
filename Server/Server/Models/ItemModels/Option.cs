using Microsoft.EntityFrameworkCore;

namespace Server.Models.ItemModels
{
    public class Option : BaseModel
    {
        public long OptionId { get; set; }
        public string Name { get; set; }
		[Precision(18, 2)]
		public decimal Price { get; set; }
    }
}
