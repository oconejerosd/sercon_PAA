using Microsoft.AspNet.Identity;
using Proyecto_PAA.Helpers;
using Proyecto_PAA.Models;
using Proyecto_PAA.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_PAA.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext db;

        public AuthController()
        {
            db = new ApplicationDbContext(); // Graba en la BD
        }
        // GET: Auth
        public ActionResult Listado(string q) // Listado de Usuarios
        {
            var usuarios = db.Users.Include("Establecimiento").OrderBy(x => x.UserId);
            RegisterViewModel vm = new RegisterViewModel();
            vm.Users = usuarios;
            //LlenarCbEstablecimientos();
            return View(vm);
        }
        public ActionResult Crear()
        {
            RegisterViewModel vm = new RegisterViewModel();
            var usuarios = db.Users.OrderBy(x => x.FirstName).ToList();
            vm.Users = usuarios;
            return View(vm);
        }

        [HttpGet]
        public ActionResult Register()
        {
            LlenarCbEstablecimientos();
            RegisterViewModel vm = new RegisterViewModel();
            vm.Roles = db.Roles.OrderBy(x => x.RoleName).ToList();
            return View(vm);
        }
        [Authorize (Roles = "Administrador") ]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            LlenarCbEstablecimientos();
            if (!ModelState.IsValid)
            {
                if (db.Users.Any(x => x.Email == model.Email))
                {
                    ViewData["ErrorMessage"] = "El mail ya se encuentra registrado";
                    return View();
                }
                var user = new User();
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.EstablecimientoId = model.EstablecimientoId;
                user.Email = model.Email;
                user.CreatedAt = DateTime.Now;
                user.UpdatedAt = DateTime.Now;
                byte[] psHash, psSalt;
                CreatePasswordHash(model.Password, out psHash, out psSalt);
                user.PasswordHash = psHash;
                user.PasswordSalt = psSalt;
                var role = db.Roles.Find(model.RoleId);
                if (role == null)
                {
                    TempData["ErrorMessage"] = "Imposible crear al usuario, el rol no existe";
                    return View();
                }
                db.Users.Add(user);
                db.SaveChanges(); // guarda los cambios
                var userRole = new UserRole
                {
                    UserId = user.UserId,
                    RoleId = role.RoleId
                };
                db.UserRoles.Add(userRole);
                db.SaveChanges();
                setSession(user, role);
                TempData["SuccessMessage"] = "Usuario creado correctamente";
                if (role.RoleName == StringHelper.ROLE_TECH)
                {
                    return RedirectToAction("Register", "Auth");
                }
                if (role.RoleName == StringHelper.ROLE_ADMINISTRATOR)
                {
                    return RedirectToAction("Register", "Auth");
                }
                if (role.RoleName == StringHelper.ROLE_CLIENT)
                {
                    return RedirectToAction("Register", "Auth");
                }
            }


            model.Roles = db.Roles.OrderBy(x => x.RoleName).ToList();
            return View(model);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key; 
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)); 
            }
        }

        private bool CheckPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var passwordComputed = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)); 
                for (int i = 0; i < passwordComputed.Length ; i++)
                    if (passwordComputed[i] != passwordHash[i])
                        return false;
            }

            return true;
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            Session["UserId"] = null;
            return View();
        }
        private void setSession(User user, Role rol)
        {
            Session["RolName"] = rol.RoleName;
            Session["UserId"] = user.UserId;
            Session["UserName"] = user.FullName;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignIn(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(x => x.Email == model.Email);

                if (user != null && CheckPassword(model.Password, user.PasswordHash, user.PasswordSalt))
                {
                    await InitOwin(user);
                    TempData["SuccessMessage"] = $"Bienvenido {user.FullName}";
                    var userRole = db.UserRoles.FirstOrDefault(x => x.UserId == user.UserId);
                    var rol = db.Roles.FirstOrDefault(x => x.RoleId == userRole.RoleId);
                    setSession(user, rol);
                    if (rol.RoleName == StringHelper.ROLE_TECH)
                    {
                        return RedirectToAction("Index", "Tecnico");
                    }
                    if (rol.RoleName == StringHelper.ROLE_ADMINISTRATOR)
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Index", "User");
                    }
                    
                   
                }

                TempData["ErrorMessage"] = "Inicio de sesión incorrecto";
                return RedirectToAction("Login");

            }

            TempData["ErrorMessage"] = "Existieron errores de validación";
            return RedirectToAction("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignOut(LoginViewModel model)
        {

            var context = Request.GetOwinContext();
            var authManager = context.Authentication;

            authManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private async Task InitOwin(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim("FullName", user.FullName),

            };
            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            var roles = await db.UserRoles.Where(x => x.UserId == user.UserId).Select(x => x.Role).ToListAsync();
            foreach (var role in roles)
                identity.AddClaim(new Claim(ClaimTypes.Role, role.RoleName));

            var context = Request.GetOwinContext();
            var authManager = context.Authentication;

            authManager.SignIn(identity);

        }
        [Authorize(Roles ="Administrador")]
        public void LlenarCbEstablecimientos()
        {
            lista = (from f in db.Establecimientos
                     select new EstablecimientoViewModel
                     {
                         EstablecimientoId = f.EstablecimientoId,
                         EstablecimientoNombre = f.EstablecimientoNombre
                     }).ToList();
            List<SelectListItem> establecimientos = lista.ConvertAll(f =>
            {
                return new SelectListItem()
                {
                    Text = f.EstablecimientoNombre.ToString(),
                    Value = f.EstablecimientoId.ToString(),
                    Selected = false
                };
            });
            ViewBag.establecimientos = establecimientos;
        }

        public ActionResult Delete(int id)
        {
            var usuarios = db.Users.OrderBy(x => x.UserId).ToList();
            var usuario = usuarios.FirstOrDefault(x => x.UserId == id);
            if (id == 0 || usuarios == null)
            {
                TempData["ErrorMessage"] = "El Usuario no fue encontrado";
                return RedirectToAction("Index");
            }
            db.Users.Remove(usuario);
            db.SaveChanges();
            TempData["SuccessMessage"] = "Usuario eliminado correctamente";
            return RedirectToAction("Listado", "Auth");
        }
        List<EstablecimientoViewModel> lista = null;
    }
}