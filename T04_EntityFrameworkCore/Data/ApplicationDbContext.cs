using Microsoft.EntityFrameworkCore;
using T04_EntityFrameworkCore.Models;

namespace T04_EntityFrameworkCore.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{

		}

		public DbSet<ShirtModel> Shirts { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// data seeding
			modelBuilder.Entity<ShirtModel>().HasData(
				new ShirtModel
				{
					ShirtId = 1,
					Brand = "My Brand",
					Color = "Blue",
					Gender = "Men",
					Price = 30,
					Size = 10
				},
				new ShirtModel
				{
					ShirtId = 2,
					Brand = "My Brand",
					Color = "Black",
					Gender = "Men",
					Price = 35,
					Size = 12
				},
				new ShirtModel
				{
					ShirtId = 3,
					Brand = "Your Brand",
					Color = "Pink",
					Gender = "Women",
					Price = 28,
					Size = 8
				},
				new ShirtModel
				{
					ShirtId = 4,
					Brand = "Your Brand",
					Color = "Yello",
					Gender = "Women",
					Price = 30,
					Size = 9
				}
			);
		}
	}
}
