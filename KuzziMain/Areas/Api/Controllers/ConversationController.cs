using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Kuzzi.DataAccess.Repository.IRepository;
using Kuzzi.Models.Chat;
using Kuzzi.Utility;
using KuzziMain.Areas.Api.Models.Conversation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace KuzziMain.Areas.Api.Controllers
{
    [Area("Api")]
    [Route("[area]/v1/[controller]")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_User)]

    public class ConversationController : Controller
    {


        private readonly IUnitOfWork _unitOfWork;


        public ConversationController(IUnitOfWork db)
        {
            _unitOfWork = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll(int id)
        {
            List<Conversation> objProductList = _unitOfWork.Conversation.GetAll().ToList();
            return Json(new { data = objProductList });
        }

        [HttpPost]
        public IActionResult NewConversation([FromBody] ConversationRequest request)
        {

            if (request.ChatUserId.IsNullOrEmpty() && request.UserId.IsNullOrEmpty())
            {
                return BadRequest(new { success = false, data = request, message = "Missing Participates" });
            }
            ICollection<string> participates = request.ChatUserId ?? [];
            // Logic
            var claimIdentity = (ClaimsIdentity)User.Identity!;
            var authUserId = claimIdentity.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            var listUserId = request.UserId ?? [];
            listUserId.Add(authUserId);
            // Check user
            foreach (var UserId in request.UserId!)
            {
                // check if chatUser NOT existed
                var chatUser = _unitOfWork.ChatUser.Get(u => u.ApplicationUserId == UserId);
                if (chatUser == null)
                {
                    // INSERT
                    var uuid = Guid.NewGuid().ToString();
                    _unitOfWork.ChatUser.Add(new ChatUser { Id = uuid, ApplicationUserId = UserId });
                    _unitOfWork.Save();

                    participates.Add(uuid);
                }
                else
                {
                    participates.Add(chatUser.Id);
                }
            }
            // Insert Conversation

            ICollection<ChatUser> chatUserList = [];

            foreach (var member in participates)
            {
                var chatUser = _unitOfWork.ChatUser.Get(u => u.Id == member);
                chatUserList.Add(chatUser);
            }
            var newConversation = new Conversation
            {
                Id = Guid.NewGuid().ToString(),
                ConversationType = request.ConversationType,
                CreatedChatUserId = participates.LastOrDefault(),
                CreatedAt = DateTime.UtcNow,
                LastUpdated = DateTime.UtcNow
            };
            _unitOfWork.Conversation.Add(newConversation);
            _unitOfWork.Save();

            foreach (var chatUser in chatUserList)
            {
                _unitOfWork.ChatUserConversation.Add(new ChatUserConversation { ConversationId = newConversation.Id, ChatUserId = chatUser.Id });
                _unitOfWork.Save();
            }


            return Ok(new { Conversation = newConversation.Id, Message =participates });
        }
        #endregion

    }
}