using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SalernoServer.Models.ItemModels
{
    public class ModifierHelper
    {
        public List<ModifierGroupHelper> Groups { get; set; } = new();
        public List<ModifierAddonHelper> Addons { get; set; } = new();
        public List<ModifierNoOptionHelper> NoOptions { get; set; } = new();
    }
}
