global using TBRPGF_API.Dto;
global using TBRPGF_API.Heroes;
using Microsoft.EntityFrameworkCore;
using TBRPGF_API.Data.Context;
using TBRPGF_API.Services.Heroes.Interfaces;
using TBRPGF_API.Services.Heroes.Requests;
using TBRPGF_API.Services.Spells.Interface;
using TBRPGF_API.Services.Spells.Requests;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TBRPGDBContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IHeroService, HeroRequests>();
builder.Services.AddScoped<ISpellService, SpellRequests>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => options.AddPolicy(name: "RpgOrigins",
    policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
    }));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("RpgOrigins");

app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.Run();