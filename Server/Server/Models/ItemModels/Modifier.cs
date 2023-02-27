using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SalernoServer.Models.ItemModels
{
    public class Modifier
    {
        public long ModifierId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = "";
        [JsonIgnore]
        public Item Item { get; set; }
        public List<Group> Groups { get; set; } = new();
        public List<Addon> Addons { get; set; } = new();
        public List<NoOption> NoOptions { get; set; } = new();
    }
}
