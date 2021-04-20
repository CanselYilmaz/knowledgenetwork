using Microsoft.EntityFrameworkCore;

namespace knowledgenetwork.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options){}

        public DbSet<User> User { get; set; }

        public DbSet<Comment> Comment{get;set;}
        public DbSet<Category> Category {get;set;}
        public DbSet<Tags> Tags {get;set;}
        public DbSet<Blog> Blog {get;set;}

          protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(new User { 
            Id = 1,
            Name="Admin",
            Surname="Admin",
            Email="admin",
            Password="admin",
            Role=Role.ADMIN
            });
        }
    }
}