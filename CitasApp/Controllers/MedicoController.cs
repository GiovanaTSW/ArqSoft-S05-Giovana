using CitasApp.Application.Services;
using CitasApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CitasApp.Web.Controllers
{
    public class MedicoController : Controller
    {
        private readonly MedicoService _service;
        public MedicoController(MedicoService service) { _service = service; }

        public IActionResult Index() => View(_service.ObtenerTodos());

        public IActionResult Detalle(int id)
        {
            var medico = _service.ObtenerPorId(id);
            return medico == null ? NotFound() : View(medico);
        }

        [HttpGet]
        public IActionResult AgregarMedico() => View(new Medico());

        [HttpPost]
        public IActionResult AgregarMedico(Medico medico)
        {
            if (!ModelState.IsValid)
                return View(medico);

            _service.Agregar(medico);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Eliminar(int id)
        {
            var medico = _service.ObtenerPorId(id);
            return medico == null ? NotFound() : View(medico);
        }

        [HttpPost, ActionName("Eliminar")]
        public IActionResult EliminarConfirmado(int id)
        {
            _service.Eliminar(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var medico = _service.ObtenerPorId(id);
            return medico == null ? NotFound() : View(medico);
        }

        [HttpPost]
        public IActionResult Editar(Medico medico)
        {
            if (!ModelState.IsValid)
                return View(medico);

            _service.Actualizar(medico);
            return RedirectToAction("Index");
        }
    }
}