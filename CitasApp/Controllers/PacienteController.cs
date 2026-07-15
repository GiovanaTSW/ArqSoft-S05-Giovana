using CitasApp.Application.Services;
using CitasApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CitasApp.Web.Controllers
{
    public class PacienteController : Controller
    {
        private readonly PacienteService _service;
        public PacienteController(PacienteService service) { _service = service; }

        public IActionResult Index() => View(_service.ObtenerTodos());

        public IActionResult Detalle(int id)
        {
            var paciente = _service.ObtenerPorId(id);
            return paciente == null ? NotFound() : View(paciente);
        }

        [HttpGet]
        public IActionResult AgregarPaciente() => View(new Paciente());

        [HttpPost]
        public IActionResult AgregarPaciente(Paciente paciente)
        {
            if (!ModelState.IsValid)
                return View(paciente);

            _service.Agregar(paciente);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Eliminar(int id)
        {
            var paciente = _service.ObtenerPorId(id);
            return paciente == null ? NotFound() : View(paciente);
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
            var paciente = _service.ObtenerPorId(id);
            return paciente == null ? NotFound() : View(paciente);
        }

        [HttpPost]
        public IActionResult Editar(Paciente paciente)
        {
            if (!ModelState.IsValid)
                return View(paciente);

            _service.Actualizar(paciente);
            return RedirectToAction("Index");
        }
    }
}