using Microsoft.AspNetCore.Mvc;

namespace BabatMebel.App.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
