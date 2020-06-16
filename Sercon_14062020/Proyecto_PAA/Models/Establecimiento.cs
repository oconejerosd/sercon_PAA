using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto_PAA.Models
{
    public class Establecimiento
    {
        [Key]
        public int EstablecimientoId { get; set; }

        [Required]
        public string EstablecimientoRbd { get; set; }

        [Required]
        public string EstablecimientoNombre { get; set; }

        [Required]
        public string EstablecimientoDirecccion { get; set; }

        [Required]
        public string EstablecimientoFono { get; set; }
    }
}