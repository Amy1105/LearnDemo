using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrderService;

namespace demo.Controllers
{
    public class MessageController : Controller
    {
        private readonly MessageService messageService;

        public MessageController(MessageService _messageService)
        {
            messageService = _messageService;
        }
        public IActionResult Index()
        {
            return View();
        }


        public record MethodParam(int id, string name);

        public IActionResult Method1([FromBody] MethodParam methodParam)
        {
            return Ok();
        }


        public IActionResult Method2([FromQuery] string name, int id)
        {
            return Ok();
        }

        public IActionResult Method3([FromRoute] int id)
        {
            return Ok();
        }

        public IActionResult Method4([FromForm] MethodParam methodParam)
        {
            return Ok();

        }

    }
}