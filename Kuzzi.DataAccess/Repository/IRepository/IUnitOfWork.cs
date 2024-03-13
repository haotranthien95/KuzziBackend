using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kuzzi.DataAccess.Repository.IRepository
{
     public interface IUnitOfWork
    {
        IApplicationUserRepository ApplicationUser {get; }
        IConversationRepository Conversation {get; }
        IMessageRepository Message {get; }
        IChatUserRepository ChatUser {get; }
        IChatUserConversationRepository ChatUserConversation {get; }

        void Save();
    }
}