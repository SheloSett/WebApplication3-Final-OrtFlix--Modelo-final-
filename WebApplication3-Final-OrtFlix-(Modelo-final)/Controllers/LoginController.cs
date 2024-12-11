using Microsoft.AspNetCore.Mvc;
using WebApplication3_Final_OrtFlix__Modelo_final_.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace WebApplication3_Final_OrtFlix__Modelo_final_.Controllers
{
    public class LoginController : Controller
    {
        private readonly OrtflixDatabaseContext _context;
        
        public LoginController(OrtflixDatabaseContext context)
        {
            _context = context;
        }
       
        public IActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(string email, string password)
        {
            if(string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "El email y la contraseña son obligatorios");
                return View();
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

            if(usuario == null)
            {
                ModelState.AddModelError("", "El email no existe");
                return View();
            }

            if(usuario.Password != password)
            {
                ModelState.AddModelError("", "La contraseña es incorrecta");
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Name, usuario.NombreCompleto),
                new Claim("UserId", usuario.Id.ToString()),
                new Claim("EsAdmin", usuario.EsAdmin.ToString()),
                new Claim("Premium", usuario.Premium.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return RedirectToAction("Index", "Home");

        }
        
    }
}
