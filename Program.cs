using GiftGivers.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; // Add this for logging
using System; // Add this for Exception

var builder = WebApplication.CreateBuilder(args);

// 1. Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// 2. Configure the DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// 3. Configure ASP.NET Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    options.SignIn.RequireConfirmedAccount = false) // Set to false to allow login without email confirmation
    .AddEntityFrameworkStores<ApplicationDbContext>();

// 4. Add support for both Controllers (MVC) and Razor Pages (Identity)
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(); // This includes the built-in anti-forgery for Identity

var app = builder.Build();

// 5. Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages(); // This handles the Identity routes and their security

// --- BEGIN MOCK USER SEEDING ---
// This block runs once at startup to create your mock user.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

        // Ensure the database is created
        context.Database.Migrate();

        // Check if there are any users in the database
        if (!context.Users.Any())
        {
            // Create a new user if the database is empty
            var user = new IdentityUser
            {
                UserName = "admin@giftgivers.com",
                Email = "admin@giftgivers.com",
                EmailConfirmed = true // Confirm email immediately
            };

            // Create the user with the specified password
            var result = await userManager.CreateAsync(user, "Password1Mask!"); // Use this password to log in

            if (!result.Succeeded)
            {
                // Handle errors (e.g., log them)
                var logger = services.GetRequiredService<ILogger<Program>>();
                foreach (var error in result.Errors)
                {
                    logger.LogError(error.Description);
                }
            }
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}
// --- END MOCK USER SEEDING ---

app.Run();

