using Elfie.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace subscription_system.Views.Shared.ViewComponents {
    public class AreaNavigationViewComponent :ViewComponent {

        public IViewComponentResult Invoke(string area) { 
            return View(
                (area == "admin")? "~/Areas/Admin/Views/Shared/_NavPartial.cshtml" : "~/Areas/User/Views/Shared/_NavPartial.cshtml"
            );

      
        }
    }
}
