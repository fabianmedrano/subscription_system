using Microsoft.AspNetCore.Mvc;

namespace subscription_system.Areas.User.Controllers
{
    public class DashboardController : Controller
    {
        [Area("User")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
