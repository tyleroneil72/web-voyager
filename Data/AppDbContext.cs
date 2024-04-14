using Microsoft.EntityFrameworkCore;
using web_voyager.Areas.TravelServices.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace web_voyager.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<GuestUser> GuestUsers { get; set; }
    public DbSet<Flight> Flights { get; set; }
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema("Identity");

        builder.Entity<ApplicationUser>(entity =>
        {
            entity.ToTable(name: "User");
        });

        builder.Entity<IdentityRole>(entity =>
        {
            entity.ToTable(name: "Role");
        });

        builder.Entity<IdentityUserRole<string>>(entity =>
        {
            entity.ToTable(name: "UserRoles");
        });

        builder.Entity<IdentityUserClaim<string>>(entity =>
        {
            entity.ToTable(name: "UserClaims");
        });

        builder.Entity<IdentityUserLogin<string>>(entity =>
        {
            entity.ToTable(name: "UserLogins");
        });

        builder.Entity<IdentityRoleClaim<string>>(entity =>
        {
            entity.ToTable(name: "RoleClaims");
        });

        builder.Entity<IdentityUserToken<string>>(entity =>
        {
            entity.ToTable(name: "UserTokens");
        });
    }
}