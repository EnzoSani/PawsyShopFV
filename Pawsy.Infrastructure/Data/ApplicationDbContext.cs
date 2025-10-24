using Microsoft.EntityFrameworkCore;
using Pawsy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawsy.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //    base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Perros",
                    Description = "Mascotas caninas"
                },
                new Category
                {
                    Id = 2,
                    Name = "Gatos",
                    Description = "Mascotas felinas"
                }
            );

            modelBuilder.Entity<Pet>().HasData(
                new Pet
                {
                    Id = 1,
                    Name = "Pupo",
                    Description = "Fusce tincidunt maximus leo, sed scelerisque massa auctor sit amet.",
                    ImageUrl = "https://placehold.co/600x400",
                    Gender = "Macho",
                    Age = 2,
                    CategoryId = 1
                },
                new Pet
                {
                    Id = 2,
                    Name = "Pipo",
                    Description = "Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                    ImageUrl = "https://placehold.co/600x400",
                    Gender = "Macho",
                    Age = 5,
                    CategoryId = 1
                },
                new Pet
                {
                    Id = 3,
                    Name = "Tito",
                    Description = "Fusce tincidunt maximus leo, sed scelerisque massa auctor sit amet.",
                    ImageUrl = "https://placehold.co/600x400",
                    Gender = "Macho",
                    Age = 7,
                    CategoryId = 2
                }
            );
        }

    }
}
