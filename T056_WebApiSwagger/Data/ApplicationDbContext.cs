using Microsoft.EntityFrameworkCore;
using T056_WebApiSwagger.Models;

namespace T056_WebApiSwagger.Data
{
	public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
	{
		public DbSet<ShirtModel> Shirts { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}
