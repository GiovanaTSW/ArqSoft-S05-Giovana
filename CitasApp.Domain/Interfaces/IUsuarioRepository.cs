using CitasApp.Domain.Models;

namespace CitasApp.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        List<Usuario> ObtenerTodos();
        Usuario? ObtenerPorId(int id);
        Usuario? ObtenerPorEmail(string email);
        void Agregar(Usuario usuario);
        void Eliminar(int id);
    }
}
