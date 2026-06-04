using CitasApp.Interfaces;
using CitasApp.Models;
using System.Text.Json;

namespace CitasApp.Repositories
{
    public class JsonMedicoRepository : IMedicoRepository
    {
        private readonly string _path;
        private readonly JsonSerializerOptions _options = new() { WriteIndented = true };

        public JsonMedicoRepository(IWebHostEnvironment env)
        {
            _path = Path.Combine(env.ContentRootPath, "data", "medicos.json");
        }

        public List<Medico> ObtenerTodos()
        {
            if (!File.Exists(_path)) return new();
            var json = File.ReadAllText(_path);
            return JsonSerializer.Deserialize<List<Medico>>(json, _options) ?? new();
        }

        public Medico? ObtenerPorId(int id) =>
            ObtenerTodos().FirstOrDefault(m => m.Id == id);

        public void Agregar(Medico medico)
        {
            var lista = ObtenerTodos();
            medico.Id = lista.Count > 0 ? lista.Max(m => m.Id) + 1 : 1;
            lista.Add(medico);
            File.WriteAllText(_path, JsonSerializer.Serialize(lista, _options));
        }

        public void Eliminar(int id)
        {
            var lista = ObtenerTodos();
            var medico = lista.FirstOrDefault(m => m.Id == id);
            if (medico == null) return;

            lista.Remove(medico);
            File.WriteAllText(_path, JsonSerializer.Serialize(lista, _options));
        }

        public void Actualizar(Medico medico)
        {
            var lista = ObtenerTodos();
            var index = lista.FindIndex(m => m.Id == medico.Id);
            if (index == -1) return;

            lista[index] = medico;
            File.WriteAllText(_path, JsonSerializer.Serialize(lista, _options));
        }
    }
}