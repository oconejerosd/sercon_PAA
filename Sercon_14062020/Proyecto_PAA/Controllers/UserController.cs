﻿using Proyecto_PAA.Helpers;
using Proyecto_PAA.Models;
using Proyecto_PAA.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace Proyecto_PAA.Controllers
{
    
    public class UserController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        // GET: User
        public ActionResult Index()
        {
            if (checkUser() == false)
            {
                return RedirectToAction("Login", "User");
            }
            var listado = context.Tickets;
            return View(listado);
           
        }
        public bool checkUser()
        {
            if (Session["RolName"] != null)
            {
                if (Session["RolName"].ToString() == StringHelper.ROLE_CLIENT)
                {
                    return true;
                }
            }
            return false;
        }
        [HttpGet]
        public ActionResult Nuevo()
        {
            var viewmodel = new TicketViewModel();
            viewmodel.Estados = context.Estados.ToList();
            viewmodel.Users = context.Users.ToList();
            return View(viewmodel);


        }
        [HttpPost]
        public ActionResult Nuevo(Ticket ticket)
        {
            context.Tickets.Add(ticket);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult Listado()
        {
            return View();
        }
        

    }
}