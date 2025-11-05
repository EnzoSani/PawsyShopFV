using Pawsy.Application.Common.Interfaces;
using Pawsy.Domain.Entities;
using Pawsy.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawsy.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public ICategoryRepository Category { get; private set; }
        public IPetRepository Pet { get; private set; }
        public IProductRepository Product { get; private set; }
        public IOrderHeaderRepository OrderHeader { get; private set; }
        public IOrderDetailRepository OrderDetail { get; private set; }
        public IRefreshTokenRepository RefreshTokens { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            Category = new CategoryRepository(db);
            Pet = new PetRepository(db);
            Product = new ProductRepository(db);
            OrderHeader = new OrderHeaderRepository(db);
            OrderDetail = new OrderDetailRepository(db);
            RefreshTokens = new RefreshTokenRepository(db);
            _db = db;
        }

        public async Task SaveAsync() => await _db.SaveChangesAsync();
    }
}
