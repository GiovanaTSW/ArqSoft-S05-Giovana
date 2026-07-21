using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using Microsoft.Data.Sqlite;

namespace CitasApp.Infrastructure.Repositories
{
    public class SqliteUsuarioRepository : IUsuarioRepository
    {
        private readonly string _connectionString;

        public SqliteUsuarioRepository(string dbPath)
        {
            _connectionString = $"Data Source={dbPath}";
            InicializarTabla();
        }

        public void Agregar(Usuario usuario)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO Usuarios (Nombre, Email, Password)
                VALUES ($nombre, $email, $password);";
            cmd.Parameters.AddWithValue("$nombre", usuario.Nombre);
            cmd.Parameters.AddWithValue("$email", usuario.Email);
            cmd.Parameters.AddWithValue("$password", usuario.Password);
            cmd.ExecuteNonQuery();
        }

        public void Eliminar(int id)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM Usuarios WHERE Id = $id;";
            cmd.Parameters.AddWithValue("$id", id);
            cmd.ExecuteNonQuery();
        }

        public List<Usuario> ObtenerTodos()
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Id, Nombre, Email, Password FROM Usuarios;";

            var lista = new List<Usuario>();
            using var r = cmd.ExecuteReader();
            while (r.Read()) lista.Add(LeerFila(r));
            return lista;
        }

        public Usuario? ObtenerPorId(int id)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Id, Nombre, Email, Password FROM Usuarios WHERE Id = $id;";
            cmd.Parameters.AddWithValue("$id", id);

            using var r = cmd.ExecuteReader();
            return r.Read() ? LeerFila(r) : null;
        }

        public Usuario? ObtenerPorEmail(string email)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Id, Nombre, Email, Password FROM Usuarios WHERE Email = $email;";
            cmd.Parameters.AddWithValue("$email", email);

            using var r = cmd.ExecuteReader();
            return r.Read() ? LeerFila(r) : null;
        }

        private void InicializarTabla()
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Usuarios (
                    Id       INTEGER PRIMARY KEY AUTOINCREMENT,
                    Nombre   TEXT NOT NULL,
                    Email    TEXT NOT NULL UNIQUE,
                    Password TEXT NOT NULL
                );";
            cmd.ExecuteNonQuery();
        }

        private static Usuario LeerFila(SqliteDataReader r) => new Usuario
        {
            Id       = r.GetInt32(0),
            Nombre   = r.GetString(1),
            Email    = r.GetString(2),
            Password = r.GetString(3)
        };
    }
}
