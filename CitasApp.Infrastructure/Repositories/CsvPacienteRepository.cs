// CitasApp.Infrastructure/Repositories/CsvPacienteRepository.cs
// Adapter de salida — implementa IPacienteRepository leyendo un archivo CSV

using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Infrastructure.Repositories
{
    public class CsvPacienteRepository : IPacienteRepository
    {
        private readonly string _filePath;

        public CsvPacienteRepository(string filePath)
        {
            _filePath = filePath;

            if (!File.Exists(_filePath))
                File.WriteAllText(_filePath, "Id,Nombre,Apellido,Email,Telefono\n");
        }

        // ── Helpers ─────────────────────────────────────────────────────────────

        private List<Paciente> LeerTodos()
        {
            var lista = new List<Paciente>();

            foreach (var linea in File.ReadAllLines(_filePath).Skip(1))
            {
                if (string.IsNullOrWhiteSpace(linea)) continue;
                var p = linea.Split(',');
                if (p.Length < 5) continue;

                lista.Add(new Paciente
                {
                    Id       = int.Parse(p[0]),
                    Nombre   = p[1],
                    Apellido = p[2],
                    Email    = p[3],
                    Telefono = p[4]
                });
            }

            return lista;
        }

        // ── Port ────────────────────────────────────────────────────────────────

        public List<Paciente> ObtenerTodos() => LeerTodos();

        public Paciente? ObtenerPorId(int id) =>
            LeerTodos().FirstOrDefault(p => p.Id == id);

        public void Agregar(Paciente paciente)
        {
            var lista = LeerTodos();
            paciente.Id = lista.Count > 0 ? lista.Max(p => p.Id) + 1 : 1;
            lista.Add(paciente);
            EscribirTodos(lista);
        }

        public void Eliminar(int id)
        {
            var lista = LeerTodos();
            var p = lista.FirstOrDefault(x => x.Id == id);
            if (p == null) return;
            lista.Remove(p);
            EscribirTodos(lista);
        }

        public void Actualizar(Paciente paciente)
        {
            var lista = LeerTodos();
            var idx = lista.FindIndex(p => p.Id == paciente.Id);
            if (idx == -1) return;
            lista[idx] = paciente;
            EscribirTodos(lista);
        }

        private void EscribirTodos(List<Paciente> pacientes)
        {
            var lineas = new List<string> { "Id,Nombre,Apellido,Email,Telefono" };
            foreach (var p in pacientes)
            {
                lineas.Add($"{p.Id},{Limpiar(p.Nombre)},{Limpiar(p.Apellido)},{Limpiar(p.Email)},{Limpiar(p.Telefono)}");
            }
            File.WriteAllLines(_filePath, lineas);
        }

        private static string Limpiar(string texto) => (texto ?? string.Empty).Replace(",", ";");
    }
}
