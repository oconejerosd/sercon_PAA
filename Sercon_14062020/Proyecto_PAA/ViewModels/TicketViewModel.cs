using Proyecto_PAA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_PAA.ViewModels
{
    public class TicketViewModel
    {
        public Ticket Ticket { get; set; }

        public List<Ticket> Tickets { get; set; }
        public List<Estado> Estados { get; set; }
        public List<Requerimiento> Requerimientos { get; set; }
        public List<Prioridad> Prioridades { get; set; }
        public List<User> Users { get; set; }
        public List<Establecimiento> Establecimientos { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public List<Role> Roles { get; set; }

    }
}