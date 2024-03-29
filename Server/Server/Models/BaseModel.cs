﻿namespace Server.Models
{
	public abstract class BaseModel : ISoftDelete
	{
		public DateTimeOffset? CreatedAt { get; set; }
		public DateTimeOffset? UpdatedAt { get; set; }
		public DateTimeOffset? DeletedAt { get; set; }
	}
}
