using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kuzzi.DataAccess.Data;
using Kuzzi.DataAccess.Repository.IRepository;

namespace Kuzzi.DataAccess.Repository
{
   public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;

            ApplicationUser = new ApplicationUserRepository(_db);

        }

        public IApplicationUserRepository ApplicationUser {get; private set;}




        public void Save()
        {
            _db.SaveChanges();
        }
    }
}