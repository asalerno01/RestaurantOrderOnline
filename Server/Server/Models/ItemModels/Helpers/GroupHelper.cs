using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SalernoServer.Models.ItemModels
{
    public class GroupHelper
    {
        public long GroupId { get; set; }
        public string Name { get; set; }
        public List<GroupOptionHelper> GroupOptions { get; set; } = new();
    }
}
