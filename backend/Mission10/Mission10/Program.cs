using Microsoft.EntityFrameworkCore;
using Mission10.Models;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();

builder.Services.AddDbContext<BowlingLeagueContext>(options =>
{
    // Assignment database: `BowlingLeague.sqlite` in this project directory (tracked in git).
    var dbPath = Path.Combine(builder.Environment.ContentRootPath, "BowlingLeague.sqlite");
    options.UseSqlite($"Data Source={dbPath}");
});

builder.Services.AddScoped<IBowlerRepository, EFBowlerRepository>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseCors("CorsPolicy");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Keep http working during local development (React dev server typically uses http).
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

// Ensure schema exists (creates tables if missing, without deleting data).
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BowlingLeagueContext>();
    db.Database.EnsureCreated();
}

app.Run();
