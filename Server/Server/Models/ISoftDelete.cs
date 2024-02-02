namespace Server.Models
{
	public interface ISoftDelete
	{
		public DateTimeOffset? DeletedAt { get; set; }
	}
}
