using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kuzzi.Models.Chat;

namespace Kuzzi.DataAccess.Repository.IRepository
{
    public interface IChatUserRepository : IRepository<ChatUser>
    {
        void Update(ChatUser obj);
    }
}