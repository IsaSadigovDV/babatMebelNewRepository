using Microsoft.AspNetCore.Mvc;

namespace BabatMebel.App.Areas.Admin.Controllers
{
    [Area("admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
