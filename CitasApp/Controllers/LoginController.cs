using CitasApp.Application.Services;
using CitasApp.Web.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CitasApp.Web.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly UsuarioService _usuarioService;

        public LoginController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var usuario = _usuarioService.ObtenerPorEmail(model.Email);

            if (usuario == null || usuario.Password != model.Password)
            {
                ModelState.AddModelError("", "Email o contraseña incorrectos");
                return View(model);
            }

            HttpContext.Session.SetInt32("UsuarioId", usuario.Id);
            HttpContext.Session.SetString("UsuarioNombre", usuario.Nombre);
            return RedirectToAction("Index", "Cita");
        }

        public IActionResult Salir()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
