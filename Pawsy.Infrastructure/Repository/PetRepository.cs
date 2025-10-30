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
    public class PetRepository : Repository<Pet>, IPetRepository
    {
        //private  readonly ApplicationDbContext _db;
        public PetRepository(ApplicationDbContext db) : base(db)
        {
            //_db = db;
        }

        //public void Update(Pet entity)
        //{
        //    _db.Pets.Update(entity);
        //}
    }
}
