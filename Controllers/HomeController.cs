
using System.Diagnostics;
using Diseño_avanzado.Models;
using Microsoft.AspNetCore.Mvc;
using Diseño_avanzado.Services;

namespace Diseño_avanzado.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Método privado para validar autenticación
        private bool UsuarioAutenticado()
        {
            return HttpContext.Session.GetString("UsuarioId") != null;
        }

        // Redirige a la calculadora si está autenticado, si no al login
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UsuarioId") != null)
                return RedirectToAction("Operar");
            return RedirectToAction("Login");
        }


        // GET: /Home/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Home/Login
        [HttpPost]
        public IActionResult Login(Usuario model)
        {
            // Simulación de autenticación simple (reemplazar por lógica real)
            if (model.Username == "Equipo" && model.Password == "secreto")
            {
                HttpContext.Session.SetString("UsuarioId", "usuario123");
                return RedirectToAction("Operar");
            }
            ViewBag.Error = "Usuario o contraseña incorrectos";
            return View(model);
        }

        // GET: /Home/Operar
        [HttpGet]
        public IActionResult Operar()
        {
            if (!UsuarioAutenticado())
                return RedirectToAction("Login");
            return View(new OperarViewModel());
        }

        // POST: /Home/Operar
        [HttpPost]
        public IActionResult Operar(OperarViewModel model)
        {
            if (!UsuarioAutenticado())
                return RedirectToAction("Login");
            if (string.IsNullOrEmpty(model.TipoOperacion) || model.Operando1 == null || model.Operando2 == null)
            {
                model.Mensaje = "Debe ingresar todos los datos";
                return View(model);
            }
            var operandos = new List<double> { model.Operando1.Value, model.Operando2.Value };
            IOperacion operacion = model.TipoOperacion switch
            {
                "Suma" => new Suma(operandos),
                "Resta" => new Resta(operandos),
                "Multiplicacion" => new Multiplicacion(operandos),
                "Division" => new Division(operandos),
                _ => null
            };
            if (operacion == null)
            {
                model.Mensaje = "Operación no válida";
                return View(model);
            }
            try
            {
                model.Resultado = operacion.Ejecutar();
                // Guardar en historial (LiteDB)
                var usuarioId = HttpContext.Session.GetString("UsuarioId") ?? "usuario123";
                var registro = new OperacionRegistro
                {
                    Id = Guid.NewGuid(),
                    TipoOperacion = operacion.TipoOperacion,
                    Operandos = operandos,
                    Resultado = model.Resultado.Value,
                    Fecha = DateTime.Now,
                    UsuarioId = usuarioId
                };
                var repo = new Services.RegistroOperacionesLiteDB("operaciones.db");
                repo.RegistrarOperacion(registro);
            }
            catch (Exception ex)
            {
                model.Mensaje = "Error al calcular: " + ex.Message;
            }
            return View(model);
        }

        // GET: /Home/Historial
        [HttpGet]
        public IActionResult Historial()
        {
            if (!UsuarioAutenticado())
                return RedirectToAction("Login");
            var usuarioId = HttpContext.Session.GetString("UsuarioId") ?? "usuario123";
            var repo = new Services.RegistroOperacionesLiteDB("operaciones.db");
            var historial = repo.ObtenerHistorial(usuarioId);
            return View(historial);
        }

        // Elimina la acción Privacy y redirige a la calculadora si alguien intenta acceder
        public IActionResult Privacy()
        {
            return RedirectToAction("Operar");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
