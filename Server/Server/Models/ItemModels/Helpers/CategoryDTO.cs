using Server.Models.ItemModels.ModelDTO;

namespace Server.Models.ItemModels.Helpers
{
    public class CategoryDTO
    {
        public long CategoryId { get; set; }
        public string Name { get; set; }
        public List<FullItemDTO> Items { get; set; }
        public CategoryDTO(Category category)
        {
            CategoryId = category.CategoryId;
            Name = category.Name;
            Items = category.Items
                .Select(item => new FullItemDTO(item))
                .Where(item => item.IsEnabled)
                .ToList();
        }
        // TODO: Fix this. Remove where is enabled.
    }
}
