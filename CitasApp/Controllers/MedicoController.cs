using CitasApp.Interfaces;
using CitasApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CitasApp.Controllers
{
    public class MedicoController : Controller
    {
        private readonly IMedicoRepository _repo;
        public MedicoController(IMedicoRepository repo) { _repo = repo; }

        public IActionResult Index() => View(_repo.ObtenerTodos());

        public IActionResult Detalle(int id)
        {
            var medico = _repo.ObtenerPorId(id);
            return medico == null ? NotFound() : View(medico);
        }

        [HttpGet]
        public IActionResult AgregarMedico() => View(new Medico());

        [HttpPost]
        public IActionResult AgregarMedico(Medico medico)
        {
            if (!ModelState.IsValid)
                return View(medico);

            _repo.Agregar(medico);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Eliminar(int id)
        {
            var medico = _repo.ObtenerPorId(id);
            return medico == null ? NotFound() : View(medico);
        }

        [HttpPost, ActionName("Eliminar")]
        public IActionResult EliminarConfirmado(int id)
        {
            _repo.Eliminar(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var medico = _repo.ObtenerPorId(id);
            return medico == null ? NotFound() : View(medico);
        }

        [HttpPost]
        public IActionResult Editar(Medico medico)
        {
            if (!ModelState.IsValid)
                return View(medico);

            _repo.Actualizar(medico);
            return RedirectToAction("Index");
        }
    }
}