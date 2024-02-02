using Server.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SalernoServer.Models.ItemModels
{
    public class Modifier : BaseModel
    {
        public long ModifierId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ItemId { get; set; }
        [ForeignKey("ItemId")]
        [JsonIgnore]
        public Item Item { get; set; }
        public List<Group> Groups { get; set; } = new();
        public List<Addon> Addons { get; set; } = new();
        public List<NoOption> NoOptions { get; set; } = new();
    }
}
