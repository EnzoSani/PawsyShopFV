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

        public DbSet<Product> Products { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Category> Categories { get; set; }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            //    base.OnModelCreating(modelBuilder);

//            modelBuilder.Entity<Category>().HasData(
//    new Category { Id = 1, Name = "Comida" },
//    new Category { Id = 2, Name = "Juguetes" },
//    new Category { Id = 3, Name = "Accesorios" }
//);

//            modelBuilder.Entity<Pet>().HasData(
//                new Pet { Id = 1, Name = "Perro", ImageUrl = "/images/dog.png" },
//                new Pet { Id = 2, Name = "Gato", ImageUrl = "/images/cat.png" },
//                new Pet { Id = 3, Name = "Conejo", ImageUrl = "/images/rabbit.png" }
//            );

//            modelBuilder.Entity<Product>().HasData(
//                new Product
//                {
//                    Id = 1,
//                    Name = "Comida Premium Perro Adulto",
//                    Description = "Alimento balanceado de alta calidad",
//                    Price = 3500,
//                    Stock = 25,
//                    Brand = "Pedigree",
//                    ImageUrl = "/images/products/pedigree.png",
//                    CategoryId = 1,
//                    PetId = 1,
//                    IsFeatured = true
//                }
//            );
//        }

    }
}
