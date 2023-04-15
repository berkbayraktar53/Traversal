using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DataAccessLayer.Concrete.EntityFramework.Contexts
{
	public class DatabaseContext : IdentityDbContext<User, Role, int>
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Server=.;Database=TraversalDb;Trusted_Connection=True;");
		}

		public DbSet<About> Abouts { get; set; }
		public DbSet<Client> Clients { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Contact> Contacts { get; set; }
		public DbSet<Destination> Destinations { get; set; }
		public DbSet<Guide> Guides { get; set; }
		public DbSet<Newsletter> Newsletters { get; set; }
		public DbSet<Notification> Notifications { get; set; }
		public DbSet<Reservation> Reservations { get; set; }
	}
}