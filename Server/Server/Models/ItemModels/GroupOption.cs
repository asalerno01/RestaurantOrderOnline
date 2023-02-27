using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SalernoServer.Models.ItemModels
{
    public class GroupOption
    {
        public long GroupOptionId { get; set; }
        public string Name { get; set; }
        // https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-version-history#c-version-60
        // https://stackoverflow.com/questions/19811180/best-data-annotation-for-a-decimal18-2

        public decimal Price { get; set; } = 0;
        [JsonIgnore]
        public Group Group { get; set; }
    }
}
