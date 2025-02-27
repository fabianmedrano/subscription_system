using Elfie.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace subscription_system.Views.Shared.ViewComponents {
    public class AreaSidebarViewComponent : ViewComponent {
        public IViewComponentResult Invoke(string area) {
            return View(  
                (area =="admin")? "~/Areas/Admin/Views/Shared/_SideBarPartial.cshtml" : "~/Areas/User/Views/Shared/_SideBarPartial.cshtml"
                );
        }
    }
}
