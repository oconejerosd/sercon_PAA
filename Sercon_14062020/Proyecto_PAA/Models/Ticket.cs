using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_PAA.Models
{
    public class Ticket
    {
        [Key]
        public int id { get; set; }
        [Required]
        public DateTime FechaGeneracion { get; set; }
        [Required]
        public int idEstado { get; set; }
        [ForeignKey("idEstado")]
        public Estado Estado { get; set; }
        [Required]
        public int idRequerimiento { get; set; }
        [ForeignKey("idRequerimiento")]
        public Requerimiento Requerimiento { get; set; }
        [Required]
        public int PrioridadId { get; set; }
        [ForeignKey("PrioridadId")]
        public Prioridad Prioridad { get; set; }
        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        [Required]
        [MaxLength(8000)]
        public string Descripcion { get; set; }
        [MaxLength(8000)]
        public string Solucion { get; set; }
        

    }
}