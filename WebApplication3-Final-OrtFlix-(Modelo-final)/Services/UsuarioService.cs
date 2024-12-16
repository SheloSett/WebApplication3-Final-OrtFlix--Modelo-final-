using Microsoft.EntityFrameworkCore;
using WebApplication3_Final_OrtFlix__Modelo_final_.Context;
using WebApplication3_Final_OrtFlix__Modelo_final_.Models;

namespace WebApplication3_Final_OrtFlix__Modelo_final_.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly OrtflixDatabaseContext _context;

        public UsuarioService(OrtflixDatabaseContext context)
        {
            _context = context;
        }

        public async Task<Usuario> GetUsuario(string email, string password)
        {
            Usuario usuario = await _context.Usuarios.Where(u => u.Email == email && u.Password == password).FirstOrDefaultAsync();
            return usuario;
        }

        public async Task<Usuario> SaveUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }
    }
}
