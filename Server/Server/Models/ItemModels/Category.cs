using SalernoServer.Models.ItemModels;
using System.Text.Json.Serialization;

namespace Server.Models.ItemModels
{
    public class Category : BaseModel
    {
        public long CategoryId { get; set; }
        public string Name { get; set; }
        public List<Item> Items { get; set; } = new();
        public Category(long categoryId, string name)
        {
            CategoryId = categoryId;
            Name = name;
        }

        public Category(string name)
        {
            Name = name;
        }
    }
}