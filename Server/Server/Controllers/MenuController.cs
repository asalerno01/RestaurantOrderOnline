using Microsoft.AspNetCore.Mvc;
using SalernoServer.Models.Authentication;
using SalernoServer.Models;
using Microsoft.EntityFrameworkCore;
using Server.Models.ItemModels.Helpers;
using Server.Models.ItemModels;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MenuController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuCategoryDTO>>> GetMenu()
        {
            var categories = await _context.Categories
                .Include(c => c.Items)
                .ThenInclude(i => i.Modifier)
                .ThenInclude(m => m.Addons)
                .Include(c => c.Items)
                .ThenInclude(i => i.Modifier)
                .ThenInclude(m => m.NoOptions)
                .Include(c => c.Items)
                .ThenInclude(i => i.Modifier)
                .ThenInclude(m => m.Groups)
                .ThenInclude(g => g.GroupOptions)
                .ToListAsync();
            return Ok(categories.Select(category => new CategoryDTO(category)).ToList().Select(category => new MenuCategoryDTO(category)).Where(category => category.Items.Count > 0).ToList());
        }
        
        public class MenuCategoryDTO
        {
            public string Name { get; set; }
            public List<CategoryItemDTO> Items { get; set; } = new();
            public MenuCategoryDTO(CategoryDTO category)
            {
                Name = category.Name;
                Items = category.Items.Select(item => new CategoryItemDTO(item.ItemId, item.Name, item.Description, item.Price, item.Modifier, item.IsEnabled)).Where(item => item.IsEnabled).ToList();
            }
        }
    }
}
