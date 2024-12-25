using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
    public class ProducersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
