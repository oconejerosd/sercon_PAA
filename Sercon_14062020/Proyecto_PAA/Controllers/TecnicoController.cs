using Proyecto_PAA.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_PAA.Controllers
{
    [Authorize]
    public class TecnicoController : Controller
    {
        // GET: Tecnico
        public ActionResult Index()
        {
            if (checkUser() == false)
            {
                return RedirectToAction("Login", "Auth");
            }
            return View();
        }

        public bool checkUser()
        {
            if (Session["RolName"] != null)
            {
                 if (Session["RolName"].ToString() == StringHelper.ROLE_TECH)
                {
                    return true;
                }
            }
            return false;
        }
    }
}