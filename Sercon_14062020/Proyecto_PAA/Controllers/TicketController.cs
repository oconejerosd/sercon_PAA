using Proyecto_PAA.Models;
using Proyecto_PAA.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_PAA.Controllers
{
    public class TicketController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        // GET: Ticket
        public ActionResult Index()
        {
            var listado = context.Tickets;
            return View(listado);
        }
        [HttpGet]
        public ActionResult Nuevo()
        {
            var viewmodel = new TicketViewModel();
            viewmodel.Requerimientos = context.Requerimientos.ToList();
            viewmodel.Estados = context.Estados.ToList();
            viewmodel.Users = context.Users.ToList();
            viewmodel.Prioridades = context.Prioridades.ToList();
            return View(viewmodel);
        }
        [HttpPost]
        public ActionResult Nuevo(Ticket ticket)
        {
            context.Tickets.Add(ticket);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
    
}