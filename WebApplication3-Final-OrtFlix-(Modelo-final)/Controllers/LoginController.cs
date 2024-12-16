using System.Drawing.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using WebApplication3_Final_OrtFlix__Modelo_final_.Context;
using WebApplication3_Final_OrtFlix__Modelo_final_.Models;
using WebApplication3_Final_OrtFlix__Modelo_final_.Services;

namespace WebApplication3_Final_OrtFlix__Modelo_final_.Controllers
{

    public class LoginController : Controller
    {

        private readonly IUsuarioService _usuarioService;
        private readonly OrtflixDatabaseContext _context;

        public bool AllowRefresh { get; private set; }

        public LoginController(IUsuarioService usuarioService, OrtflixDatabaseContext context)
        {
            _usuarioService = usuarioService;
            _context = context;
        }

        [HttpPost]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registro(Usuario usuario)
        {
            usuario.Password = usuario.Password;

            Usuario usuarioCreado = await _usuarioService.SaveUsuario(usuario);

            if (usuarioCreado.Id > 0)
            {
                return RedirectToAction("IniciarSesion", "Login");
            }

            ViewData["Mensaje"] = "No se pudo crear el usuario";
            return View();
        }

        [HttpPost]
        public IActionResult IniciarSesion()
        {
            return View();
        }

        public async Task<IActionResult> IniciarSesion(string email, string password)
        {
            
            Usuario usuarioEncontrado = await _usuarioService.GetUsuario(email, password);

            if (usuarioEncontrado == null)
            {
                ViewData["Mensaje"] = "Usuario no encontrado";
                return View();
            }

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, usuarioEncontrado.NombreCompleto),
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties();
            {
                AllowRefresh = true;
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
                );

            return RedirectToAction("Index", "Home");
        }
    }
}
