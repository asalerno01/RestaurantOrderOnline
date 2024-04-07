using Microsoft.Build.Framework;
using Server.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SalernoServer.Models.ItemModels
{
    public class Group : BaseModel
    {
        public long GroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public List<GroupOption> GroupOptions { get; set; } = new();
    }
}
