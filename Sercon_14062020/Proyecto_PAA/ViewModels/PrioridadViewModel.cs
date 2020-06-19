using Proyecto_PAA.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto_PAA.ViewModels
{
    public class PrioridadViewModel
    {
        public string q { get; set; }
        public List<Prioridad> Prioridades { get; set; }
        public int PrioridadId { get; set; }
        [Required]
        [Display(Name = "Tipo de Prioridad")]
        public string PrioridadTipo { get; set; }
    }
}