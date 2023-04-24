namespace Server.Models.ItemModels.Helpers
{
    public class CategoryItem
    {
        public long ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsEnabled { get; set; }
        public CategoryItem(long itemId, string name, string description, decimal price, bool isEnabled)
        {
            ItemId = itemId;
            Name = name;
            Description = description;
            Price = price;
            IsEnabled = isEnabled;
        }
    }
}
