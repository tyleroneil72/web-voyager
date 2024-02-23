using Microsoft.EntityFrameworkCore;
using web_voyager.Models;

namespace web_voyager.Controllers;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
}