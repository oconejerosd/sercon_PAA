using Proyecto_PAA.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto_PAA.ViewModels
{
    public class RequerimientosViewModel
    {
        public string q { get; set; }
        public List<Requerimiento> Requerimientos { get; set; }
        public int RequerimientoId { get; set; }
        [Required]
        [Display(Name ="Tipo de Requerimiento")]
        public string RequerimientoTipo { get; set; }
    }
}