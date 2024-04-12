using Microsoft.EntityFrameworkCore;
using web_voyager.Areas.TravelServices.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace web_voyager.Data;

public class AppDbContext : IdentityDbContext
{
    public DbSet<OldU> OldUs { get; set; }
    public DbSet<Flight> Flights { get; set; }
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    // protected override void OnModelCreating(ModelBuilder builder)
    // {
    //     base.OnModelCreating(builder);
    //     builder.HasDefaultSchema("Identity");

    //     builder.Entity<User>(entity =>
    //     {
    //         entity.ToTable(name: "User");
    //     });

    //     builder.Entity<IdentityRole>(entity =>
    //     {
    //         entity.ToTable(name: "Role");
    //     });

    //     builder.Entity<IdentityUserRole<string>>(entity =>
    //     {
    //         entity.ToTable("UserRoles");
    //     });

    //     builder.Entity<IdentityUserClaim<string>>(entity =>
    //     {
    //         entity.ToTable("UserClaims");
    //     });

    //     builder.Entity<IdentityUserLogin<string>>(entity =>
    //     {
    //         entity.ToTable("UserLogins");
    //     });

    //     builder.Entity<IdentityRoleClaim<string>>(entity =>
    //     {
    //         entity.ToTable("RoleClaims");
    //     });

    //     builder.Entity<IdentityUserToken<string>>(entity =>
    //     {
    //         entity.ToTable("UserTokens");
    //     });
    // }
}