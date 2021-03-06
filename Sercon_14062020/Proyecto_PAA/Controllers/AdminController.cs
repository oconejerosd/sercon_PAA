﻿using Proyecto_PAA.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_PAA.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class AdminController : Controller
    {
        // GET: Admin
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
                if (Session["RolName"].ToString() == StringHelper.ROLE_ADMINISTRATOR)
                {
                    return true;
                }
            }
            return false;
        }
        public ActionResult Asignacion()
        {  
                return View();
        }
        public ActionResult Informes()
        {
            return View();

        }
    }
}