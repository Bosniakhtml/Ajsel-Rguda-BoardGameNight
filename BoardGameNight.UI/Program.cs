using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BoardGameNight.Infrastructure;
using BoardGameNight.DomainServices;
using BoardGameNight.DomainServices.Services;
using BoardGameNight.Infrastructure.Repositories;
DotNetEnv.Env.Load();
var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();


var connectionStringBoardgame = builder.Configuration["BoardgameConnectionString"]
    ?? builder.Configuration.GetConnectionString("Boardgame");
var connectionStringAuthDb = builder.Configuration["AuthDbConnectionString"]
    ?? builder.Configuration.GetConnectionString("AuthDb");

if (string.IsNullOrWhiteSpace(connectionStringBoardgame) || string.IsNullOrWhiteSpace(connectionStringAuthDb))
{
    throw new InvalidOperationException(
        "Database configuration is missing. Set BoardgameConnectionString and AuthDbConnectionString environment variables.");
}

builder.Services.AddDbContext<BoardgameDbContext>(
    options => {
        options.UseSqlServer(connectionStringBoardgame);
    });

builder.Services.AddDbContext<AuthDbContext>(
    options => {
        options.UseSqlServer(connectionStringAuthDb);
    });

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedPhoneNumber = false;

    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 3;
})
.AddEntityFrameworkStores<AuthDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddScoped<AuthSeedData>();
builder.Services.AddScoped<SeedData>();

builder.Services.AddScoped<IBoardgameService, BoardgameService>();
builder.Services.AddScoped<IBoardgameRepository, BoardgameRepository>();

builder.Services.AddScoped<IBoardgameNightService, BoardgameNightService>();
builder.Services.AddScoped<IBoardgameNightRepository, BoardgameNightRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seedData = scope.ServiceProvider.GetRequiredService<SeedData>();

    seedData.SeedDatabase();

    var seedDataIdentity = scope.ServiceProvider.GetRequiredService<AuthSeedData>();

    await seedDataIdentity.SeedDatabase();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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

app.Run();
