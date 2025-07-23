using Microsoft.AspNetCore.Mvc;

namespace GadgetHub.API.Controllers
{
    public class OrdersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
