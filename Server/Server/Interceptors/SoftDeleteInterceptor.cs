using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Server.Models;

namespace Server.Interceptors
{
	public class SoftDeleteInterceptor : SaveChangesInterceptor
	{
		// https://blog.jetbrains.com/dotnet/2023/06/14/how-to-implement-a-soft-delete-strategy-with-entity-framework-core/
		public override InterceptionResult<int> SavingChanges(
			DbContextEventData eventData,
			InterceptionResult<int> result)
		{
			if (eventData.Context is null) return result;

			foreach (var entry in eventData.Context.ChangeTracker.Entries())
			{
				if (entry is not { State: Microsoft.EntityFrameworkCore.EntityState.Deleted, Entity: ISoftDelete delete }) continue;

				entry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
				delete.DeletedAt = DateTimeOffset.UtcNow;
			}

			return result;
		}
	}
}
