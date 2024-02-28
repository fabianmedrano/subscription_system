using Microsoft.AspNetCore.Mvc;
using subscription_system.Enums;

namespace subscription_system.Extensions
{

  
    public class BaseController :Controller
    {

        [TempData]
        public String notification { get; set; } = " putos todos";
        public void Alert(NotificationType type, string mjs, string title ="") {
            notification = $" Swal.fire( '{title}','{mjs}','{type}');";
        }

        /// </summary>
        /// <param name="message">The message to display to the user.</param>
        /// <param name="notifyType">The type of notification to display to the user: Success, Error or Warning.</param>
        public void Confirmation(NotificationType type, string mjs, string actionName, string controllerName, string title = "", string confirmButtonText = " ",string cancelButtonColor = "#d33", string confirmButtonColor = "#3085d6", bool showCancelButton = true)
        {
            notification = $@"Swal.fire({{ title: '{title}', text: '{mjs}', icon: '{type}', showCancelButton: {showCancelButton.ToString().ToLower()}, confirmButtonColor: '{confirmButtonColor}', cancelButtonColor: '{cancelButtonColor}', confirmButtonText: '{confirmButtonText}' }})
                .then((result) => {{
                if (result.isConfirmed) {{
                    $.ajax({{
                        url: '{Url.Action(actionName, controllerName)}',
                        type: 'POST',
                        success: function (data) {{
                            console.log('Petición enviada con éxito');
                        }},
                        error: function (error) {{
                            console.error('Error al enviar la petición:', error);
                        }}
                    }});
                }}
                Swal.fire({{
                    title: 'Deleted!',
                    text: 'Your file has been deleted.',
                    icon: 'success'
                }});
            }});";
        }

        // TODO: aqui hay que probar si esto funciona
        // FIX:
        // IDEA: CREO QUE PUEDE SER MEJOR TENERLO COMO STATICO Y SIMPLEMENTE IMPORTARLO CADA VER QUE LO OCUPO
        //  OPTIMIZE:
        // NOTE: ESTO HAY QUE VER DONDE PONERLO REALMENTE PORQUE NOSE SI ES EL MEJOR LUGAR PARA TENERLO 
        // BUG:

        public static List<TTarget> ConvertObjects<TSource, TTarget>(List<TSource> sourceList, TTarget targetObject)
                where TSource : class
                where TTarget : class
        {
            var targetList = new List<TTarget>();

            foreach (var sourceObj in sourceList)
            {
                if (sourceObj is TTarget)
                {
                    // Si el objeto de origen ya es del tipo de destino, agrégalo directamente
                    targetList.Add(sourceObj as TTarget);
                }
                else
                {
                    // Maneja la lógica de conversión (por ejemplo, crea un nuevo objeto de destino a partir de sourceObj)
                    // Para fines de demostración, asumamos que TTarget tiene un constructor que acepta TSource
                   // var targetObj = Activator.CreateInstance(typeof(TTarget), sourceObj) as TTarget;
                    var targetObj = (TTarget)Convert.ChangeType(sourceObj, typeof(TTarget));
                    targetList.Add(targetObj);
                }
            }

            return targetList;
        }



    }
}

