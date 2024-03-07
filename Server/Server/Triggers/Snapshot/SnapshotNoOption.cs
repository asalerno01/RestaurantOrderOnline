using EntityFrameworkCore.Triggered;
using SalernoServer.Models.ItemModels;
using SalernoServer.Models;
using Server.Models.ItemModels.SnapshotModels;

namespace Server.Triggers.Snapshot
{
	public class SnapshotNoOption : IBeforeSaveTrigger<NoOption>
	{
		private readonly AppDbContext _context;

		public SnapshotNoOption(AppDbContext context)
		{
			_context = context;
		}

		public Task BeforeSave(ITriggerContext<NoOption> context, CancellationToken cancellationToken)
		{
			if (context.ChangeType == ChangeType.Added || context.ChangeType == ChangeType.Modified)
			{
				Logger.Logger.Log($"Snapshotting noOption:{context.Entity.NoOptionId}");
				NoOptionSnapshot noOptionSnapshot = new()
				{
					NoOption = context.Entity,
					NoOptionId = context.Entity.NoOptionId,
					Name = context.Entity.Name,
					Price = context.Entity.Price
				};

				_context.NoOptionSnapshots.Add(noOptionSnapshot);
			}

			return Task.CompletedTask;
		}
	}
}

