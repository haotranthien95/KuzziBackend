using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kuzzi.Models.Chat;

namespace Kuzzi.DataAccess.Repository.IRepository
{
    public interface IMessageRepository : IRepository<Message>
    {
        void Update(Message obj);
    }
}