using WebApplication3_Final_OrtFlix__Modelo_final_.Models;

namespace WebApplication3_Final_OrtFlix__Modelo_final_.Services
{
    public interface IUsuarioService
    {
        Task<Usuario> GetUsuario(string email, string password);
        Task<Usuario> SaveUsuario(Usuario usuario);

    }
}
