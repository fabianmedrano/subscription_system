using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using subscription_system.Data;
using subscription_system.Models;
using System.Threading.Tasks;

namespace subscription_system.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class SubscriptionMiddleware
    {
        private readonly RequestDelegate _next;
        public SubscriptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {

            if (!httpContext.Request.Path.StartsWithSegments("/UserSubscriptions")) { 
                // Comprobar si el usuario está autenticado
                if (httpContext.User.Identity!.IsAuthenticated  )
                {
                    // Obtener el nombre del usuario
                    string ? userName = httpContext.User.Identity.Name;

                    // Obtener el plan del usuario usando el contexto de la base de datos
                    if (userName != null)
                        using (var scope = httpContext.RequestServices.CreateScope())
                        {
                            var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                        
                            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
                            if (user != null)
                            {
                                var subscription = await _context.Subscriptions
                                    .Where(u => (u.UserId == user.Id) && (u.IsActive == true) && (u.EndDate >= DateTime.Now))
                                    .FirstOrDefaultAsync();

                                // Comprobar si el usuario tiene un plan válido
                                if (subscription == null  )
                                {
                                    httpContext.Response.Redirect($"/UserSubscriptions/{user.Id}/Index");
                                    return;

                                };
                            }
                    }
                }
            }
            await _next(httpContext);
        }

    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class SubscriptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseSubscriptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SubscriptionMiddleware>();
        }
    }
}
