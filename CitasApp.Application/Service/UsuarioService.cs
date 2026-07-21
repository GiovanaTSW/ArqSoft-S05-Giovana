using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Application.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repo;

        public UsuarioService(IUsuarioRepository repo)
        {
            _repo = repo;
        }

        public List<Usuario> ObtenerTodos() => _repo.ObtenerTodos();

        public Usuario? ObtenerPorId(int id) => _repo.ObtenerPorId(id);

        public Usuario? ObtenerPorEmail(string email) => _repo.ObtenerPorEmail(email);

        public void Agregar(Usuario usuario) => _repo.Agregar(usuario);

        public void Eliminar(int id) => _repo.Eliminar(id);
    }
}
