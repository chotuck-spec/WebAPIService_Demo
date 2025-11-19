using Microsoft.EntityFrameworkCore;
using WebAPIService.Models;

namespace WebAPIService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("Person", "dbo"); // Map to actual table name
        }
    }
}
