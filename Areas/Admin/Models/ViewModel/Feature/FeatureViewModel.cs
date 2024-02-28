using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace subscription_system.Areas.Admin.Models.ViewModel.Feature
{
    public class FeatureViewModel
    {
        
            public int Id { get; set; }

            [
                Required(ErrorMessage ="Se requiere el nombre de la caracteristica"),
                MinLength(3, ErrorMessage ="El nombre no puede ser menor de 3 caracteres"),
                MaxLength(200, ErrorMessage ="El nombre no puede ser mayor de 200 caracteries"), 
                Display(Name ="Nombre")
                ]
            public string Name { get; set; } = "";

            [Required(ErrorMessage = "Se requiere una descripción de la caracteristica"),
                MinLength(10, ErrorMessage = "El descripción no puede ser menor de 3 caracteres"),
                MaxLength(200, ErrorMessage = "El descripción no puede ser mayor de 500 caracteries"),
                Display(Name = "Nombre")]
            public string Description { get; set; } = "";

        
    }
}
