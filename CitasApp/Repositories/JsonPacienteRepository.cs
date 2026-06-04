using CitasApp.Interfaces;
using CitasApp.Models;
using System.Text.Json;

namespace CitasApp.Repositories
{
    public class JsonPacienteRepository : IPacienteRepository
    {
        private readonly string _path;
        private readonly JsonSerializerOptions _options = new() { WriteIndented = true };

        public JsonPacienteRepository(IWebHostEnvironment env)
        {
            _path = Path.Combine(env.ContentRootPath, "data", "pacientes.json");
        }

        public List<Paciente> ObtenerTodos()
        {
            if (!File.Exists(_path)) return new();
            var json = File.ReadAllText(_path);
            return JsonSerializer.Deserialize<List<Paciente>>(json, _options) ?? new();
        }

        public Paciente? ObtenerPorId(int id) =>
            ObtenerTodos().FirstOrDefault(p => p.Id == id);

        public void Agregar(Paciente paciente)          // ← nuevo
        {
            var lista = ObtenerTodos();
            paciente.Id = lista.Count > 0 ? lista.Max(p => p.Id) + 1 : 1;
            lista.Add(paciente);
            File.WriteAllText(_path, JsonSerializer.Serialize(lista, _options));
        }

        public void Eliminar(int id)
        {
            var lista = ObtenerTodos();
            var paciente = lista.FirstOrDefault(p => p.Id == id);
            if (paciente == null) return;

            lista.Remove(paciente);
            File.WriteAllText(_path, JsonSerializer.Serialize(lista, _options));
        }
    }
}