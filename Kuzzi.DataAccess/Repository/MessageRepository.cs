using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kuzzi.DataAccess.Data;
using Kuzzi.DataAccess.Repository.IRepository;
using Kuzzi.Models.Chat;

namespace Kuzzi.DataAccess.Repository
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        public MessageRepository(ApplicationDbContext db) : base(db)
        {
        }

        public void Update(Message obj)
        {
            return;
        }
    }
}