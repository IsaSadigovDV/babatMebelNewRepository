using Microsoft.AspNetCore.Mvc;

namespace BabatMebel.App.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
