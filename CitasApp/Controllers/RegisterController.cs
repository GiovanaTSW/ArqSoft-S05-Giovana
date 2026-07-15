using CitasApp.Application.Services;
using CitasApp.Domain.Models;
using CitasApp.Web.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CitasApp.Web.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        private readonly UsuarioService _usuarioService;

        public RegisterController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (_usuarioService.ObtenerPorEmail(model.Email) != null)
            {
                ModelState.AddModelError("Email", "El email ya está registrado");
                return View(model);
            }

            var usuario = new Usuario
            {
                Nombre = model.Nombre,
                Email = model.Email,
                Password = model.Password
            };

            _usuarioService.Agregar(usuario);
            return RedirectToAction("Index", "Login");
        }
    }
}
