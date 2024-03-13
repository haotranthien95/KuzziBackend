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
            Conversation = new ConversationRepository(_db);
            ChatUser = new ChatUserRepository(_db);
            Message = new MessageRepository(_db);
            ChatUserConversation = new ChatUserConversationRepository(_db);

        }

        public IApplicationUserRepository ApplicationUser {get; private set;}

        public IConversationRepository Conversation {get; private set;}

        public IChatUserRepository ChatUser  {get; private set;}
        public IMessageRepository Message  {get; private set;}

        public IChatUserConversationRepository ChatUserConversation {get; private set;}

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}