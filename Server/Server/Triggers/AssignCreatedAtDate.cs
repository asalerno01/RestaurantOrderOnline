using EntityFrameworkCore.Triggered;
using Server.Models;

namespace Server.Triggers
{
	public class AssignCreatedAtDate : IBeforeSaveTrigger<BaseModel>
	{
		public Task BeforeSave(ITriggerContext<BaseModel> context, CancellationToken cancellationToken)
		{
			Logger.Logger.Log($"CreatedAt:{context.Entity.CreatedAt}");
			context.Entity.CreatedAt = DateTimeOffset.UtcNow;

			return Task.CompletedTask;
		}
	}
}
