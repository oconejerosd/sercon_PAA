using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_PAA.Models
{
    public class Requerimiento
    {
        [Key]
        public int RequerimientoId { get; set; }

        [Required]
        public string RequerimientoTipo { get; set; }

        public List<Ticket> Tickets { get; set; }

    }
}