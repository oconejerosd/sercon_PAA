using Proyecto_PAA.Models;
using Proyecto_PAA.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_PAA.Controllers
{
    [Authorize]
    public class EstablecimientoController : Controller
    {
        private readonly ApplicationDbContext db;

        public EstablecimientoController()
        {
            db = new ApplicationDbContext();
        }
        // GET: Establecimiento
        public ActionResult Index(string q)
        {
            var establecimientos = db.Establecimientos.OrderBy(x => x.EstablecimientoId).ToList();
            EstablecimientoViewModel vm = new EstablecimientoViewModel();
            vm.Establecimientos = establecimientos;
            return View(vm);
        }
        public ActionResult Crear()
        {
            EstablecimientoViewModel vm = new EstablecimientoViewModel();
            var establecimientos = db.Establecimientos.OrderBy(x => x.EstablecimientoNombre).ToList();
            vm.Establecimientos = establecimientos;
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(EstablecimientoViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (db.Establecimientos.Any(x => x.EstablecimientoRbd == model.EstablecimientoRbd)) // select * from users where email = leonardo.norambuena@inacap.cl
                {
                    ViewData["ErrorMessage"] = "El RBD ya se encuentra registrado";
                    return View(model);
                }
                var establecimiento = new Establecimiento();
                establecimiento.EstablecimientoRbd = model.EstablecimientoRbd;
                establecimiento.EstablecimientoNombre = model.EstablecimientoNombre;
                establecimiento.EstablecimientoDirecccion = model.EstablecimientoDirecccion;
                establecimiento.EstablecimientoFono = model.EstablecimientoFono;
                db.Establecimientos.Add(establecimiento);
                db.SaveChanges(); // guarda los cambios

                TempData["SuccessMessage"] = "Establecimiento creado correctamente";
                return RedirectToAction("Index", "Establecimiento");
            }
            var establecimientos = db.Establecimientos.OrderBy(x => x.EstablecimientoId).ToList();
            model.Establecimientos = establecimientos;
            return View("Index", model);
        }

        public ActionResult Delete(int id)
        {
            var establecimientos = db.Establecimientos.OrderBy(x => x.EstablecimientoId).ToList();
            var establecimiento = establecimientos.FirstOrDefault(x => x.EstablecimientoId == id);
            if(id == 0 || establecimientos == null)
            {
                TempData["ErrorMessage"] = "El RBD no fue encontrado";
                return RedirectToAction("Index");
            }
            db.Establecimientos.Remove(establecimiento);
            db.SaveChanges();
            TempData["SuccessMessage"] = "Establecimiento eliminado correctamente";
            return RedirectToAction("Index", "Establecimiento");
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var establecimientos = db.Establecimientos.OrderBy(x => x.EstablecimientoId).ToList();
            var establecimiento = establecimientos.FirstOrDefault(x => x.EstablecimientoId == id);
            if (id == 0 || establecimientos == null)
            {
                TempData["ErrorMessage"] = "El RBD no fue encontrado";
                return RedirectToAction("Index");
            }
            EstablecimientoViewModel vm = new EstablecimientoViewModel();
            vm.EstablecimientoId = establecimiento.EstablecimientoId;
            vm.EstablecimientoRbd = establecimiento.EstablecimientoRbd;
            vm.EstablecimientoNombre = establecimiento.EstablecimientoNombre;
            vm.EstablecimientoDirecccion = establecimiento.EstablecimientoDirecccion;
            vm.EstablecimientoFono = establecimiento.EstablecimientoFono;
            return View(vm);
        }

        public ActionResult Update(EstablecimientoViewModel vm)
        {
            var establecimientos = db.Establecimientos.OrderBy(x => x.EstablecimientoId).ToList();
            var establecimiento = establecimientos.FirstOrDefault(x => x.EstablecimientoId == vm.EstablecimientoId);
            if (establecimientos == null)
            {
                TempData["ErrorMessage"] = "El RBD no fue encontrado";
                return RedirectToAction("Index");
            }
            establecimiento.EstablecimientoRbd = vm.EstablecimientoRbd;
            establecimiento.EstablecimientoNombre = vm.EstablecimientoNombre;
            establecimiento.EstablecimientoDirecccion = vm.EstablecimientoDirecccion;
            establecimiento.EstablecimientoFono = vm.EstablecimientoFono;
            db.Entry(establecimiento).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            TempData["SuccessMessage"] = "Establecimiento actualizado correctamente";
            return RedirectToAction("Index", "Establecimiento");
        }
    }
}