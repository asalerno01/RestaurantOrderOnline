using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SalernoServer.Models;
using Server.Models.Authentication;
using Server.Models.ItemModels.Helpers;
using Server.Models.OrderModels;
using System.Security.Claims;

namespace SalernoServer.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }
        //public class ADTO
        //{
        //    public long AccountId { get; set; }
        //    public string FirstName { get; set; }
        //    public string LastName { get; set; }
        //    public string Email { get; set; }
        //    public string PhoneNumber { get; set; }
        //}
        //public class ODTO
        //{
        //    public long OrderId { get; set; }
        //    public ADTO Account { get; set; }
        //    public decimal Subtotal { get; set; }
        //    public decimal SubtotalTax { get; set; }
        //    public decimal Total { get; set; }
        //    public string Status { get; set; }
        //    public List<OIDTO> OrderItems { get; set; } = new();
        //}
        //public class OIDTO
        //{
        //    public string ItemId { get; set; }
        //    public string Name { get; set; }
        //    public decimal Price { get; set; }
        //    public int Count { get; set; }
        //    public List<OrderItemAddon> Addons { get; set; }
        //    public List<OrderItemNoOption> NoOptions { get; set; }
        //    public List<OrderItemGroup> Groups { get; set; }
        //}
        // GET: api/orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
        {
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Addons)
                .ThenInclude(oia => oia.Addon)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.NoOptions)
                .ThenInclude(oino => oino.NoOption)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Groups)
                //.ThenInclude(oig => oig.Group)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Groups)
                .ThenInclude(oig => oig.GroupOption)
                .Include(o => o.Account)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Item)
                .AsSplitQuery()
                .ToListAsync();

            return Ok(orders.Select(order => new OrderDTO(order)).ToList());
        }
        //[HttpGet]
        //[Route("test")]
        //public Task<ActionResult<IEnumerable<ODTO>>> GetOrdersTest()
        //{
        //    var x = _context.Orders.Select(o => new ODTO
        //    {
        //        OrderId = o.OrderId,
        //        Account = new ADTO
        //        {
        //            AccountId = o.Account.AccountId,
        //            FirstName = o.Account.FirstName,
        //            LastName = o.Account.LastName,
        //            PhoneNumber = o.Account.PhoneNumber,
        //            Email = o.Account.Email
        //        },
        //        Subtotal = o.Subtotal,
        //        SubtotalTax = o.SubtotalTax,
        //        Total = o.Total,
        //        Status = o.Status,
        //        OrderItems = o.OrderItems.Select(oi => new OIDTO
        //        {
        //            ItemId = oi.Item.ItemId,
        //            Name = oi.Item.Name,
        //            Price = oi.Price,
        //            Count = oi.Count,
        //            Addons = oi.Addons,
        //            NoOptions = oi.NoOptions,
        //            Groups = oi.Groups
        //        }).ToList()
        //    }).ToList();

        //    return Ok(x);
        //}
        [HttpGet]
        [Route("simple/active")]
        public async Task<ActionResult<IEnumerable<SimpleOrder>>> GetSimpleOrdersActive()
        {
            var orders = await _context.Orders
                    .Include(o => o.Account)
                    .Where(order => order.Status.Equals("Pending") || order.Status.Equals("Accepted"))
                    .OrderByDescending(order => order.OrderId)
                    .ToListAsync();
            return Ok(orders.Select(order => new SimpleOrder(order)).ToList());
        }
        [HttpGet]
        [Route("simple")]
        public async Task<ActionResult<IEnumerable<SimpleOrder>>> GetSimpleOrders()
        {
            var orders = await _context.Orders
                    .Include(o => o.Account)
                    .ToListAsync();
            return Ok(orders.Select(order => new SimpleOrder(order)).ToList());
        }
        public class OrderUpdateHelper
        {
            public long OrderId { get; set; }
            public string Status { get; set; }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(long id, [FromBody] OrderUpdateHelper order)
        {
            if (id != order.OrderId) return BadRequest();
            var foundOrder = await _context.Orders.FindAsync(id);
            if (foundOrder is null) return BadRequest();
            foundOrder.Status = order.Status;
            _context.Update(foundOrder);
            await _context.SaveChangesAsync();
            return Ok();
        }
        public class DateHelper
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }
        [HttpPost]
        [Route("date")]
        public async Task<ActionResult<IEnumerable<SimpleOrder>>> GetOrdersByDate([FromBody] DateHelper dateHelper)
        {
            Console.WriteLine(dateHelper.StartDate);
            var orders = await _context.Orders
                    .Include(order => order.Account)
                    .Where(o => o.OrderDate <= dateHelper.EndDate.AddDays(1).AddTicks(-1) && o.OrderDate >= dateHelper.StartDate.AddDays(-1).AddTicks(1))
                    .ToListAsync();
            return Ok(orders.Select(order => new SimpleOrder(order)).ToList());
        }
        [HttpPost]
        [Route("date/full")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrdersByDateFull([FromBody] DateHelper dateHelper)
        {
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Addons)
                .ThenInclude(oia => oia.Addon)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.NoOptions)
                .ThenInclude(oino => oino.NoOption)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Groups)
                //.ThenInclude(oig => oig.Group)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Groups)
                .ThenInclude(oig => oig.GroupOption)
                .Include(o => o.Account)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Item)
                .Where(o => o.OrderDate <= dateHelper.EndDate.AddDays(1).AddTicks(-1) && o.OrderDate >= dateHelper.StartDate.AddDays(-1).AddTicks(1))
                .ToListAsync();
            return Ok(orders.Select(order => new OrderDTO(order)).ToList());
        }

        // GET: api/items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(long id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Item)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Addons)
                .ThenInclude(a => a.Addon)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.NoOptions)
                .ThenInclude(no => no.NoOption)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Groups)
                .ThenInclude(g => g.GroupOption)
                .Include (o => o.Account)
                .FirstOrDefaultAsync(order => order.OrderId == id);

            if (order is null) return NotFound();

            return Ok(new OrderDTO(order));
        }
        [HttpGet]
        [Route("savedorders/{id}")]
        public async Task<ActionResult<List<SavedOrderDTO>>> GetSavedOrders(long id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account is null) return NotFound();
            var savedOrders = await _context.SavedOrders
                .Include(savedOrder => savedOrder.OrderItems)
                .ThenInclude(orderItem => orderItem.OrderItem)
                .ThenInclude(orderItem => orderItem.Addons)
                .ThenInclude(addons => addons.Addon)
                .Include(savedOrder => savedOrder.OrderItems)
                .ThenInclude(orderItem => orderItem.OrderItem)
                .ThenInclude(orderItem => orderItem.NoOptions)
                .ThenInclude(noOption => noOption.NoOption)
                .Include(savedOrder => savedOrder.OrderItems)
                .ThenInclude(orderItem => orderItem.OrderItem)
                .ThenInclude(orderItem => orderItem.Groups)
                //.ThenInclude(group => group.Group)
                .Include(savedOrder => savedOrder.OrderItems)
                .ThenInclude(orderItem => orderItem.OrderItem)
                .ThenInclude(orderItem => orderItem.Groups)
                .ThenInclude(group => group.GroupOption)
                .Include(savedOrder => savedOrder.OrderItems)
                .ThenInclude(orderItem => orderItem.OrderItem)
                .Include(savedOrder => savedOrder.OrderItems)
                .ThenInclude(savedOrderItem => savedOrderItem.OrderItem)
                .ThenInclude(orderItem => orderItem.Item)
                .Where(so => so.Account == account)
                .ToListAsync();
            
            return Ok(savedOrders.Select(savedOrder => new SavedOrderDTO(savedOrder)).ToList());
        }

		[Authorize]
        [HttpPost]
		public IActionResult CreateOrder()
        {
            var x = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            return Ok(x);
        }
        /*[HttpPost]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] OrderHelper order)
        {
            if (order is null) return BadRequest("Invalid order");
            var newOrder = new Order
            {
                Subtotal = order.Subtotal,
                SubtotalTax = order.SubtotalTax,
                Total = order.Total
            };
            var account = await _context.Accounts.FindAsync(order.AccountId);
            if (account is null)
            {
                account = await _context.Accounts.Where(account => (account.Email.Equals(order.Email) && account.PhoneNumber.Equals(order.PhoneNumber))).FirstOrDefaultAsync();
            }
            account ??= new()
            {
                FirstName = order.FirstName,
                LastName = order.LastName,
                Email = order.Email,
                PhoneNumber = order.PhoneNumber,
                OrderCount = 1,
                Password = "",
                IsVerified = false
                };
            newOrder.Account = account;
            if (order.OrderItems.IsNullOrEmpty()) return BadRequest("Order must have at least 1 order item.");
            foreach (var orderItem in order.OrderItems)
            {
                var foundItem = await _context.Items.FindAsync(orderItem.ItemId);
                if (foundItem is null) return BadRequest($"Cannot find item with ID => {orderItem.ItemId}");
                var newOrderItem = new OrderItem
                {
                    Order = newOrder,
                    Item = foundItem,
                    Count = orderItem.Count,
                    Price = foundItem.Price,
                    Name = foundItem.Name
                };
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
                    newOrderItem.Groups.Add(newOrderItemGroup);
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
                    newOrderItem.Addons.Add(newOrderItemAddon);
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
                    newOrderItem.NoOptions.Add(newOrderItemNoOption);
                }
                newOrder.OrderItems.Add(newOrderItem);
            }
            if (order.SaveOrder && order.SavedOrderName is not null && account is not null)
            {
                SavedOrder savedOrder = await _context.SavedOrders.Where(savedOrder => (savedOrder.Name.Equals(order.SavedOrderName) && savedOrder.Account == account)).FirstOrDefaultAsync();
                SavedOrder newSavedOrder = new()
                {
                    Name = order.SavedOrderName,
                    Account = account
                };
                newSavedOrder.OrderItems = OrderItemsToSavedOrderOrderItems(newSavedOrder, newOrder);
                if (savedOrder is not null)
                {
                    newSavedOrder.SavedOrderId = savedOrder.SavedOrderId;
                    _ = _context.Remove(savedOrder);
                }
                await _context.SavedOrders.AddAsync(newSavedOrder);
            }
            await _context.Orders.AddAsync(newOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetOrder),
                new { id = newOrder.OrderId },
                newOrder);
        }*/
        /*[HttpPost]
        [Route("many")]
        public async Task<ActionResult<IEnumerable<Order>>> CreateOrders([FromBody] List<OrderHelper> orders)
        {
            foreach (var order in orders)
            {
                if (order is null) return BadRequest("Invalid order");
                var newOrder = new Order
                {
                    Subtotal = order.Subtotal,
                    SubtotalTax = order.SubtotalTax,
                    Total = order.Total,
                    OrderDate = order.OrderDate
                };
                var account = await _context.Accounts.FindAsync(order.AccountId);
                if (account is null)
                {
                    account = await _context.Accounts.Where(account => (account.Email.Equals(order.Email) && account.PhoneNumber.Equals(order.PhoneNumber))).FirstOrDefaultAsync();
                }
                account ??= new()
                {
                    FirstName = order.FirstName,
                    LastName = order.LastName,
                    Email = order.Email,
                    PhoneNumber = order.PhoneNumber,
                    OrderCount = 1,
                    Password = "",
                    IsVerified = false
                };
                newOrder.Account = account;
                if (order.OrderItems.IsNullOrEmpty()) return BadRequest("Order must have at least 1 order item.");
                foreach (var orderItem in order.OrderItems)
                {
                    var foundItem = await _context.Items.FindAsync(orderItem.ItemId);
                    if (foundItem is null) return BadRequest($"Cannot find item with ID => {orderItem.ItemId}");
                    var newOrderItem = new OrderItem
                    {
                        Order = newOrder,
                        Item = foundItem,
                        Count = orderItem.Count,
                        Price = foundItem.Price,
                        Name = foundItem.Name
                    };
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
                        newOrderItem.Groups.Add(newOrderItemGroup);
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
                        newOrderItem.Addons.Add(newOrderItemAddon);
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
                        newOrderItem.NoOptions.Add(newOrderItemNoOption);
                    }
                    newOrder.OrderItems.Add(newOrderItem);
                }
                if (order.SaveOrder && order.SavedOrderName is not null && account is not null)
                {
                    SavedOrder savedOrder = await _context.SavedOrders.Where(savedOrder => (savedOrder.Name.Equals(order.SavedOrderName) && savedOrder.Account == account)).FirstOrDefaultAsync();
                    SavedOrder newSavedOrder = new()
                    {
                        Name = order.SavedOrderName,
                        Account = account
                    };
                    newSavedOrder.OrderItems = OrderItemsToSavedOrderOrderItems(newSavedOrder, newOrder);
                    if (savedOrder is not null)
                    {
                        newSavedOrder.SavedOrderId = savedOrder.SavedOrderId;
                        _ = _context.Remove(savedOrder);
                    }
                    await _context.SavedOrders.AddAsync(newSavedOrder);
                }
                await _context.Orders.AddAsync(newOrder);
            }
            await _context.SaveChangesAsync();

            return Ok();
        }*/
        private static List<SavedOrderOrderItem> OrderItemsToSavedOrderOrderItems(SavedOrder savedOrder, Order order)
        {
            List<SavedOrderOrderItem> savedOrderOrderItems = new();
            foreach(OrderItem orderItem in order.OrderItems)
            {
                savedOrderOrderItems.Add(new()
                {
                    SavedOrder = savedOrder,
                    OrderItem = orderItem
                });
            }
            return savedOrderOrderItems;

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
