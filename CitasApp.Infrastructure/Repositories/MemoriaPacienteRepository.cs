using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Infrastructure.Repositories
{
    public class MemoriaPacienteRepository : IPacienteRepository
    {
        private readonly List<Paciente> _pacientes = new();
        private int _nextId = 1;

        public List<Paciente> ObtenerTodos()
        {
            return new List<Paciente>(_pacientes);
        }

        public Paciente? ObtenerPorId(int id)
        {
            return _pacientes.FirstOrDefault(p => p.Id == id);
        }

        public void Agregar(Paciente paciente)
        {
            paciente.Id = _nextId++;
            _pacientes.Add(paciente);
        }

        public void Eliminar(int id)
        {
            var paciente = _pacientes.FirstOrDefault(p => p.Id == id);
            if (paciente != null) _pacientes.Remove(paciente);
        }

        public void Actualizar(Paciente paciente)
        {
            var existente = _pacientes.FirstOrDefault(p => p.Id == paciente.Id);
            if (existente != null)
            {
                existente.Nombre = paciente.Nombre;
                existente.Apellido = paciente.Apellido;
                existente.Email = paciente.Email;
                existente.Telefono = paciente.Telefono;
            }
        }
    }
}
