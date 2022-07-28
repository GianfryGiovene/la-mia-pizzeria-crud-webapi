using Microsoft.AspNetCore.Mvc;

namespace LaMiaPizzeria.Controllers
{
    public class MessaggioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
