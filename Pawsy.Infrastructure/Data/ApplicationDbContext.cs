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

    }
}
