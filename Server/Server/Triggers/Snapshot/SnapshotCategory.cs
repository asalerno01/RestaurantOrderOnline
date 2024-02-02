using EntityFrameworkCore.Triggered;
using SalernoServer.Models;
using Server.Models.ItemModels;
using Server.Models.ItemModels.SnapshotModels;

namespace Server.Triggers.Snapshot
{
	public class SnapshotCategory : IBeforeSaveTrigger<Category>
	{
		private readonly AppDbContext _context;

		public SnapshotCategory(AppDbContext context)
		{
			_context = context;
		}

		public Task BeforeSave(ITriggerContext<Category> context, CancellationToken cancellationToken)
		{
			if (context.ChangeType == ChangeType.Added)
			{
				Console.WriteLine("Adding category");
				CategorySnapshot categorySnapshot = new()
				{
					CategoryId = context.Entity.CategoryId,
					Category = context.Entity,
					Name = context.Entity.Name
				};

				_context.CategorySnapshots.Add(categorySnapshot);
			}
			else if (context.ChangeType == ChangeType.Modified)
			{
				Console.WriteLine("Modifying category");
				CategorySnapshot categorySnapshot = new()
				{
					CategoryId = context.Entity.CategoryId,
					Category = context.Entity,
					Name = context.Entity.Name
				};

				_context.CategorySnapshots.Add(categorySnapshot);
			}

			return Task.CompletedTask;
		}
	}
}
