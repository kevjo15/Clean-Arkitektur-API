using Application;
using FluentAssertions.Common;
using Infrastructure;
using Infrastructure.Database;
using Infrastructure.Database.Repository;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Lägg till tjänster i containern.

// Add services to the container.
builder.Services.AddControllers(options =>
{
    // Apply a global authorization policy
    var policy = new AuthorizationPolicyBuilder()
                     .RequireAuthenticatedUser()
                     .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(SwaggerConfig =>
{
    // Creating a Swagger document.
    SwaggerConfig.SwaggerDoc("v1", new OpenApiInfo { Title = "API Animal", Version = "v1" });

    // Adding JWT Authentication definition to Swagger.
    // This allows Swagger UI to send the JWT token in the Authorization header.
    SwaggerConfig.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });

    // Telling SwaggerUI that the API uses Bearer(JWT) authentication
    // so you don't need to add the Bearer in front of the pasted token.
    SwaggerConfig.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("ConnectionString is null.");

//// Lägg till konfiguration för DbContext
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseMySQL(connectionString));

// Lägg till tjänster från Application och Infrastructure-projekten
builder.Services.AddApplication().AddInfrastructure();

builder.Services.AddAuthentication(options =>
{
    // Setting the schemes to JWT Bearer.
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    // Setting the parameters for validating incoming JWT tokens.
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        // Setting signing key from the configuration.
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]!))
    };
});


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
