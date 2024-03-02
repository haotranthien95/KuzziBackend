using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Kuzzi.DataAccess.Repository.IRepository;
using Kuzzi.Models.Chat;
using Kuzzi.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KuzziMain.Areas.Api.Controllers
{
    [Area("Api")]
    [Route("[area]/v1/[controller]")]
    [Authorize(Roles = SD.Role_Admin)]
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
        #endregion

    }
}