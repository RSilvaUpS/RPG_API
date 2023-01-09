using Microsoft.EntityFrameworkCore;
using TBRPGF_API;
using TBRPGF_API.DbInitializer;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("ConnectionDB") ?? throw new InvalidOperationException("Connection string 'ConnectionDB' not found.");

builder.Services.AddDbContext<TBRPGDBContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<TBRPGDBContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("ConnectionDB")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    dbInitializer.Initialize();
}

app.UseAuthorization();

app.MapControllers();

app.Run();