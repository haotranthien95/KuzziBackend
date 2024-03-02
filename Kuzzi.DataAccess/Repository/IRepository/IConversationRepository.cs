using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kuzzi.Models.Chat;

namespace Kuzzi.DataAccess.Repository.IRepository
{
    public interface IConversationRepository : IRepository<Conversation>
    {
        void Update(Conversation obj);
    }
}