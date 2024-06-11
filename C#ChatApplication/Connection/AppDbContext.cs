using C_ChatApplication.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace C_ChatApplication.Connection
{
	public class AppDbContext:IdentityDbContext<AppUser,IdentityRole,string>
	{

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("server=Odissey;initial catalog=ChatAppTestDb;integrated Security=true;TrustServercertificate=true");
		}
		public DbSet<Message> Messages { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Message>()
	   .HasOne(m => m.Sender)
	   .WithMany(u => u.SendMessageList)
	   .HasForeignKey(m => m.SenderId)
	   .OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Message>()
				.HasOne(m => m.Receiver)
				.WithMany(u => u.ReceiverMessageList)
				.HasForeignKey(m => m.ReceiverId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<AppUser>().HasKey(x => x.Id);
			modelBuilder.Entity<AppUser>().HasMany(x => x.ReceiverMessageList).WithOne(x => x.Receiver).HasForeignKey(x => x.ReceiverId);
			modelBuilder.Entity<AppUser>().HasMany(x => x.SendMessageList).WithOne(x => x.Sender).HasForeignKey(x => x.SenderId
			);
		}
	}
		
}

