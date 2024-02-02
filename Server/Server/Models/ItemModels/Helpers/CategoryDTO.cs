namespace Server.Models.ItemModels.Helpers
{
    public class CategoryDTO : BaseModel
    {
        public long CategoryId { get; set; }
        public string Name { get; set; }
        public List<CategoryItemDTO> Items { get; set; }
        public CategoryDTO(Category category)
        {
            CategoryId = category.CategoryId;
            Name = category.Name;
            CreatedAt = category.CreatedAt;
            UpdatedAt = category.UpdatedAt;
            Items = category.Items.Select(item => new CategoryItemDTO(item.ItemId, item.Name, item.Description, item.Price, item.Modifier, item.IsEnabled)).Where(item => item.IsEnabled).ToList();
        }
    }
}
