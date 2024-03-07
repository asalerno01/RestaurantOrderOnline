using EntityFrameworkCore.Triggered;
using SalernoServer.Models;
using SalernoServer.Models.ItemModels;
using Server.Models.ItemModels.SnapshotModels;

namespace Server.Triggers.Snapshot
{
	public class SnapshotGroupOption : IBeforeSaveTrigger<GroupOption>
	{
		private readonly AppDbContext _context;

		public SnapshotGroupOption(AppDbContext context)
		{
			_context = context;
		}

		public Task BeforeSave(ITriggerContext<GroupOption> context, CancellationToken cancellationToken)
		{
			if (context.ChangeType == ChangeType.Added || context.ChangeType == ChangeType.Modified)
			{
				Logger.Logger.Log($"Snapshotting groupOption:{context.Entity.GroupOptionId}");
				GroupOptionSnapshot groupOptionSnapshot = new()
				{
					GroupOption = context.Entity,
					GroupOptionId = context.Entity.GroupOptionId,
					Name = context.Entity.Name,
					Price = context.Entity.Price,
					IsDefault = context.Entity.IsDefault
				};

				_context.GroupOptionSnapshots.Add(groupOptionSnapshot);
			}

			return Task.CompletedTask;
		}
	}
}
