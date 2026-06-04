using CitasApp.Interfaces;
using CitasApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CitasApp.Controllers
{
    public class CitaController : Controller
    {
        private readonly ICitaRepository _citaRepo;
        private readonly IPacienteRepository _pacienteRepo;
        private readonly IMedicoRepository _medicoRepo;

        public CitaController(ICitaRepository citaRepo,
                              IPacienteRepository pacienteRepo,
                              IMedicoRepository medicoRepo)
        {
            _citaRepo = citaRepo;
            _pacienteRepo = pacienteRepo;
            _medicoRepo = medicoRepo;
        }

        public IActionResult Index()
        {
            ViewBag.Pacientes = _pacienteRepo.ObtenerTodos();
            ViewBag.Medicos = _medicoRepo.ObtenerTodos();
            return View(_citaRepo.ObtenerTodos());
        }

        public IActionResult PorPaciente(int pacienteId)
        {
            ViewBag.Pacientes = _pacienteRepo.ObtenerTodos();
            ViewBag.Medicos = _medicoRepo.ObtenerTodos();
            return View(_citaRepo.ObtenerPorPaciente(pacienteId));
        }

        [HttpGet]
        public IActionResult AgregarCita()
        {
            ViewBag.Pacientes = _pacienteRepo.ObtenerTodos();
            ViewBag.Medicos = _medicoRepo.ObtenerTodos();
            return View(new Cita());
        }

        [HttpPost]
        public IActionResult AgregarCita(Cita cita)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Pacientes = _pacienteRepo.ObtenerTodos();
                ViewBag.Medicos = _medicoRepo.ObtenerTodos();
                return View(cita);
            }

            _citaRepo.Agregar(cita);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Eliminar(int id)
        {
            var cita = _citaRepo.ObtenerTodos().FirstOrDefault(c => c.Id == id);
            if (cita == null) return NotFound();

            ViewBag.Pacientes = _pacienteRepo.ObtenerTodos();
            ViewBag.Medicos = _medicoRepo.ObtenerTodos();
            return View(cita);
        }

        [HttpPost, ActionName("Eliminar")]
        public IActionResult EliminarConfirmado(int id)
        {
            _citaRepo.Eliminar(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var cita = _citaRepo.ObtenerTodos().FirstOrDefault(c => c.Id == id);
            if (cita == null) return NotFound();

            ViewBag.Pacientes = _pacienteRepo.ObtenerTodos();
            ViewBag.Medicos = _medicoRepo.ObtenerTodos();
            return View(cita);
        }

        [HttpPost]
        public IActionResult Editar(Cita cita)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Pacientes = _pacienteRepo.ObtenerTodos();
                ViewBag.Medicos = _medicoRepo.ObtenerTodos();
                return View(cita);
            }

            _citaRepo.Actualizar(cita);
            return RedirectToAction("Index");
        }
    }
}