using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace subscription_system.Models
{

    [Authorize(Roles = "SystemAdmin")]
    public class Feature
    {
        public int Id { get; set; }
        
        [Required, Column(TypeName = "varchar(200)")]
        public string Name { get; set; } = "";

        [Required, Column(TypeName ="varchar(500)")]
        public string Description { get; set; } ="";

    }
}
