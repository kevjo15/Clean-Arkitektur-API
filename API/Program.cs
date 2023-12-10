using Application;
using Infrastructure;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// L�gg till tj�nster i containern.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("ConnectionString is null.");

// L�gg till konfiguration f�r DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(connectionString));

// L�gg till tj�nster fr�n Application och Infrastructure-projekten
builder.Services.AddApplication().AddInfrastructure();

var app = builder.Build();

// Konfigurera HTTP-request-pipelinen.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
