using Microsoft.AspNetCore.Mvc;

namespace subscription_system.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
