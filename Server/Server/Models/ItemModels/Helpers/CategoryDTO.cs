﻿namespace Server.Models.ItemModels.Helpers
{
    public class CategoryDTO
    {
        public long CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<CategoryItemDTO> Items { get; set; } = new();
    }
}
