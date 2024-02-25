using Microsoft.EntityFrameworkCore;
using web_voyager.Models;

namespace web_voyager.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Flight> Flights { get; set; }
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
}