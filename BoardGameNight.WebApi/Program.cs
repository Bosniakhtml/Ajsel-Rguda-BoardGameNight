using BoardGameNight.DomainServices;
using BoardGameNight.DomainServices.Services;
using BoardGameNight.Infrastructure;
using BoardGameNight.Infrastructure.Repositories;
using BoardGameNight.WebApi.Queries;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

DotNetEnv.Env.Load();
var builder = WebApplication.CreateBuilder(args);


var connectionStringBoardgame = builder.Configuration["BoardgameConnectionString"]
    ?? builder.Configuration.GetConnectionString("Boardgame");
var connectionStringIdentity = builder.Configuration["AuthDbConnectionString"]
    ?? builder.Configuration.GetConnectionString("AuthDb");

if (string.IsNullOrWhiteSpace(connectionStringBoardgame) || string.IsNullOrWhiteSpace(connectionStringIdentity))
{
    throw new InvalidOperationException(
        "Database configuration is missing. Set BoardgameConnectionString and AuthDbConnectionString environment variables.");
}

builder.Services.AddDbContext<BoardgameDbContext>(options => options.UseSqlServer(connectionStringBoardgame));

builder.Services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(connectionStringIdentity));

// Add services to the container.

builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<IdentityUser>(options => {
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedPhoneNumber = false;

    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 3;

}).AddEntityFrameworkStores<AuthDbContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBoardgameNightRepository, BoardgameNightRepository>();
builder.Services.AddScoped<IBoardgameNightService, BoardgameNightService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services
    .AddGraphQLServer()
    .RegisterService<IBoardgameNightService>()
    .AddQueryType<BoardgameNightQueries>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.MapGraphQL();

app.UseAuthentication();
app.UseAuthorization();

app.MapGroup("/api/account").MapIdentityApi<IdentityUser>();

app.Run();
