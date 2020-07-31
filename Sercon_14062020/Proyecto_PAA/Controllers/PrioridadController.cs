using Proyecto_PAA.Models;
using Proyecto_PAA.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace Proyecto_PAA.Controllers
{
    public class PrioridadController : Controller
    {
        private readonly ApplicationDbContext db;

        public PrioridadController()
        {
            db = new ApplicationDbContext();
        }
        // GET: Prioridad
        public ActionResult Index(string q)
        {
            var prioridades = db.Prioridades.OrderBy(x => x.PrioridadId).ToList();
            PrioridadViewModel vm = new PrioridadViewModel();
            vm.Prioridades = prioridades;
            return View(vm);
        }
        public ActionResult Crear()
        {
            PrioridadViewModel vm = new PrioridadViewModel();
            var prioridades = db.Prioridades.OrderBy(x => x.PrioridadTipo).ToList();
            vm.Prioridades = prioridades;
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(PrioridadViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (db.Prioridades.Any(x => x.PrioridadTipo == model.PrioridadTipo)) 
                {
                    ViewData["ErrorMessage"] = "La prioridad ya se encuentra registrada";
                    return View(model);
                }
                var prioridad = new Prioridad();
                prioridad.PrioridadTipo = model.PrioridadTipo;
                db.Prioridades.Add(prioridad);
                db.SaveChanges(); // guarda los cambios

                TempData["SuccessMessage"] = "Prioridad creada correctamente";
                return RedirectToAction("Index", "Prioridad");
            }
            var prioridades = db.Prioridades.OrderBy(x => x.PrioridadId).ToList();
            model.Prioridades = prioridades;
            return View("Index", model);
        }

        public ActionResult Delete(int id)
        {
            var prioridades = db.Prioridades.OrderBy(x => x.PrioridadId).ToList();
            var prioridad = prioridades.FirstOrDefault(x => x.PrioridadId == id);
            if (id == 0 || prioridades == null)
            {
                TempData["ErrorMessage"] = "La prioridad no fue encontrada";
                return RedirectToAction("Index");
            }
            db.Prioridades.Remove(prioridad);
            db.SaveChanges();
            TempData["SuccessMessage"] = "Prioridad eliminada correctamente";
            return RedirectToAction("Index", "Prioridad");
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var prioridades = db.Prioridades.OrderBy(x => x.PrioridadId).ToList();
            var prioridad = prioridades.FirstOrDefault(x => x.PrioridadId == id);
            if (id == 0 || prioridades == null)
            {
                TempData["ErrorMessage"] = "La prioridad no fue encontrada";
                return RedirectToAction("Index");
            }
            PrioridadViewModel vm = new PrioridadViewModel();
            vm.PrioridadId = prioridad.PrioridadId;
            vm.PrioridadTipo = prioridad.PrioridadTipo;
            return View(vm);
        }

        public ActionResult Update(PrioridadViewModel vm)
        {
            var prioridades = db.Prioridades.OrderBy(x => x.PrioridadId).ToList();
            var prioridad = prioridades.FirstOrDefault(x => x.PrioridadId == vm.PrioridadId);
            if (prioridades == null)
            {
                TempData["ErrorMessage"] = "La prioridad no fue encontrada";
                return RedirectToAction("Index");
            }
            prioridad.PrioridadTipo = vm.PrioridadTipo;
            db.Entry(prioridad).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            TempData["SuccessMessage"] = "Prioridad actualizada correctamente";
            return RedirectToAction("Index", "Prioridad");
        }


    }
}