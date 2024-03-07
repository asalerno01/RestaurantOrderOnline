using EntityFrameworkCore.Triggered;
using SalernoServer.Models.ItemModels;
using SalernoServer.Models;
using Server.Models.ItemModels.SnapshotModels;

namespace Server.Triggers.Snapshot
{
	public class SnapshotGroup : IBeforeSaveTrigger<Group>
	{
		private readonly AppDbContext _context;

		public SnapshotGroup(AppDbContext context)
		{
			_context = context;
		}

		public Task BeforeSave(ITriggerContext<Group> context, CancellationToken cancellationToken)
		{
			if (context.ChangeType == ChangeType.Added || context.ChangeType == ChangeType.Modified)
			{
				Logger.Logger.Log($"Snapshotting group:{context.Entity.GroupId}");
				GroupSnapshot groupSnapshot = new()
				{
					Group = context.Entity,
					GroupId = context.Entity.GroupId,
					Name = context.Entity.Name,
				};

				_context.GroupSnapshots.Add(groupSnapshot);
			}

			return Task.CompletedTask;
		}
	}
}
