using Microsoft.EntityFrameworkCore;
using web_voyager.Models;

namespace web_voyager.Controllers;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Flight> Flights { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
}