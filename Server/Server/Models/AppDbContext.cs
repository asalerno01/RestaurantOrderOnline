using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SalernoServer.Models.ItemModels;
using SalernoServer.Models.Authentication;
using Server.Models.Authentication;
using Server.Models.ItemModels;
using Server.Models.OrderModels;
using Server.Models.ItemModels.SnapshotModels;
using Server.Old;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Server.Interceptors;
using Server.Models;

namespace SalernoServer.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Item> Items { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Modifier> Modifiers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupOption> GroupOptions { get; set; }
        public DbSet<Addon> Addons { get; set; }
        public DbSet<NoOption> NoOptions { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderItemAddon> OrderItemAddons { get; set; }
        public DbSet<OrderItemNoOption> OrderItemNoOptions { get; set; }
        public DbSet<OrderItemGroup> OrderItemGroups { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SavedOrder> SavedOrders { get; set; }
        public DbSet<SavedOrderOrderItem> SavedOrderOrderItems { get; set; }

        public DbSet<ItemSnapshot> ItemSnapshots { get; set; }
        public DbSet<ModifierSnapshot> ModifierSnapshots { get; set; }
        public DbSet<AddonSnapshot> AddonSnapshots { get; set; }
        public DbSet<NoOptionSnapshot> NoOptionSnapshots { get; set; }
        public DbSet<GroupSnapshot> GroupSnapshots { get; set; }
        public DbSet<GroupOptionSnapshot> GroupOptionSnapshots { get; set; }
		public DbSet<CategorySnapshot> CategorySnapshots { get; set; }

		public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
		{
			// https://stackoverflow.com/a/66072734
			foreach (var entry in ChangeTracker.Entries<ISoftDelete>())
			{
				switch (entry.State)
				{
					case EntityState.Deleted:
						// Override removal. Unchanged is better than Modified, because the latter flags ALL properties for update.
						// With Unchanged, the change tracker will pick up on the freshly changed properties and save them.
						entry.State = EntityState.Unchanged;
						entry.Property(nameof(ISoftDelete.DeletedAt)).CurrentValue = DateTimeOffset.UtcNow;
						break;
				}
			}
			return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ModifierSnapshot>()
                .HasMany(m => m.Addons)
                .WithOne(a => a.Modifier)
                .OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<ModifierSnapshot>()
				.HasMany(m => m.NoOptions)
				.WithOne(a => a.Modifier)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<ModifierSnapshot>()
				.HasMany(m => m.Groups)
				.WithOne(a => a.Modifier)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<GroupOptionSnapshot>()
				.HasOne(go => go.Group)
				.WithMany(g => g.GroupOptions)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<ItemSnapshot>()
				.HasOne(i => i.Item)
				.WithMany()
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<ModifierSnapshot>()
				.HasOne(m => m.Modifier)
				.WithMany()
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<ItemSnapshot>()
				.HasOne(i => i.Category)
				.WithMany(c => c.Items);

			// DeletedAt query filters
			modelBuilder.Entity<Item>()
				.HasQueryFilter(i => i.DeletedAt == null);
			modelBuilder.Entity<ItemSnapshot>()
				.HasQueryFilter(i => i.DeletedAt == null);
			modelBuilder.Entity<Modifier>()
				.HasQueryFilter(m => m.DeletedAt == null);
			modelBuilder.Entity<ModifierSnapshot>()
				.HasQueryFilter(m => m.DeletedAt == null);
			modelBuilder.Entity<Addon>()
				.HasQueryFilter(a => a.DeletedAt == null);
			modelBuilder.Entity<AddonSnapshot>()
				.HasQueryFilter(a => a.DeletedAt == null);
			modelBuilder.Entity<NoOption>()
				.HasQueryFilter(n => n.DeletedAt == null);
			modelBuilder.Entity<NoOptionSnapshot>()
				.HasQueryFilter(n => n.DeletedAt == null);
			modelBuilder.Entity<Group>()
				.HasQueryFilter(g => g.DeletedAt == null);
			modelBuilder.Entity<GroupSnapshot>()
				.HasQueryFilter(g => g.DeletedAt == null);
			modelBuilder.Entity<GroupOption>()
				.HasQueryFilter(g => g.DeletedAt == null);
			modelBuilder.Entity<GroupOptionSnapshot>()
				.HasQueryFilter(g => g.DeletedAt == null);
			modelBuilder.Entity<Category>()
				.HasQueryFilter(c => c.DeletedAt == null);
			modelBuilder.Entity<CategorySnapshot>()
				.HasQueryFilter(c => c.DeletedAt == null);

			/*modelBuilder.Entity<ModifierSnapshot>()
                .HasOne(m => m.Item)
                .WithOne(i => i.Modifier)
                .HasForeignKey<ModifierSnapshot>(i => i.ItemId);*/


			/*modelBuilder.Entity<Modifier>().Navigation(m => m.Addons).AutoInclude();*/
			//modelBuilder.Entity<Modifier>()
			//    .HasMany(m => m.Addons)
			//    .WithOne(a => a.Modifier)
			//    .OnDelete(DeleteBehavior.SetNull);
			//modelBuilder.Entity<Modifier>()
			//    .HasMany(m => m.NoOptions)
			//    .WithOne(no => no.Modifier)
			//    .OnDelete(DeleteBehavior.SetNull);
			//modelBuilder.Entity<Modifier>()
			//    .HasMany(m => m.Groups)
			//    .WithOne(g => g.Modifier)
			//    .OnDelete(DeleteBehavior.SetNull);
			//modelBuilder.Entity<Group>()
			//    .HasMany(m => m.GroupOptions)
			//    .WithOne(gi => gi.Group);

			//modelBuilder.Entity<Item>()
			//    .HasKey(i => i.ItemId);

			//modelBuilder.Entity<Item>()
			//    .HasMany(i => i.Modifiers)
			//    .WithOne(m => m.Item)
			//    .OnDelete(DeleteBehavior.SetNull);

			//modelBuilder.Entity<Order>()
			//    .HasMany(o => o.OrderItems);

			//modelBuilder.Entity<OrderItem>()
			//    .HasOne(oi => oi.Item);

			//modelBuilder.Entity<OrderItem>()
			//    .HasMany(oi => oi.OrderItemAddons)
			//    .WithOne(oia => oia.OrderItem)
			//    .OnDelete(DeleteBehavior.Cascade);
			//modelBuilder.Entity<OrderItem>()
			//    .HasMany(oi => oi.OrderItemNoOptions)
			//    .WithOne(oia => oia.OrderItem)
			//    .OnDelete(DeleteBehavior.Cascade);
			//modelBuilder.Entity<OrderItem>()
			//    .HasMany(oi => oi.OrderItemGroups)
			//    .WithOne(oia => oia.OrderItem)
			//    .OnDelete(DeleteBehavior.Cascade);

			//modelBuilder.Entity<OrderItemAddon>()
			//    .HasOne(oia => oia.Addon);
			//modelBuilder.Entity<OrderItemNoOption>()
			//    .HasOne(oino => oino.NoOption);
			//modelBuilder.Entity<OrderItemGroup>()
			//    .HasOne(oig => oig.GroupOption);
			//modelBuilder.Entity<OrderItemGroup>()
			//    .HasOne(oig => oig.GroupOption);

			/*modelBuilder.Entity<OrderItemAddon>()
                .HasKey("OrderItemId", "AddonId");
            modelBuilder.Entity<OrderItemNoOption>()
                .HasKey("OrderItemId", "NoOptionId");
            modelBuilder.Entity<OrderItemGroup>()
                .HasKey("OrderItemId", "GroupId", "GroupOptionId");*/

			/*modelBuilder.Entity<NoOption>()
                .HasOne(n => n.Modifier)
                .WithMany(m => m.NoOptions);

            modelBuilder.Entity<GroupOption>()
                .HasOne(go => go.Group)
                .WithMany(g => g.GroupOptions);*/

			/*modelBuilder.Entity<Modifier>()
                .HasMany<Group>(m => m.Groups)
                .WithOne(g => g.Modifier)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Modifier>()
                .HasMany<Addon>(m => m.Addons)
                .WithOne(a => a.Modifier)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Modifier>()
                .HasMany<NoOption>(m => m.NoOptions)
                .WithOne(n => n.Modifier)
                .OnDelete(DeleteBehavior.Cascade);*/

			/*modelBuilder.Entity<Employee>()
                .HasOne<Account>(e => e.Account)
                .WithOne(a => a.Employee)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Account>()
                .HasOne<Employee>(e => e.Employee)
                .WithOne(a => a.Account)
                .OnDelete(DeleteBehavior.Cascade);*/
		}
    }
}
