using EntityFrameworkCore.Triggered;
using SalernoServer.Models;
using Server.Models.ItemModels.SnapshotModels;
using Server.Models.ItemModels;
using SalernoServer.Models.ItemModels;

namespace Server.Triggers.Snapshot
{
	public class SnapshotModifier : IBeforeSaveTrigger<Modifier>
	{
		private readonly AppDbContext _context;

		public SnapshotModifier(AppDbContext context)
		{
			_context = context;
		}

		public Task BeforeSave(ITriggerContext<Modifier> context, CancellationToken cancellationToken)
		{
			if (context.ChangeType == ChangeType.Added || context.ChangeType == ChangeType.Modified)
			{
				Console.WriteLine($"Snapshotting modifier:{context.Entity.ModifierId}");
				ModifierSnapshot modifierSnapshot = new()
				{
					Modifier = context.Entity,
					ModifierId = context.Entity.ModifierId,
				};

				_context.ModifierSnapshots.Add(modifierSnapshot);
			}

			return Task.CompletedTask;
		}
	}
}
