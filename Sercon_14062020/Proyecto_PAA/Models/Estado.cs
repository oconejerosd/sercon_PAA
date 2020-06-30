using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto_PAA.Models
{
    public class Estado
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Activo { get; set; }

    }
}