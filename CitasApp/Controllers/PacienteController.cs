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

        public IActionResult AgregarPaciente(int id)
        {
            var paciente = _repo.ObtenerPorId(id);
            return paciente == null ? NotFound() : View(paciente);
        }

        //GET: Paciente/AgregarPaciente
        [HttpGet]
        public IActionResult AgregarPaciente() => View(new Paciente());

        //POST: Paciente/AgregarPaciente
        [HttpPost]
        public IActionResult AgregarPaciente(Paciente paciente)
        {
            if (!ModelState.IsValid)
                return View(paciente);

            _repo.Agregar(paciente);
            return RedirectToAction("Index");
        }
    }
}