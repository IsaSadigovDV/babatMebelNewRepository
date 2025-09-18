using Microsoft.AspNetCore.Mvc;

namespace BabatMebel.App.Controllers
{
    public class ServicesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
