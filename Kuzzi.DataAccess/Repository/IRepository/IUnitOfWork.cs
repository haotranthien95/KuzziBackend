using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kuzzi.DataAccess.Repository.IRepository
{
     public interface IUnitOfWork
    {
         IApplicationUserRepository ApplicationUser {get; }
        void Save();
    }
}