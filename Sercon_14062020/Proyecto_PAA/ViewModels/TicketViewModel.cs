using Proyecto_PAA.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Proyecto_PAA.ViewModels
{
    public class TicketViewModel
    {
        public string q { get; set; }
        
        public IEnumerable<Ticket> Tickets { get; set; }

        [Required]
        public System.DateTime FechaGeneracion { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        public int idEstado { get; set; }
        public Estado Estado { get; set; }

        [Required]
        public int idRequerimiento { get; set; }
        public Requerimiento Requerimiento { get; set; }

        [Required]
        public int PrioridadId { get; set; }
        public Prioridad Prioridad { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        [MaxLength(8000)]
        public string Descripcion { get; set; }

        [MaxLength(8000)]
        public string Solucion { get; set; }
    }
}