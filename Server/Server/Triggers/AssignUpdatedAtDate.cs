using EntityFrameworkCore.Triggered;
using Server.Models;

namespace Server.Triggers
{
	public class AssignUpdatedAtDate : IBeforeSaveTrigger<BaseModel>
	{
		public Task BeforeSave(ITriggerContext<BaseModel> context, CancellationToken cancellationToken)
		{
			context.Entity.UpdatedAt = DateTimeOffset.UtcNow;

			return Task.CompletedTask;
		}
	}
}
