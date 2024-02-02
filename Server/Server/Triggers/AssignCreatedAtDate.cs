using EntityFrameworkCore.Triggered;
using Server.Models;

namespace Server.Triggers
{
	public class AssignCreatedAtDate : IBeforeSaveTrigger<BaseModel>
	{
		public Task BeforeSave(ITriggerContext<BaseModel> context, CancellationToken cancellationToken)
		{
			context.Entity.CreatedAt = DateTimeOffset.UtcNow;

			return Task.CompletedTask;
		}
	}
}
