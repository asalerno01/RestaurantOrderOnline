using Microsoft.AspNetCore.Mvc;
using SalernoServer.Models.Authentication;
using SalernoServer.Models;
using Microsoft.EntityFrameworkCore;
using Server.Models.ItemModels.Helpers;
using Server.Models.ItemModels;
using Server.Models.ItemModels.ModelDTO;

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
                .Include(c => c.Items)
                .ToListAsync();

            return Ok(categories.Select(category => new MenuCategoryDTO(category)).ToList());
        }
        
        public class MenuCategoryDTO
        {
            public string Name { get; set; }
            public List<SimpleItemDTO> Items { get; set; }
            public MenuCategoryDTO(Category category)
            {
                Name = category.Name;
                Items = category.Items.Select(item => new SimpleItemDTO(item)).ToList();
            }
        }
    }
}
