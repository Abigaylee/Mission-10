using Microsoft.EntityFrameworkCore;
using Mission10.Models;
using System.IO;
using System.Linq;

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
    // The assignment database is provided as a `.sqlite` file.
    // We use it directly (no manual data entry). This also supports common spelling variants.
    var candidates = new[]
    {
        "BowlingLeague.sqlite",
        "BowlingLegue.Sqlite",
        "BowlingLegue.sqlite",
        "BowlingLeague.Sqlite",
        "BowlingLegue.SQLITE"
    };

    var contentRoot = builder.Environment.ContentRootPath;
    var searchDirs = new[]
    {
        contentRoot,
        Path.GetFullPath(Path.Combine(contentRoot, "..")),
        Path.GetFullPath(Path.Combine(contentRoot, "../.."))
    };

    var dbCandidates =
        (from dir in searchDirs
         from name in candidates
         let full = Path.Combine(dir, name)
         where File.Exists(full)
         select full).ToArray();

    // Prefer the most "complete" database (typically the downloaded one is larger than an accidentally created empty file).
    var dbPath = dbCandidates
        .OrderByDescending(p => new FileInfo(p).Length)
        .FirstOrDefault()
        ?? Path.Combine(contentRoot, "BowlingLeague.sqlite");

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
