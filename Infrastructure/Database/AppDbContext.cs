using Domain.Models;
using Domain.Models.Animal;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }
        public virtual DbSet<Bird> Birds { get; set; }
        public virtual DbSet<Cat> Cats { get; set; }
        public virtual DbSet<Dog> Dogs { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseMySQL("Server=127.0.0.1;Database=clean-api2;User=root;Password=Bajsan123;");
            optionsBuilder.UseMySQL(_configuration.GetConnectionString("DefaultConnection")!);
            //optionsBuilder.UseSqlServer("Server=127.0.0.1;Database=clean-api;User=root;Password=Bajsan123;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed data
            modelBuilder.Entity<Bird>().HasData(
            new Bird { Id = Guid.NewGuid(), Name = "Drake", CanFly = false },
            new Bird { Id = Guid.NewGuid(), Name = "Simon", CanFly = false },
            new Bird { Id = Guid.NewGuid(), Name = "Gustav", CanFly = false }
            );
            // Seed data
            modelBuilder.Entity<Cat>().HasData(
            new Cat { Id = Guid.NewGuid(), Name = "Nemo", LikesToPlay = false },
            new Cat { Id = Guid.NewGuid(), Name = "Doris", LikesToPlay = false },
            new Cat { Id = Guid.NewGuid(), Name = "Simba", LikesToPlay = false }
            );
            // Seed data
            modelBuilder.Entity<Dog>().HasData(
            new Dog { Id = Guid.NewGuid(), Name = "Björn" },
            new Dog { Id = Guid.NewGuid(), Name = "Patrik" },
            new Dog { Id = Guid.NewGuid(), Name = "Alfred" }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
