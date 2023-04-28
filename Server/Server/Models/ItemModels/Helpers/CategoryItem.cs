namespace Server.Models.ItemModels.Helpers
{
    public class CategoryItem
    {
        public long ItemId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsEnabled { get; set; }
        public CategoryItem(long itemId, string name, decimal price, bool isEnabled)
        {
            ItemId = itemId;
            Name = name;
            Price = price;
            IsEnabled = isEnabled;
        }
    }
}
