using Proyecto_PAA.Models;
using Proyecto_PAA.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_PAA.Controllers
{
    public class RequerimientoController : Controller
    {
        private readonly ApplicationDbContext db;

        public RequerimientoController()
        {
            db = new ApplicationDbContext();
        }
        // GET: Requerimiento
        public ActionResult Index(string q)
        {
            var requerimientos = db.Requerimientos.OrderBy(x => x.RequerimientoId).ToList();
            RequerimientosViewModel vm = new RequerimientosViewModel();
            vm.Requerimientos = requerimientos;
            return View(vm);
        }
        public ActionResult Crear()
        {
            RequerimientosViewModel vm = new RequerimientosViewModel();
            var requerimientos = db.Requerimientos.OrderBy(x => x.RequerimientoTipo).ToList();
            vm.Requerimientos = requerimientos;
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(RequerimientosViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (db.Requerimientos.Any(x => x.RequerimientoTipo == model.RequerimientoTipo)) // select * from users where email = leonardo.norambuena@inacap.cl
                {
                    ViewData["ErrorMessage"] = "El Requerimiento ya se encuentra registrado";
                    return View(model);
                }
                var requerimiento = new Requerimiento();
                requerimiento.RequerimientoTipo = model.RequerimientoTipo;
                db.Requerimientos.Add(requerimiento);
                db.SaveChanges(); // guarda los cambios

                TempData["SuccessMessage"] = "Requerimiento creado correctamente";
                return RedirectToAction("Index", "Requerimiento");
            }
            var requerimientos = db.Requerimientos.OrderBy(x => x.RequerimientoId).ToList();
            model.Requerimientos = requerimientos;
            return View("Index", model);
        }

        public ActionResult Delete(int id)
        {
            var requerimientos = db.Requerimientos.OrderBy(x => x.RequerimientoId).ToList();
            var requerimiento = requerimientos.FirstOrDefault(x => x.RequerimientoId == id);
            if (id == 0 || requerimientos == null)
            {
                TempData["ErrorMessage"] = "El Requerimiento no fue encontrado";
                return RedirectToAction("Index");
            }
            db.Requerimientos.Remove(requerimiento);
            db.SaveChanges();
            TempData["SuccessMessage"] = "Requerimiento eliminado correctamente";
            return RedirectToAction("Index", "Requerimiento");
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var requerimientos = db.Requerimientos.OrderBy(x => x.RequerimientoId).ToList();
            var requerimiento = requerimientos.FirstOrDefault(x => x.RequerimientoId == id);
            if (id == 0 || requerimientos == null)
            {
                TempData["ErrorMessage"] = "El Requerimiento no fue encontrado";
                return RedirectToAction("Index");
            }
            RequerimientosViewModel vm = new RequerimientosViewModel();
            vm.RequerimientoId = requerimiento.RequerimientoId;
            vm.RequerimientoTipo = requerimiento.RequerimientoTipo;
            return View(vm);
        }

        public ActionResult Update(RequerimientosViewModel vm)
        {
            var requerimientos = db.Requerimientos.OrderBy(x => x.RequerimientoId).ToList();
            var requerimiento = requerimientos.FirstOrDefault(x => x.RequerimientoId == vm.RequerimientoId);
            if (requerimientos == null)
            {
                TempData["ErrorMessage"] = "El Requerimiento no fue encontrado";
                return RedirectToAction("Index");
            }
            requerimiento.RequerimientoTipo = vm.RequerimientoTipo;
            db.Entry(requerimiento).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            TempData["SuccessMessage"] = "Requerimiento actualizado correctamente";
            return RedirectToAction("Index", "Requerimiento");
        }


    }
}