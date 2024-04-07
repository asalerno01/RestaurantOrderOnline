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
using Server.Models.ItemModels;
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
        public async Task<ActionResult<IEnumerable<FullItemDTO>>> GetItems()
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
            return Ok(items.Select(item => new FullItemDTO(item)).ToList());
        }
        [HttpGet]
        [Route("menu")]
        public async Task<ActionResult<IEnumerable<FullItemDTO>>> GetMenuItems()
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

            return Ok(items.Select(item => new FullItemDTO(item)).ToList());
        }

        // GET: api/items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FullItemDTO>> GetItem(string id)
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
            

            return Ok(new FullItemDTO(item));
        }
        // PUT: api/Items/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{itemId}")]
        public async Task<IActionResult> PutItem(string itemId, [FromBody] ItemHelper item)
        {
            if (itemId != item.ItemId) return BadRequest($"{item.ItemId}");

            var newItem = await _context.Items.FindAsync(itemId);
            if (newItem == null) return NotFound();

            var category = await _context.Categories.FindAsync(item.Category.CategoryId);
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

            newItem.Modifier.Addons.Clear();
            newItem.Modifier.NoOptions.Clear();
            newItem.Modifier.Groups.Clear();

            foreach (var addon in newItem.Modifier.Addons)
            {
                var foundAddon = await _context.Addons.FindAsync(addon.AddonId) ?? new()
                {
                    Name = addon.Name,
                    Price = addon.Price
                };
				newItem.Modifier.Addons.Add(foundAddon);
            }
			foreach (var noOption in newItem.Modifier.NoOptions)
			{
				var foundNoOption = await _context.NoOptions.FindAsync(noOption.NoOptionId) ?? new()
				{
					Name = noOption.Name,
					Price = noOption.Price
				};
				newItem.Modifier.NoOptions.Add(foundNoOption);
			}
			foreach (var group in newItem.Modifier.Groups)
			{
				var foundGroup = await _context.Groups.FindAsync(group.GroupId) ?? new()
				{
					Name = group.Name
				};
                foreach (var groupOption in group.GroupOptions)
                {
                    var foundGroupOption = await _context.GroupOptions.FindAsync(groupOption.GroupOptionId) ?? new()
                    {
                        Name = groupOption.Name,
                        Price = groupOption.Price
                    };
                    foundGroup.GroupOptions.Add(foundGroupOption);
                }
				newItem.Modifier.Groups.Add(foundGroup);
			}

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

            Category category;
            if (itemHelper.Category.CategoryId == 0)
            {
                category = new(itemHelper.Category.Name);
            }
            else
            {
				var foundCategory = await _context.Categories.FindAsync(itemHelper.Category.CategoryId);
				if (foundCategory is null) return BadRequest("Bad category");
                category = foundCategory;
			}

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
					var category = await _context.Categories.FindAsync(itemHelper.Category.CategoryId);
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
