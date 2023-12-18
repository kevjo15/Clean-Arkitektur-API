using API.Authentication;
using API.Helpers;
using Application;
using FluentAssertions.Common;
using Infrastructure;
using Infrastructure.Database;
using Infrastructure.Database.Repository;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// L�gg till tj�nster i containern.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var secretKey = SecretKeyHelper.GetSecretKey(builder.Configuration);

builder.Services.AddMyCustomAuthentication(secretKey);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy =>
    {
        policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireAuthenticatedUser();
    });
});


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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
