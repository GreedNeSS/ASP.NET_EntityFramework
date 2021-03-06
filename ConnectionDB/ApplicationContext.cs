using Microsoft.EntityFrameworkCore;

namespace ConnectionDB
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Person> People { get; set; } = null!;

        public ApplicationContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasData(
                new Person() { Id = 1, Name = "GreedNeSS", Age = 30},
                new Person() { Id = 2, Name = "Marcus", Age = 45},
                new Person() { Id = 3, Name = "Henry", Age = 24}
                );
        }
    }
}
