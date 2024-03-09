using IFMAMVCDemo.Data;
using IFMAMVCDemo.Data.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Diagnostics;
using System.Runtime.InteropServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21))));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();
//builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
//    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
    .ReadFrom.Configuration(hostingContext.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day));

//// Configure Kestrel
//builder.WebHost.UseKestrel(options =>
//{
//    options.ListenLocalhost(5001, listenOptions =>
//    {
//        listenOptions.UseHttps("certificate.pfx", "P@ssw0rd");
//    });
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(errorApp =>
    {
        errorApp.Run(async context =>
        {
            context.Response.StatusCode = 500; // or another Status accordingly to Exception Type
            context.Response.ContentType = "text/html";

            var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
            if (errorFeature != null)
            {
                var exception = errorFeature.Error;

                // Log the exception
                var logger = app.Services.GetRequiredService<ILogger<Program>>();
                logger.LogError(exception, "Unhandled exception");

                // You may want to include a custom error page here
                await context.Response.WriteAsync("<h1>Error occurred</h1>").ConfigureAwait(false);
            }
        });
    });
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

//CreateDbIfNotExists(app);

// Open the URL in the default browser when running in a self-contained deployment
//if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
//{
//    Process.Start(new ProcessStartInfo("cmd", $"/c start https://localhost:5001") { CreateNoWindow = true });
//}
//else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
//{
//    Process.Start("xdg-open", "https://localhost:5001");
//}
//else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
//{
//    Process.Start("open", "https://localhost:5001")
//}

app.Run();

//async void CreateDbIfNotExists(WebApplication app)
//{
//    using var scope = app.Services.CreateScope();
//    var services = scope.ServiceProvider;

//    try
//    {
//        var context = services.GetRequiredService<ApplicationDbContext>();
//        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
//        var env = services.GetRequiredService<IWebHostEnvironment>();
//        await context.Database.MigrateAsync();
//        await DbInitializer.Initialize(context, env, userManager);
//    }
//    catch (Exception ex)
//    {
//        var logger = services.GetRequiredService<ILogger<Program>>();
//        logger.LogError(ex, "An error occurred creating the DB.");
//    }
//}