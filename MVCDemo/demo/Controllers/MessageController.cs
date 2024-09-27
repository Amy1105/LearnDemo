using Microsoft.AspNetCore.Mvc;
using OrderService;

namespace demo.Controllers
{
    public class MessageController : Controller
    {
        private readonly MessageService messageService;

        public MessageController(MessageService  _messageService)
        {
            messageService = _messageService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
