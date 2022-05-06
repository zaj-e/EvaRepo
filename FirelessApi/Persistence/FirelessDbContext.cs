using FirelessApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace FirelessApi.Persistence;

public class FirelessDbContext : DbContext
{
    public DbSet<Alert> Alerts { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Data> Data { get; set; }
    public DbSet<User> Users { get; set; }

    public FirelessDbContext(DbContextOptions<FirelessDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Alert>(venue =>
        {
            venue.HasKey(v => v.Id);
            venue.Property(v => v.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
        });
            
        builder.Entity<Region>(venue =>
        {
            venue.HasKey(v => v.Id);
            venue.Property(v => v.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
        });

        builder.Entity<Data>(venue =>
        {
            venue.HasKey(v => v.Id);
            venue.Property(v => v.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
        });

        builder.Entity<User>(venue =>
        {
            venue.HasKey(v => v.Id);
            venue.Property(v => v.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
        });
    }
}