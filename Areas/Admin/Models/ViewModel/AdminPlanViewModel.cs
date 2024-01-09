using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace subscription_system.Areas.Admin.Models.ViewModel
{
    public class AdminPlanViewModel
    {

        public int Id { get; set; }

        [
           Display(Name = "Nombre"),
           Required(ErrorMessage = "Debes de ingresar un nombre "),
           MaxLength(150),
           MinLength(5)
       ]
        public string Name { set; get; } = "";

        [

            Display(Name = "Descripción"),
            Required(ErrorMessage = " Debes de Ingresar una descripción"),
            MaxLength(250, ErrorMessage = "Excediste la catidad maxima de caracteres"),
            MinLength(10, ErrorMessage = "La catidad de caracteres minima es de 10")
        ]
        public string Description { set; get; } = "";

        [
            Display(Name = "Precio"),
            Required(ErrorMessage = " Debes de Ingresar un precio"),
            DisplayFormat(DataFormatString = "{0:C}"),
            Range(0, float.MaxValue, ErrorMessage = "El valor debe ser un número positivo")
        ]
        public float Price { set; get; } = 0;

        [
            Display(Name = "Estado"),
            Required
        ]
        public bool Active { set; get; } = true;

        [
            Display(Name = "Pertiodo"),
            Required(ErrorMessage = "Debes de ingresar el periodo de vigiencia de la subscripción"),
            Range(1, 12, ErrorMessage = "El valor debe de estar entre 1 y 12")
        ]
        public int BillingPeriod { set; get; } = 1;

        [
             Display(Name = "Periodo de prueba"),
            Required(ErrorMessage = "Debes de Ingresar la cantidad de dias de prueba"),
            Range(1, 30, ErrorMessage = "El valor debe de estar entre 1 y 30")
        ]
        public int TrialPeriod { set; get; } = 0;
    }
}
