using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SalernoServer.Models;
using SalernoServer.Models.ItemModels;
using Server.Models.ItemModels.Helpers;
using Server.Models.ItemModels.ModelDTO;

namespace SalernoServer.Controllers
{
    [Route("api/items")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ItemController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDTO>>> GetItems()
        {
            var items = await _context.Items
                .Include(i => i.Modifier)
                .ThenInclude(m => m.Addons)
                .Include(m => m.Modifier)
                .ThenInclude(m => m.NoOptions)
                .Include(m => m.Modifier)
                .ThenInclude(m => m.Groups)
                .ThenInclude(g => g.GroupOptions)
                .Include(i => i.Category)
                .OrderBy(i => i.Category)
                .ToListAsync();
            return Ok(items.Select(item => new ItemDTO(item)).ToList());
        }
        [HttpGet]
        [Route("menu")]
        public async Task<ActionResult<IEnumerable<ItemDTO>>> GetMenuItems()
        {
            var items = await _context.Items
                .Include(i => i.Modifier)
                .ThenInclude(m => m.Addons)
                .Include(m => m.Modifier)
                .ThenInclude(m => m.NoOptions)
                .Include(m => m.Modifier)
                .ThenInclude(m => m.Groups)
                .ThenInclude(g => g.GroupOptions)
                .Include(i => i.Category)
                .Where(item => item.IsEnabled)
                .OrderBy(i => i.Category)
                .ToListAsync();

            return Ok(items.Select(item => new ItemDTO(item)).ToList());
        }

        // GET: api/items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDTO>> GetItem(string id)
        {
            //var item = await _context.Items.FindAsync(itemId);
            var item = await _context.Items
                .Include(i => i.Modifier)
                .ThenInclude(m => m.Addons)
                .Include(m => m.Modifier)
                .ThenInclude(m => m.NoOptions)
                .Include(m => m.Modifier)
                .ThenInclude(m => m.Groups)
                .ThenInclude(g => g.GroupOptions)
                .Include(i => i.Category)
                .FirstOrDefaultAsync(i => i.ItemId.Equals(id));

            if (item == null) return NotFound();
            

            return Ok(new ItemDTO(item));
        }
        // PUT: api/Items/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(string id, [FromBody] ItemHelper item)
        {
            if (id != item.ItemId) return BadRequest($"{item.ItemId}");
            
            var newItem = await _context.Items.FindAsync(id);
            if (newItem == null) return NotFound();

            var category = await _context.Categories.FindAsync(item.CategoryId);
            if (category is null) return BadRequest("Bad category");

            newItem.ItemId = item.ItemId;
            newItem.Name = item.Name;
            newItem.Description = item.Description;
            newItem.Department = item.Department;
            newItem.Category = category;
            newItem.SKU = item.SKU;
            newItem.UPC = item.UPC;
            newItem.Price = item.Price;
            newItem.Discountable = item.Discountable;
            newItem.Taxable = item.Taxable;
            newItem.TrackingInventory = item.TrackingInventory;
            newItem.Cost = item.Cost;
            newItem.AssignedCost = item.AssignedCost;
            newItem.Quantity = item.Quantity;
            newItem.ReorderTrigger = item.ReorderTrigger;
            newItem.RecommendedOrder = item.RecommendedOrder;
            newItem.Supplier = item.Supplier;
            newItem.LiabilityItem = item.LiabilityItem;
            newItem.LiabilityRedemptionTender = item.LiabilityRedemptionTender;
            newItem.TaxGroupOrRate = item.TaxGroupOrRate;
            newItem.IsEnabled = item.IsEnabled;

            _context.Update(newItem);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("item")]
        public async Task<ActionResult> PostItem([FromBody] ItemHelper itemHelper)
        {
            if (itemHelper is null) return BadRequest("ItemHelper is null");

            bool itemExists = _context.Items.Any(i => i.ItemId.Equals(itemHelper.ItemId));
            if (itemExists) return BadRequest($"ItemId exists:{itemHelper.ItemId}");

            var category = await _context.Categories.FindAsync(itemHelper.CategoryId);
            if (category is null) return BadRequest("Bad category");

            Item newItem = new()
            {
                ItemId = itemHelper.ItemId.Equals("") ? Guid.NewGuid().ToString() : itemHelper.ItemId,
                Name = itemHelper.Name,
                Description = itemHelper.Description,
                Department = itemHelper.Department,
                Category = category,
                SKU = itemHelper.SKU,
                UPC = itemHelper.UPC,
                Price = itemHelper.Price,
                Discountable = itemHelper.Discountable,
                Taxable = itemHelper.Taxable,
                TrackingInventory = itemHelper.TrackingInventory,
                Cost = itemHelper.Cost,
                AssignedCost = itemHelper.AssignedCost,
                Quantity = itemHelper.Quantity,
                ReorderTrigger = itemHelper.ReorderTrigger,
                RecommendedOrder = itemHelper.RecommendedOrder,
                Supplier = itemHelper.Supplier,
                LiabilityItem = itemHelper.LiabilityItem,
                LiabilityRedemptionTender = itemHelper.LiabilityRedemptionTender,
                TaxGroupOrRate = itemHelper.TaxGroupOrRate,
                IsEnabled = itemHelper.IsEnabled
            };

            await _context.Items.AddAsync(newItem);
            await _context.SaveChangesAsync();

            return Ok();
        }

		[HttpPost]
        [Route("items")]
		public async Task<ActionResult> PostItems([FromBody] List<ItemHelper> itemHelpers)
		{
			foreach(ItemHelper itemHelper in itemHelpers)
            {
				bool itemExists = _context.Items.Any(i => i.ItemId.Equals(itemHelper.ItemId));
				if (!itemExists)
                {
					var category = await _context.Categories.FindAsync(itemHelper.CategoryId);
					if (category is null) return BadRequest("Bad category");

					Item newItem = new()
					{
						ItemId = itemHelper.ItemId.Equals("") ? Guid.NewGuid().ToString() : itemHelper.ItemId,
						Name = itemHelper.Name,
						Description = itemHelper.Description,
						Department = itemHelper.Department,
						Category = category,
						SKU = itemHelper.SKU,
						UPC = itemHelper.UPC,
						Price = itemHelper.Price,
						Discountable = itemHelper.Discountable,
						Taxable = itemHelper.Taxable,
						TrackingInventory = itemHelper.TrackingInventory,
						Cost = itemHelper.Cost,
						AssignedCost = itemHelper.AssignedCost,
						Quantity = itemHelper.Quantity,
						ReorderTrigger = itemHelper.ReorderTrigger,
						RecommendedOrder = itemHelper.RecommendedOrder,
						Supplier = itemHelper.Supplier,
						LiabilityItem = itemHelper.LiabilityItem,
						LiabilityRedemptionTender = itemHelper.LiabilityRedemptionTender,
						TaxGroupOrRate = itemHelper.TaxGroupOrRate,
						IsEnabled = itemHelper.IsEnabled
					};

					await _context.Items.AddAsync(newItem);
				}
			}

			await _context.SaveChangesAsync();

			return Ok();
		}

		// DELETE: api/Items/5
		[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(string id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
