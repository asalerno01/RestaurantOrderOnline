using EntityFrameworkCore.Triggered;
using SalernoServer.Models;
using Server.Models;

namespace Server.Triggers
{
	public class EnsureSoftDelete : IBeforeSaveTrigger<ISoftDelete>
	{
		private readonly AppDbContext _appDbContext;

		public EnsureSoftDelete(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}

		public Task BeforeSave(ITriggerContext<ISoftDelete> context, CancellationToken cancellationToken)
		{
			if (context.ChangeType == ChangeType.Deleted)
			{
				context.Entity.DeletedAt = DateTimeOffset.UtcNow;
				_appDbContext.Entry(context.Entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			}

			return Task.CompletedTask;
		}
	}
}
