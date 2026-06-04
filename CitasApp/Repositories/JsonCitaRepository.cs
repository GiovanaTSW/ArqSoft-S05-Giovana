using CitasApp.Interfaces;
using CitasApp.Models;
using System.Text.Json;

namespace CitasApp.Repositories
{
    public class JsonCitaRepository : ICitaRepository
    {
        private readonly string _path;
        private readonly JsonSerializerOptions _options = new() { WriteIndented = true };

        public JsonCitaRepository(IWebHostEnvironment env)
        {
            _path = Path.Combine(env.ContentRootPath, "data", "citas.json");
        }

        public List<Cita> ObtenerTodos()
        {
            if (!File.Exists(_path)) return new();
            var json = File.ReadAllText(_path);
            var citasJson = JsonSerializer.Deserialize<List<CitaJson>>(json, _options) ?? new();
            return citasJson.Select(c => new Cita
            {
                Id = c.Id,
                PacienteId = c.PacienteId,
                MedicoId = c.MedicoId,
                Fecha = DateOnly.Parse(c.Fecha),
                Hora = TimeOnly.Parse(c.Hora),
                Motivo = c.Motivo,
                Estado = c.Estado
            }).ToList();
        }

        public List<Cita> ObtenerPorPaciente(int pacienteId) =>
            ObtenerTodos().Where(c => c.PacienteId == pacienteId).ToList();

        public void Agregar(Cita cita)
        {
            var lista = ObtenerTodos();
            cita.Id = lista.Count > 0 ? lista.Max(c => c.Id) + 1 : 1;
            lista.Add(cita);
            GuardarTodos(lista);
        }

        public void Eliminar(int id)
        {
            var lista = ObtenerTodos();
            var cita = lista.FirstOrDefault(c => c.Id == id);
            if (cita == null) return;

            lista.Remove(cita);
            GuardarTodos(lista);
        }

        public void Actualizar(Cita cita)
        {
            var lista = ObtenerTodos();
            var index = lista.FindIndex(c => c.Id == cita.Id);
            if (index == -1) return;

            lista[index] = cita;
            GuardarTodos(lista);
        }

        private void GuardarTodos(List<Cita> lista)
        {
            var citasJson = lista.Select(c => new CitaJson
            {
                Id = c.Id,
                PacienteId = c.PacienteId,
                MedicoId = c.MedicoId,
                Fecha = c.Fecha.ToString("yyyy-MM-dd"),
                Hora = c.Hora.ToString("HH:mm"),
                Motivo = c.Motivo,
                Estado = c.Estado
            }).ToList();
            File.WriteAllText(_path, JsonSerializer.Serialize(citasJson, _options));
        }
    }
}