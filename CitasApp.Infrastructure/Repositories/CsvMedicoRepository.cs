// CitasApp.Infrastructure/Repositories/CsvMedicoRepository.cs
// Adapter de salida — implementa IMedicoRepository leyendo un archivo CSV

using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Infrastructure.Repositories
{
    public class CsvMedicoRepository : IMedicoRepository
    {
        private readonly string _filePath;

        public CsvMedicoRepository(string filePath)
        {
            _filePath = filePath;

            if (!File.Exists(_filePath))
                File.WriteAllText(_filePath, "Id,Nombre,Apellido,Especialidad,NumeroLicencia\n");
        }

        // ── Helpers ─────────────────────────────────────────────────────────────

        private List<Medico> LeerTodos()
        {
            var lista = new List<Medico>();

            foreach (var linea in File.ReadAllLines(_filePath).Skip(1))
            {
                if (string.IsNullOrWhiteSpace(linea)) continue;
                var p = linea.Split(',');
                if (p.Length < 5) continue;

                lista.Add(new Medico
                {
                    Id             = int.Parse(p[0]),
                    Nombre         = p[1],
                    Apellido       = p[2],
                    Especialidad   = p[3],
                    NumeroLicencia = p[4]
                });
            }

            return lista;
        }

        // ── Port ────────────────────────────────────────────────────────────────

        public List<Medico> ObtenerTodos() => LeerTodos();

        public Medico? ObtenerPorId(int id) =>
            LeerTodos().FirstOrDefault(m => m.Id == id);

        public void Agregar(Medico medico)
        {
            var lista = LeerTodos();
            medico.Id = lista.Count > 0 ? lista.Max(m => m.Id) + 1 : 1;
            lista.Add(medico);
            EscribirTodos(lista);
        }

        public void Eliminar(int id)
        {
            var lista = LeerTodos();
            var m = lista.FirstOrDefault(x => x.Id == id);
            if (m == null) return;
            lista.Remove(m);
            EscribirTodos(lista);
        }

        public void Actualizar(Medico medico)
        {
            var lista = LeerTodos();
            var idx = lista.FindIndex(m => m.Id == medico.Id);
            if (idx == -1) return;
            lista[idx] = medico;
            EscribirTodos(lista);
        }

        private void EscribirTodos(List<Medico> medicos)
        {
            var lineas = new List<string> { "Id,Nombre,Apellido,Especialidad,NumeroLicencia" };
            foreach (var m in medicos)
            {
                lineas.Add($"{m.Id},{Limpiar(m.Nombre)},{Limpiar(m.Apellido)},{Limpiar(m.Especialidad)},{Limpiar(m.NumeroLicencia)}");
            }
            File.WriteAllLines(_filePath, lineas);
        }

        private static string Limpiar(string texto) => (texto ?? string.Empty).Replace(",", ";");
    }
}
