using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using Task4.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Task4.Helper;


namespace Task4.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private MessageContext db;

        public HomeController(MessageContext context)
        {
            db = context;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            if ((!User.Identity.IsAuthenticated) || (!HttpContext.Session.TryGetValue("UserName", out _)))
            {
                return RedirectToAction("login");
            }
            var lastMessage = HomeControllerHelper.GetMessageFromDb(db, MessageType.LAST_MESSAGE);
            ViewData["LastMessage"] = lastMessage.Text;
            ViewData["LastMessageInfo"] = lastMessage.ToString();
            ViewData["UserName"] = HttpContext.Session.GetString("UserName");
            return View(User.Claims.ToList());
        }

        [HttpGet]
        [AllowAnonymous, Route("login")]
        public IActionResult Login()
        {
            ViewData["BroCount"] = HomeControllerHelper.GetMessageCount(db, MessageType.BRO_MESSAGE).ToString();
            ViewData["SisCount"] = HomeControllerHelper.GetMessageCount(db, MessageType.SIS_MESSAGE).ToString();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendSis()
        {
            var messages = HomeControllerHelper.GetMessagesFromDb(db);
            HomeControllerHelper.UpdateMessages(
                messages: messages, 
                messageType: MessageType.SIS_MESSAGE, 
                userName: HttpContext.Session.GetString("UserName"),
                text: "Sis!");
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> SendBro()
        {
            var messages = HomeControllerHelper.GetMessagesFromDb(db);
            HomeControllerHelper.UpdateMessages(
                messages: messages,
                messageType: MessageType.BRO_MESSAGE,
                userName: HttpContext.Session.GetString("UserName"),
                text: "Bro!");
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
