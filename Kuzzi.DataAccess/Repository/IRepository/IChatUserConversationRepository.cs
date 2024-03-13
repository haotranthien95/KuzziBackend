using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kuzzi.Models.Chat;

namespace Kuzzi.DataAccess.Repository.IRepository
{
    public interface IChatUserConversationRepository : IRepository<ChatUserConversation>
    {
        void Update(ChatUserConversation obj);
    }
}