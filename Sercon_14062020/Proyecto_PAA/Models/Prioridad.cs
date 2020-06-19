using System.ComponentModel.DataAnnotations;

namespace Proyecto_PAA.Models
{
    public class Prioridad
    {
        [Key]
        public int PrioridadId { get; set; }

        [Required]
        public string PrioridadTipo { get; set; }
    }
}