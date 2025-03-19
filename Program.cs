
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using subscription_system.Data;
using subscription_system.Data.SeedData;
using subscription_system.Mapper;
using subscription_system.Middleware;
using subscription_system.Models;
using subscription_system.Services;
using subscription_system.TagHelpers;

//TODO: inyectar dependencia , para poder utilizar IHTML

/*
 services.AddTransient<CustomTagHelper>();
 */

var builder = WebApplication.CreateBuilder(args);

//NOTE:LOG
builder.Logging.ClearProviders();
builder.Logging.AddConsole();


// NOTE: DATABASE  CONFIGURATION Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//  NOTE:
builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews().AddSessionStateTempDataProvider();

//NOTE: services
builder.Services.AddLogging();
builder.Services.AddSingleton<ILoggerFactory, LoggerFactory>();

builder.Services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
builder.Services.AddScoped<PlanFeatureService>();

builder.Services.AddScoped< IFeatureService,FeatureService >();
builder.Services.AddScoped<IPlanService,PlanService >();

//mapers
builder.Services.AddSingleton<PlanMapper>();
builder.Services.AddSingleton<PlanFeatureMapper>();
builder.Services.AddSingleton<FeatureMapper>();

builder.Services.AddScoped<ApplicationDbContext>();


/**/



builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});



builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;


    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;

});





var app = builder.Build();
/*
 
SEED DATA
 


using (var scope = app.Services.CreateScope()) {
        var services = scope.ServiceProvider;
        try
        {
            await RolSeedData.Initialize(services);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while seeding the database.");
        }

        await app.RunAsync();
    }

*/
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
//app.UseSubscriptionMiddleware();

app.UseSession();

app.MapControllerRoute(
    name: "MyArea",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
