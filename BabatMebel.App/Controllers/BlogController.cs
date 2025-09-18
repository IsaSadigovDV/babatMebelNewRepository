using Microsoft.AspNetCore.Mvc;

namespace BabatMebel.App.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
