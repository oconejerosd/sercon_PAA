using Proyecto_PAA.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_PAA.ViewModels
{
    public class RegisterViewModel
    {
        public string q { get; set; }
        public List<Establecimiento> Establecimientos { get; set; }

        public IEnumerable<User> Users { get; set; }

        [Required]
        [MaxLength(50)] // FirstName Varchar(50)
        [Display(Name = "Nombre")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Apellido")]
        public string LastName { get; set; }
        [Required]
        public int EstablecimientoId { get; set; }
        [Required]
        public string EstablecimientoNombre { get; set; }
        [Required]
        [MaxLength(50)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        [Display(Name = "Confirme Contraseña")]
        public string ConfirmPassword { get; set; }
        [Required]
        public int RoleId { get; set; }

        public List<Role> Roles { get; set; }
    }
}