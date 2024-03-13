using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kuzzi.DataAccess.Data;
using Kuzzi.DataAccess.Repository.IRepository;
using Kuzzi.Models.Chat;

namespace Kuzzi.DataAccess.Repository
{
    public class ChatUserRepository : Repository<ChatUser>, IChatUserRepository
    {
        public ChatUserRepository(ApplicationDbContext db) : base(db)
        {
        }

        public void Update(ChatUser obj)
        {
            return;
        }
    }
}