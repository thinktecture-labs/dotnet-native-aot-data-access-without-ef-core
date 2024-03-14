using Microsoft.EntityFrameworkCore;
using WebApp.DatabaseAccess.Model;

namespace WebApp.DatabaseAccess;

public sealed class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    public DbSet<Contact> Contacts => Set<Contact>();
    public DbSet<Address> Addresses => Set<Address>();

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder
           .Entity<Contact>(
                contact =>
                {
                    contact.Property(x => x.FirstName).IsRequired().HasMaxLength(200);
                    contact.Property(x => x.LastName).IsRequired().HasMaxLength(200);
                    contact.Property(x => x.Email).HasMaxLength(200);
                    contact.Property(x => x.Phone).HasMaxLength(50);
                }
            )
           .Entity<Address>(
                address =>
                {
                    address.Property(x => x.Street).IsRequired().HasMaxLength(200);
                    address.Property(x => x.ZipCode).IsRequired().HasMaxLength(5);
                    address.Property(x => x.City).IsRequired().HasMaxLength(200);
                }
            );
}