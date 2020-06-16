using Proyecto_PAA.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto_PAA.ViewModels
{
    public class EstablecimientoViewModel
    {
        public string q { get; set; }

        public List<Establecimiento> Establecimientos { get; set; }

        public int EstablecimientoId { get; set; }

        [Required]
        [Display(Name ="Rol BD")]
        public string EstablecimientoRbd { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string EstablecimientoNombre { get; set; }

        [Required]
        [Display(Name = "Direccion")]
        public string EstablecimientoDirecccion { get; set; }

        [Required]
        [Display(Name = "Fono")]
        public string EstablecimientoFono { get; set; }
    }
}