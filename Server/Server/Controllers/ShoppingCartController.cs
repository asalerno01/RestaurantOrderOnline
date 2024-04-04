using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalernoServer.Models;
using SalernoServer.Models.ItemModels;
using Server.Logger;
using Server.Models.OrderModels;
using Server.Models.ShoppingCartModels;
using Server.Models.ShoppingCartModels.DTO;
using Server.Models.ShoppingCartModels.Helpers;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace Server.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ShoppingCartController : ControllerBase
	{
		private readonly AppDbContext _context;

		public ShoppingCartController(AppDbContext context)
		{
			_context = context;
		}

		// GET: api/shoppingcart/1
		[HttpGet("{shoppingCartId}")]
		public async Task<ActionResult<Order>> GetShoppingCart(long shoppingCartId)
		{
			var shoppingCart = await _context.ShoppingCarts
				.Include(s => s.ShoppingCartItems)
				.ThenInclude(si => si.Addons)
				.ThenInclude(a => a.Addon)
				.Include(s => s.ShoppingCartItems)
				.ThenInclude(si => si.NoOptions)
				.ThenInclude(n => n.NoOption)
				.Include(s => s.ShoppingCartItems)
				.ThenInclude(si => si.Groups)
				.ThenInclude(g => g.GroupOption)
				.AsSplitQuery()
				.FirstOrDefaultAsync(s => s.ShoppingCartId == shoppingCartId);

			if (shoppingCart is null) return NotFound();

			return Ok(shoppingCart);
		}

		[Authorize]
		[HttpGet]
		[Route("user")]
		public async Task<ActionResult<Order>> GetUserShoppingCart()
		{
			long shoppingCartId = long.Parse(User.Claims.FirstOrDefault(c => c.Type == "ShoppingCartId").Value);

			var shoppingCart = await _context.ShoppingCarts
				.Include(s => s.ShoppingCartItems)
				.ThenInclude(si => si.Item)
				.Include(s => s.ShoppingCartItems)
				.ThenInclude(si => si.Addons)
				.ThenInclude(a => a.Addon)
				.Include(s => s.ShoppingCartItems)
				.ThenInclude(si => si.NoOptions)
				.ThenInclude(n => n.NoOption)
				.Include(s => s.ShoppingCartItems)
				.ThenInclude(si => si.Groups)
				.ThenInclude(g => g.GroupOption)
				.AsSplitQuery()
				.FirstOrDefaultAsync(s => s.ShoppingCartId == shoppingCartId);

			if (shoppingCart is null) return NotFound();

			return Ok(new ShoppingCartDTO(shoppingCart));
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<ShoppingCart>>> GetShoppingCarts()
		{
			var shoppingCarts = await _context.ShoppingCarts
				.Include(s => s.ShoppingCartItems)
				.ThenInclude(si => si.Item)
				.ThenInclude(i => i.Category)
				.Include(s => s.ShoppingCartItems)
				.ThenInclude(si => si.Addons)
				.ThenInclude(a => a.Addon)
				.Include(s => s.ShoppingCartItems)
				.ThenInclude(si => si.NoOptions)
				.ThenInclude(n => n.NoOption)
				.Include(s => s.ShoppingCartItems)
				.ThenInclude(si => si.Groups)
				.ThenInclude(g => g.GroupOption)
				.AsSplitQuery()
				.ToListAsync();

			return Ok(shoppingCarts.Select(shoppingCart => new ShoppingCartDTO(shoppingCart)).ToList());
		}

		[Authorize]
		[HttpPost]
		public async Task<ActionResult<ShoppingCart>> AddShoppingCartItem([FromBody] ShoppingCartItemHelper shoppingCartItem)
		{
			long shoppingCartId = long.Parse(User.Claims.FirstOrDefault(c => c.Type == "ShoppingCartId").Value);

			var foundItem = await _context.Items.FirstAsync(i => i.ItemId.Equals(shoppingCartItem.ItemId));
			if (foundItem is null) return NotFound();

			ShoppingCartItem newShoppingCartItem = new()
			{
				ShoppingCartId = shoppingCartId,
				Item = foundItem,
				Count = shoppingCartItem.Count
			};

			foreach (var addon in shoppingCartItem.Addons)
			{
				var foundAddon = await _context.Addons.FindAsync(addon.AddonId);
				if (foundAddon is null) return BadRequest($"Cannot find addon with ID => {addon.AddonId}");
				var newShoppingCartAddon = new ShoppingCartAddon
				{
					Addon = foundAddon,
					ShoppingCartItem = newShoppingCartItem
				};
				newShoppingCartItem.Addons.Add(newShoppingCartAddon);
			}

			foreach (var noOption in shoppingCartItem.NoOptions)
			{
				var foundNoOption = await _context.NoOptions.FindAsync(noOption.NoOptionId);
				if (foundNoOption is null) return BadRequest($"Cannot find NoOption with ID => {noOption.NoOptionId}");
				var newShoppingCartNoOption = new ShoppingCartNoOption
				{
					NoOption = foundNoOption,
					ShoppingCartItem = newShoppingCartItem
				};
				newShoppingCartItem.NoOptions.Add(newShoppingCartNoOption);
			}

			foreach (var groupOption in shoppingCartItem.Groups)
			{
				var foundGroupOption = await _context.GroupOptions.FindAsync(groupOption.GroupOptionId);
				if (foundGroupOption is null) return BadRequest($"GroupOptionID {groupOption.GroupOptionId} is not a group option.");

				var newShoppingCartGroup = new ShoppingCartGroup
				{
					GroupOption = foundGroupOption,
					ShoppingCartItem = newShoppingCartItem
				};
				newShoppingCartItem.Groups.Add(newShoppingCartGroup);
			}

			await _context.ShoppingCartItems.AddAsync(newShoppingCartItem);
			await _context.SaveChangesAsync();

			var shoppingCart = await _context.ShoppingCarts
				.Include(s => s.ShoppingCartItems)
				.ThenInclude(si => si.Addons)
				.ThenInclude(a => a.Addon)
				.Include(s => s.ShoppingCartItems)
				.ThenInclude(si => si.NoOptions)
				.ThenInclude(n => n.NoOption)
				.Include(s => s.ShoppingCartItems)
				.ThenInclude(si => si.Groups)
				.ThenInclude(g => g.GroupOption)
				.Include(s => s.ShoppingCartItems)
				.ThenInclude(si => si.Item)
				.AsSplitQuery()
				.FirstOrDefaultAsync(s => s.ShoppingCartId == shoppingCartId);

			return Ok(new ShoppingCartDTO(shoppingCart));
		}

		[Authorize]
		[HttpPut("{shoppingCartItemId}")]
		public async Task<ActionResult<ShoppingCart>> UpdateShoppingCartItem(long shoppingCartItemId, [FromBody] ShoppingCartItemHelper shoppingCartItem)
		{
			long shoppingCartId = long.Parse(User.Claims.FirstOrDefault(c => c.Type == "ShoppingCartId").Value);

			if (shoppingCartItemId != shoppingCartItem.ShoppingCartItemId) return BadRequest("id doesnt match");

            ShoppingCartItem? foundShoppingCartItem = await _context.ShoppingCartItems.FindAsync(shoppingCartItemId);
			if (foundShoppingCartItem is null) return NotFound($"bad shoppingCartItemId:{shoppingCartItemId}");

			var foundItem = await _context.Items.FindAsync(shoppingCartItem.ItemId);

			foundShoppingCartItem.Count = shoppingCartItem.Count;
			foundShoppingCartItem.Addons.Clear();
			foundShoppingCartItem.NoOptions.Clear();
			foundShoppingCartItem.Groups.Clear();

			List<ShoppingCartAddon> newAddons = new();
			foreach (var addon in shoppingCartItem.Addons)
			{
				var foundAddon = await _context.Addons.FindAsync(addon.AddonId);
				if (foundAddon is null) return BadRequest($"Cannot find addon with ID => {addon.AddonId}");
				var newShoppingCartAddon = new ShoppingCartAddon
				{
					Addon = foundAddon,
					ShoppingCartItem = foundShoppingCartItem
				};
				newAddons.Add(newShoppingCartAddon);
			}

			List<ShoppingCartNoOption> newNoOptions = new();
			foreach (var noOption in shoppingCartItem.NoOptions)
			{
				var foundNoOption = await _context.NoOptions.FindAsync(noOption.NoOptionId);
				if (foundNoOption is null) return BadRequest($"Cannot find NoOption with ID => {noOption.NoOptionId}");
				var newShoppingCartNoOption = new ShoppingCartNoOption
				{
					NoOption = foundNoOption,
					ShoppingCartItem = foundShoppingCartItem
				};
				newNoOptions.Add(newShoppingCartNoOption);
			}

			List<ShoppingCartGroup> newGroups = new();
			foreach (var groupOption in shoppingCartItem.Groups)
			{
				var foundGroupOption = await _context.GroupOptions.FindAsync(groupOption.GroupOptionId);
				if (foundGroupOption is null) return BadRequest($"GroupOptionID {groupOption.GroupOptionId} is not a group option.");

				var newShoppingCartGroup = new ShoppingCartGroup
				{
					GroupOption = foundGroupOption,
					ShoppingCartItem = foundShoppingCartItem
				};
				newGroups.Add(newShoppingCartGroup);
			}

			foundShoppingCartItem.Addons = newAddons;
			foundShoppingCartItem.Groups = newGroups;
			foundShoppingCartItem.NoOptions = newNoOptions;

			/*for (int i = foundShoppingCartItem.Addons.Count - 1; i >= 0; i--)
			{
				if (shoppingCartItem.Addons.Any(a => a.AddonId == foundShoppingCartItem.Addons[i].Addon.AddonId))
				{
					foundShoppingCartItem.Addons.RemoveAt(i);
				}
			}

			for (int i = foundShoppingCartItem.NoOptions.Count - 1; i >= 0; i--)
			{
				if (shoppingCartItem.NoOptions.Any(n => n.NoOptionId == foundShoppingCartItem.NoOptions[i].NoOption.NoOptionId))
				{
					foundShoppingCartItem.NoOptions.RemoveAt(i);
				}
			}

			for (int i = foundShoppingCartItem.Groups.Count - 1; i >= 0; i--)
			{
				if (shoppingCartItem.Groups.Any(g => g.GroupOptionId == foundShoppingCartItem.Groups[i].GroupOption.GroupOptionId))
				{
					foundShoppingCartItem.Groups.RemoveAt(i);
				}
			}*/

			_context.Update(foundShoppingCartItem);
			await _context.SaveChangesAsync();

			var shoppingCart = await _context.ShoppingCarts
				.Include(s => s.ShoppingCartItems)
				.ThenInclude(si => si.Addons)
				.ThenInclude(a => a.Addon)
				.Include(s => s.ShoppingCartItems)
				.ThenInclude(si => si.NoOptions)
				.ThenInclude(n => n.NoOption)
				.Include(s => s.ShoppingCartItems)
				.ThenInclude(si => si.Groups)
				.ThenInclude(g => g.GroupOption)
				.Include(s => s.ShoppingCartItems)
				.ThenInclude(si => si.Item)
				.AsSplitQuery()
				.FirstOrDefaultAsync(s => s.ShoppingCartId == shoppingCartId);

			return Ok(new ShoppingCartDTO(shoppingCart));
		}

		[Authorize]
		[HttpDelete("{shoppingCartItemId}")]
		public async Task<ActionResult<ShoppingCart>> DeleteShoppingCartItem(long shoppingCartItemId)
		{
			long shoppingCartId = long.Parse(User.Claims.FirstOrDefault(c => c.Type == "ShoppingCartId").Value);

			var shoppingCartItem = await _context.ShoppingCartItems.FindAsync(shoppingCartItemId);
			if (shoppingCartItem is null) return NotFound();

			_context.ShoppingCartItems.Remove(shoppingCartItem);
			await _context.SaveChangesAsync();

			var shoppingCart = await _context.ShoppingCarts
				.Include(s => s.ShoppingCartItems)
				.ThenInclude(si => si.Item)
				.Include(s => s.ShoppingCartItems)
				.ThenInclude(si => si.Addons)
				.ThenInclude(a => a.Addon)
				.Include(s => s.ShoppingCartItems)
				.ThenInclude(si => si.NoOptions)
				.ThenInclude(n => n.NoOption)
				.Include(s => s.ShoppingCartItems)
				.ThenInclude(si => si.Groups)
				.ThenInclude(g => g.GroupOption)
				//.AsSplitQuery()
				.FirstOrDefaultAsync(s => s.ShoppingCartId == shoppingCartId);
			
			// bandaid fix
			shoppingCart.ShoppingCartItems = shoppingCart.ShoppingCartItems.Where(s => s.ShoppingCartItemId != shoppingCartItemId).ToList();
			
			return Ok(new ShoppingCartDTO(shoppingCart));
		}
	}
}
