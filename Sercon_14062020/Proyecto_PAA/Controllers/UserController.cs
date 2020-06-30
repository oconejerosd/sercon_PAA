using Proyecto_PAA.Helpers;
using Proyecto_PAA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_PAA.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            if (checkUser() == false)
            {
                return RedirectToAction("Login", "User");
            }
            return View();
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
        public ActionResult Nuevo(string q)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var listado = context.Tickets.ToList();
                return View(listado);
            }


        }

    }
}