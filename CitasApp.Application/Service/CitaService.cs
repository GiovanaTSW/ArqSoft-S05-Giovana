using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CitasApp.Application.Services
{
    public class CitaService
    {
        private readonly ICitaRepository _repo;
        private readonly List<ICitaObserver> _observers = new();

        //resibe el repo por el constructor
        public CitaService(ICitaRepository repo)
        {
            _repo = repo;
        }
        public void AgregarObserver(ICitaObserver observer) => _observers.Add(observer);

        public List<Cita> ObtenerTodos() => _repo.ObtenerTodos();

        public List<Cita> ObtenerPorPaciente(int Id)
        {
            return _repo.ObtenerPorPaciente(Id);
        }
        public void Agregar(Cita cita)
        {
            _repo.Agregar(cita);
        }

        public Cita? Confirmar(int id)
        {
            var cita = _repo.Confirmar(id);
            if (cita == null) return null;

            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Cita {id} confirmada");

            foreach (var obs in _observers)
                obs.Notificar(cita);

            return cita;
        }

    }
}
