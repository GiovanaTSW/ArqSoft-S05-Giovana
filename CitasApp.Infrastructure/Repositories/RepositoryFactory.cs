using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using Microsoft.AspNetCore.Hosting;

namespace CitasApp.Infrastructure.Repositories
{
    public static class RepositoryFactory
    {
        public static IPacienteRepository CrearPacienteRepository(
            string entorno, IWebHostEnvironment env)
        {
            var path = Path.Combine(env.ContentRootPath, "data", "pacientes.json");
            return entorno switch
            {
                "Production" => new MemoriaPacienteRepository(),
                _ => new JsonPacienteRepository(path)
            };
        }

        public static IMedicoRepository CrearMedicoRepository(
            string entorno, IWebHostEnvironment env)
        {
            var path = Path.Combine(env.ContentRootPath, "data", "medicos.json");
            return entorno switch
            {
                "Production" => new JsonMedicoRepository(path),
                _ => new JsonMedicoRepository(path)
            };
        }

        public static ICitaRepository CrearCitaRepository(
            string entorno, IWebHostEnvironment env)
        {
            var path = Path.Combine(env.ContentRootPath, "data", "citas.json");
            return entorno switch
            {
                "Production" => new JsonCitaRepository(path),
                _ => new JsonCitaRepository(path)
            };
        }
    }
}