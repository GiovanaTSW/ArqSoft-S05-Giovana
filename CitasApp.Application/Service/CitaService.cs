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
        //resibe el repo por el constructor
        public CitaService(ICitaRepository repo)
        {
            _repo = repo;
        }
        public List<Cita> ObtenerTodos()
        {
            return _repo.ObtenerTodos();
        }

        public List<Cita> ObtenerPorPaciente(int Id)
        {
            return _repo.ObtenerPorPaciente(Id);
        }
        public void Agregar(Cita cita)
        {
            _repo.Agregar(cita);
        }

    }
}
