using EntityFrameworkCore.Triggered;
using SalernoServer.Models.ItemModels;
using SalernoServer.Models;
using Server.Models.ItemModels.SnapshotModels;

namespace Server.Triggers.Snapshot
{
	public class SnapshotAddon : IBeforeSaveTrigger<Addon>
	{
		private readonly AppDbContext _context;

		public SnapshotAddon(AppDbContext context)
		{
			_context = context;
		}

		public Task BeforeSave(ITriggerContext<Addon> context, CancellationToken cancellationToken)
		{
			if (context.ChangeType == ChangeType.Added || context.ChangeType == ChangeType.Modified)
			{
				Logger.Logger.Log($"Snapshotting addon:{context.Entity.AddonId}");
				AddonSnapshot addonSnapshot = new()
				{
					Addon = context.Entity,
					AddonId = context.Entity.AddonId,
					Name = context.Entity.Name,
					Price = context.Entity.Price
				};

				_context.AddonSnapshots.Add(addonSnapshot);
			}

			return Task.CompletedTask;
		}
	}
}
