using System.Data.Common;
using LightboardApi.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Create and open connection (important: keep open for app lifetime)
var keepAliveConnection = new SqliteConnection("Data Source=:memory:");
keepAliveConnection.Open();

// Register EF Core with SQLite in-memory
builder.Services.AddDbContext<LightContext>(options => options.UseSqlite(keepAliveConnection));
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Ensure schema is created on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LightContext>();
    db.Database.EnsureCreated();
}

app.UseAuthorization();
app.UseDefaultFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.MapControllers();

app.Run();
