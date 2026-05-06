using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

// dotnet ef migrations add Initial --project Infrastructure --startup-project Web
// dotnet ef database update --project Infrastructure --startup-project Web
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.Email).IsRequired().HasMaxLength(50);
            entity.HasIndex(e => e.Email).IsUnique();

            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
            
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("timestamp without time zone");

            entity.HasData(
                new User
                {
                    Id = Guid.NewGuid(),
                    Email = "robert-martin@gmail.ru",
                    FirstName = "Robert",
                    LastName = "Martin",
                    DateOfBirth = new DateTime(1952, 12, 05),
                    RegistrationDate = DateTime.UtcNow,
                    IsActive = true
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Email = "george-orwell@gmail.ru",
                    FirstName = "George",
                    LastName = "Orwell",
                    DateOfBirth = new DateTime(1903, 6, 25),
                    RegistrationDate = DateTime.UtcNow,
                    IsActive = true
                }
            );
        });
    }
}