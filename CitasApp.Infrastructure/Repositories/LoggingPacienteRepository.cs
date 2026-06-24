using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Infrastructure.Repositories
{
    public class LoggingPacienteRepository : IPacienteRepository
    {
        private readonly IPacienteRepository _inner;

        public LoggingPacienteRepository(IPacienteRepository inner)
        {
            _inner = inner;
        }

        public List<Paciente> ObtenerTodos()
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ObtenerTodos — inicio");
            var resultado = _inner.ObtenerTodos();
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ObtenerTodos — {resultado.Count} registros");
            return resultado;
        }

        public Paciente? ObtenerPorId(int id)
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ObtenerPorId({id}) — inicio");
            var resultado = _inner.ObtenerPorId(id);
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ObtenerPorId({id}) — {(resultado != null ? "encontrado" : "no encontrado")}");
            return resultado;
        }

        public void Agregar(Paciente paciente)
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Agregar — inicio");
            _inner.Agregar(paciente);
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Agregar — OK (Id={paciente.Id})");
        }

        public void Eliminar(int id)
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Eliminar({id}) — inicio");
            _inner.Eliminar(id);
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Eliminar({id}) — OK");
        }

        public void Actualizar(Paciente paciente)
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Actualizar(Id={paciente.Id}) — inicio");
            _inner.Actualizar(paciente);
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Actualizar(Id={paciente.Id}) — OK");
        }
    }
}