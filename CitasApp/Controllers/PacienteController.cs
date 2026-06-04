using CitasApp.Interfaces;
using CitasApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CitasApp.Controllers
{
    public class PacienteController : Controller
    {
        private readonly IPacienteRepository _repo;
        public PacienteController(IPacienteRepository repo) { _repo = repo; }

        public IActionResult Index() => View(_repo.ObtenerTodos());

        public IActionResult Detalle(int id)
        {
            var paciente = _repo.ObtenerPorId(id);
            return paciente == null ? NotFound() : View(paciente);
        }

        [HttpGet]
        public IActionResult AgregarPaciente() => View(new Paciente());

        [HttpPost]
        public IActionResult AgregarPaciente(Paciente paciente)
        {
            if (!ModelState.IsValid)
                return View(paciente);

            _repo.Agregar(paciente);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Eliminar(int id)
        {
            var paciente = _repo.ObtenerPorId(id);
            return paciente == null ? NotFound() : View(paciente);
        }

        [HttpPost, ActionName("Eliminar")]
        public IActionResult EliminarConfirmado(int id)
        {
            _repo.Eliminar(id);
            return RedirectToAction("Index");
        }
    }
}