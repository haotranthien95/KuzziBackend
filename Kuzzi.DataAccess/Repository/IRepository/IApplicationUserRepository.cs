using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kuzzi.Models.Auth;
using Kuzzi.DataAccess.Repository.IRepository;

namespace Kuzzi.DataAccess.Repository.IRepository
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        
    }
}