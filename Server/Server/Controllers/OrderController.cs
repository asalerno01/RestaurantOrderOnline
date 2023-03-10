using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol;
using SalernoServer.Models;

namespace SalernoServer.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var orders = await _context.Orders
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.OrderItemAddons)
                    .ThenInclude(oia => oia.Addon)
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.OrderItemNoOptions)
                    .ThenInclude(oino => oino.NoOption)
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.OrderItemGroups)
                    .ThenInclude(oig => oig.Group)
                    .ToListAsync();
            return Ok(orders);
        }

        // GET: api/items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(long id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Item)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.OrderItemGroups)
                .ThenInclude(g => g.GroupOption)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.OrderItemAddons)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.OrderItemNoOptions)
                .Where(o => o.OrderId == id)
                .FirstOrDefaultAsync();

            if (order == null) return NotFound();

            return Ok(order);
        }

        // PUT: api/Items/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(long id, [FromBody] Order Order)
        {
            //if (id != Order.OrderId)
            //{
            //    return BadRequest();
            //}

            //var order = await _context.Orders.FindAsync(id);
            //if (order == null)
            //{
            //    return NotFound();
            //}

            //// _context.Entry(Order).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (Exception)
            //{
            //    return NotFound();
            //}

            return NoContent();
        }

        // POST: api/order
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] OrderHelper order)
        {
            if (order is null) return BadRequest("Invalid order");
            var newOrder = new Order
            {
                Subtotal = order.Subtotal,
                Net = order.Net,
                Tax = order.Tax
            };
            var customerAccount = await _context.CustomerAccounts.FindAsync(order.CustomerAccountId);
            if (customerAccount is null) return BadRequest($"No customer account exists for ID => {order.CustomerAccountId}");
            newOrder.CustomerAccount = customerAccount;
            if (order.OrderItems.IsNullOrEmpty()) return BadRequest("Order must have at least 1 order item.");
            foreach (var orderItem in order.OrderItems)
            {
                var foundItem = await _context.Items.FindAsync(orderItem.ItemId);
                if (foundItem is null) return BadRequest($"Cannot find item with ID => {orderItem.ItemId}");
                var newOrderItem = new OrderItem
                {
                    Order = newOrder,
                    Item = foundItem
                };
                //await _context.OrderItems.AddAsync(newOrderItem);
                foreach (var groupOption in orderItem.GroupOptions)
                {
                        var foundGroup = await _context.Groups.FindAsync(groupOption.GroupId);
                        if (foundGroup is null) return BadRequest($"GroupID {groupOption.GroupId} is not a group.");
                        var foundGroupOption = await _context.GroupOptions.FindAsync(groupOption.GroupOptionId);
                        if (foundGroupOption is null) return BadRequest($"GroupOptionID {groupOption.GroupOptionId} is not a group option.");
                        if (!foundGroup.GroupOptions.Any(go => go.GroupOptionId == foundGroupOption.GroupOptionId)) return BadRequest($"GroupID {foundGroup.GroupId} does not contain a group option with ID {foundGroupOption.GroupOptionId}");

                        var newOrderItemGroup = new OrderItemGroup
                        {
                            OrderItem = newOrderItem,
                            Group = foundGroup,
                            GroupOption = foundGroupOption
                        };
                        newOrderItem.OrderItemGroups.Add(newOrderItemGroup);
                }
                foreach (var addon in orderItem.Addons)
                {
                    var foundAddon = await _context.Addons.FindAsync(addon.AddonId);
                    if (foundAddon is null) return BadRequest($"Cannot find addon with ID => {addon.AddonId}");
                    var newOrderItemAddon = new OrderItemAddon
                    {
                        OrderItem = newOrderItem,
                        Addon = foundAddon
                    };
                    newOrderItem.OrderItemAddons.Add(newOrderItemAddon);
                }
                foreach (var noOption in orderItem.NoOptions)
                {
                    var foundNoOption = await _context.NoOptions.FindAsync(noOption.NoOptionId);
                    if (foundNoOption is null) return BadRequest($"Cannot find NoOption with ID => {noOption.NoOptionId}");
                    var newOrderItemNoOption = new OrderItemNoOption
                    {
                        OrderItem = newOrderItem,
                        NoOption = foundNoOption
                    };
                    newOrderItem.OrderItemNoOptions.Add(newOrderItemNoOption);
                }
                newOrder.OrderItems.Add(newOrderItem);
            }
            await _context.Orders.AddAsync(newOrder);
            await _context.SaveChangesAsync();
            Console.WriteLine($"Added Order => {newOrder.OrderId}");

            return CreatedAtAction(
                nameof(GetOrder),
                new { id = newOrder.OrderId },
                newOrder);
        }

        // DELETE: api/orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(long id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
