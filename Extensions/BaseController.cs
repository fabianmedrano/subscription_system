using Microsoft.AspNetCore.Mvc;
using subscription_system.Enums;

namespace subscription_system.Extensions
{

  
    public class BaseController :Controller
    {

        [TempData]
        public String notification { get; set; } = " putos todos";
        public void Alert(string mjs, NotificationType type, string title ="") {

            notification = $" Swal.fire( {title},{mjs},{type});";
        //    TempData.Add("notification", "puitos");
        }

        /// </summary>
        /// <param name="message">The message to display to the user.</param>
        /// <param name="notifyType">The type of notification to display to the user: Success, Error or Warning.</param>
        public void Message(string message, NotificationType notifyType)
        {
            TempData["Notification2"] = message;

            switch (notifyType)
            {
                case NotificationType.Success:
                    TempData["NotificationCSS"] = "alert-box success";
                    break;
                case NotificationType.Error:
                    TempData["NotificationCSS"] = "alert-box errors";
                    break;
                case NotificationType.Warning:
                    TempData["NotificationCSS"] = "alert-box warning";
                    break;

                case NotificationType.Info:
                    TempData["NotificationCSS"] = "alert-box notice";
                    break;
            }
        }
    }
}

