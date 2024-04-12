using web_voyager.Data;
using web_voyager.Areas.TravelServices.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using web_voyager.Services;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.AspNetCore.Identity.UI.Services;
var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddIdentity<User, IdentityRole>()
//     .AddEntityFrameworkStores<AppDbContext>()
//     .AddDefaultUI()
//     .AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 21)),
        mySqlOptions => mySqlOptions.SchemaBehavior(MySqlSchemaBehavior.Ignore))
);

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddSingleton<IEmailSender, EmailSender>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseStatusCodePagesWithRedirects("/Home/NotFound?statusCode={0}");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
