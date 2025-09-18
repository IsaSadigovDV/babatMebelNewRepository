using Microsoft.AspNetCore.Mvc;

namespace BabatMebel.App.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
