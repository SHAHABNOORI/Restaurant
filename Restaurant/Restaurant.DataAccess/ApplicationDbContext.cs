using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurant.Models;

namespace Restaurant.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<FoodType> FoodTypes { get; set; }

        public DbSet<MenuItem> MenuItems { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>(entity =>
            {
                entity.HasKey(category => category.Id);
                entity.Property(category => category.Name).IsRequired();
                entity.Property(category => category.DisplayOrder).IsRequired();
            });

            builder.Entity<FoodType>(entity =>
            {
                entity.HasKey(foodType => foodType.Id);
                entity.Property(foodType => foodType.Name).IsRequired();
            });

            builder.Entity<MenuItem>(entity =>
            {
                entity.Property(menuItem => menuItem.Name).IsRequired();

                entity.HasOne(e => e.Category)
                    .WithMany(c => c.MenuItems)
                    .IsRequired();

                entity.HasOne(e => e.FoodType)
                    .WithMany(c => c.MenuItems)
                    .IsRequired();
            });

        }
    }
}
