using EntityFrameworkCore.Triggered;
using SalernoServer.Models;
using Server.Models.ItemModels.SnapshotModels;
using Server.Models.ItemModels;
using SalernoServer.Models.ItemModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
using System.Xml.Linq;

namespace Server.Triggers.Snapshot
{
	public class SnapshotItem : IBeforeSaveTrigger<Item>
	{
		private readonly AppDbContext _context;

		public SnapshotItem(AppDbContext context)
		{
			_context = context;
		}

		public Task BeforeSave(ITriggerContext<Item> context, CancellationToken cancellationToken)
		{
			if (context.ChangeType == ChangeType.Added || context.ChangeType == ChangeType.Modified)
			{
				Logger.Logger.Log($"Snapshotting item:{context.Entity.ItemId}");
				ItemSnapshot itemSnapshot = new()
				{
					ItemId = context.Entity.ItemId,
					Item = context.Entity,
					Name = context.Entity.Name,
					Description = context.Entity.Description,
					Department = context.Entity.Department,
					CategoryId = context.Entity.Category.CategoryId,
					Category = context.Entity.Category,
					UPC = context.Entity.UPC,
					SKU = context.Entity.SKU,
					Price = context.Entity.Price,
					Discountable = context.Entity.Discountable,
					Taxable = context.Entity.Taxable,
					TrackingInventory = context.Entity.TrackingInventory,
					Cost = context.Entity.Cost,
					AssignedCost = context.Entity.AssignedCost,
					Quantity = context.Entity.Quantity,
					ReorderTrigger = context.Entity.ReorderTrigger,
					RecommendedOrder = context.Entity.RecommendedOrder,
					Supplier = context.Entity.Supplier,
					LiabilityItem = context.Entity.LiabilityItem,
					LiabilityRedemptionTender = context.Entity.LiabilityRedemptionTender,
					TaxGroupOrRate = context.Entity.TaxGroupOrRate,
					IsEnabled = context.Entity.IsEnabled
				};

				_context.ItemSnapshots.Add(itemSnapshot);
			}

			return Task.CompletedTask;
		}
	}
}
