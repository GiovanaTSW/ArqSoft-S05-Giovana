using CitasApp.Application.Services;
using CitasApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CitasApp.Web.Controllers
{
    public class CitaController : Controller
    {
        private readonly CitaService _citaService;
        private readonly PacienteService _pacienteService;
        private readonly MedicoService _medicoService;

        public CitaController(CitaService citaService,
                              PacienteService pacienteService,
                              MedicoService medicoService)
        {
            _citaService = citaService;
            _pacienteService = pacienteService;
            _medicoService = medicoService;
        }

        private void CargarViewBags()
        {
            ViewBag.Pacientes = _pacienteService.ObtenerTodos();
            ViewBag.Medicos = _medicoService.ObtenerTodos();
        }

        public IActionResult Index()
        {
            CargarViewBags();
            return View(_citaService.ObtenerTodos());
        }

        public IActionResult PorPaciente(int pacienteId)
        {
            CargarViewBags();
            return View(_citaService.ObtenerPorPaciente(pacienteId));
        }

        [HttpGet]
        public IActionResult AgregarCita()
        {
            CargarViewBags();
            return View(new Cita());
        }

        [HttpPost]
        public IActionResult AgregarCita(Cita cita)
        {
            if (!ModelState.IsValid)
            {
                CargarViewBags();
                return View(cita);
            }

            _citaService.Agregar(cita);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Eliminar(int id)
        {
            var cita = _citaService.ObtenerTodos().FirstOrDefault(c => c.Id == id);
            if (cita == null) return NotFound();

            CargarViewBags();
            return View(cita);
        }

        [HttpPost, ActionName("Eliminar")]
        public IActionResult EliminarConfirmado(int id)
        {
            _citaService.Eliminar(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var cita = _citaService.ObtenerTodos().FirstOrDefault(c => c.Id == id);
            if (cita == null) return NotFound();

            CargarViewBags();
            return View(cita);
        }

        [HttpPost]
        public IActionResult Editar(Cita cita)
        {
            if (!ModelState.IsValid)
            {
                CargarViewBags();
                return View(cita);
            }

            _citaService.Actualizar(cita);
            return RedirectToAction("Index");
        }
    }
}