using Microsoft.EntityFrameworkCore;
using Server.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SalernoServer.Models.ItemModels
{
    public class NoOption : BaseModel
    {
        public long NoOptionId { get; set; }
        public string Name { get; set; }
		[Precision(18, 2)]
		public decimal Price { get; set; }
        [JsonIgnore]
        public Modifier Modifier { get; set; }
    }
}
