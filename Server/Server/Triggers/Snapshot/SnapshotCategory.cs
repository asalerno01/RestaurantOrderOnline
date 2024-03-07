using EntityFrameworkCore.Triggered;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
using SalernoServer.Models;
using SalernoServer.Models.ItemModels;
using Server.Logger;
using Server.Models.ItemModels;
using Server.Models.ItemModels.SnapshotModels;
using System.Xml.Linq;

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
			if (context.ChangeType == ChangeType.Added || context.ChangeType == ChangeType.Modified)
			{
				// Console.WriteLine($"Snapshotting category:{context.Entity.CategoryId}");
				Server.Logger.Logger.Log($"Snapshotting category:{context.Entity.CategoryId}");

				CategorySnapshot categorySnapshot = new()
				{
					Category = context.Entity,
					CategoryId = context.Entity.CategoryId,
					Name = context.Entity.Name
				};

				_context.CategorySnapshots.Add(categorySnapshot);
			}

			return Task.CompletedTask;
		}
	}
}
