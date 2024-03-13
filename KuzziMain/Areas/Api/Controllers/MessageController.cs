using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Kuzzi.DataAccess.Repository.IRepository;
using Kuzzi.Models.Chat;
using Kuzzi.Utility;
using KuzziMain.Areas.Api.Models.Message;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace KuzziMain.Areas.Api.Controllers
{
    [Area("Api")]
    [Route("[area]/v1/[controller]")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_User)]
    public class MessageController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;


        public MessageController(IUnitOfWork db)
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

        [HttpPost, ActionName("Send")]
        public IActionResult Send([FromBody] MessageRequest request)
        {
            if ((request.Text ?? "").Trim().IsNullOrEmpty() && request.Type.IsNullOrEmpty())
            {
                return BadRequest(new { success = false, data = request, message = "Empty Message" });

            }
            if (request.ConversationId == null){
                return BadRequest(new { success = false, data = request, message = "ConversationId Missing" });

            }
            else
            {
                var conversation = _unitOfWork.Conversation.Get(u=>u.Id==request.ConversationId);
                if(conversation == null) {
                return BadRequest(new { success = false, data = request, message = "Conversation Not Found" });

                }


                var claimIdentity = (ClaimsIdentity)User.Identity!;
                var authUserId = claimIdentity.FindFirst(ClaimTypes.NameIdentifier)!.Value;
                var chatUser = _unitOfWork.ChatUser.Get(u => u.ApplicationUserId == authUserId);


                var message = new Message
                {
                    Id = Guid.NewGuid().ToString(),
                    LocalMessageId = request.LocalMessageId,
                    SenderUserId = chatUser.Id,
                    Text = request.Text,
                    Value = request.Value,
                    //  SenderUserId = authUserId,
                    SentAt = DateTime.UtcNow,
                    ReplyTo = request.ReplyTo,
                    Status = "sent",
                    Type = "text",
                    ConversationId = request.ConversationId

                };
                _unitOfWork.Message.Add(message );
                _unitOfWork.Save();





                return Ok(new { data = message, Message = "Success" });
            }

        }

    }
}