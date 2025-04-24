using HorizonCruises.Application.DTOs;
using HorizonCruises.Application;
using HorizonCruises.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HorizonCruises.Web.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IServiceCliente _serviceUsuario;
        private readonly IServiceRol _serviceRol;

        public UsuarioController(IServiceCliente serviceUsuario, IServiceRol serviceRol)
        {
            _serviceUsuario = serviceUsuario;
            _serviceRol = serviceRol;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var collection = await _serviceUsuario.ListAsync();
            return View(collection);
        }

        [HttpGet]
        public async Task<IActionResult> Login(string id, string password)
        {
            var @object = await _serviceUsuario.LoginAsync(id, password);
            if (@object == null)
            {
                ViewBag.Message = "Error en Login o Password";
                return View("Login");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private List<string> ObtenerListaPaises()
        {
            return new List<string>
         {
        "Afganistán", "Albania", "Alemania", "Andorra", "Angola", "Anguila", "Antigua y Barbuda", "Antártida",
        "Arabia Saudí", "Argelia", "Argentina", "Armenia", "Aruba", "Australia", "Austria", "Azerbaiyán",
        "Bahamas", "Bangladés", "Barbados", "Baréin", "Belice", "Benín", "Bermudas", "Bielorrusia", "Bolivia",
        "Bosnia y Herzegovina", "Botsuana", "Brasil", "Brunéi", "Bulgaria", "Burkina Faso", "Burundi", "Bután",
        "Bélgica", "Cabo Verde", "Camboya", "Camerún", "Canadá", "Caribe neerlandés", "Catar", "Chad", "Chequia",
        "Chile", "China", "Chipre", "Ciudad del Vaticano", "Colombia", "Comoras", "Congo", "Corea del Norte",
        "Corea del Sur", "Costa Rica", "Croacia", "Cuba", "Curazao", "Côte d’Ivoire", "Dinamarca", "Dominica",
        "Ecuador", "Egipto", "El Salvador", "Emiratos Árabes Unidos", "Eritrea", "Eslovaquia", "Eslovenia",
        "España", "Estados Unidos", "Estonia", "Esuatini", "Etiopía", "Filipinas", "Finlandia", "Fiyi", "Francia",
        "Gabón", "Gambia", "Georgia", "Ghana", "Gibraltar", "Granada", "Grecia", "Groenlandia", "Guadalupe",
        "Guam", "Guatemala", "Guayana Francesa", "Guernesey", "Guinea", "Guinea Ecuatorial", "Guinea-Bisáu",
        "Guyana", "Haití", "Honduras", "Hungría", "India", "Indonesia", "Irak", "Irlanda", "Irán", "Isla Bouvet",
        "Isla Norfolk", "Isla de Man", "Isla de Navidad", "Islandia", "Islas Aland", "Islas Caimán", "Islas Cocos",
        "Islas Cook", "Islas Feroe", "Islas Georgia del Sur y Sandwich del Sur", "Islas Heard y McDonald",
        "Islas Malvinas", "Islas Marianas del Norte", "Islas Marshall", "Islas Pitcairn", "Islas Salomón",
        "Islas Turcas y Caicos", "Islas Vírgenes Británicas", "Islas Vírgenes de EE. UU.",
        "Islas menores alejadas de EE. UU.", "Israel", "Italia", "Jamaica", "Japón", "Jersey", "Jordania",
        "Kazajistán", "Kenia", "Kirguistán", "Kiribati", "Kuwait", "Laos", "Lesoto", "Letonia", "Liberia", "Libia",
        "Liechtenstein", "Lituania", "Luxemburgo", "Líbano", "Macedonia del Norte", "Madagascar", "Malasia",
        "Malaui", "Maldivas", "Mali", "Malta", "Marruecos", "Martinica", "Mauricio", "Mauritania", "Mayotte",
        "Micronesia", "Moldavia", "Mongolia", "Montenegro", "Montserrat", "Mozambique", "Myanmar (Birmania)",
        "México", "Mónaco", "Namibia", "Nauru", "Nepal", "Nicaragua", "Nigeria", "Niue", "Noruega",
        "Nueva Caledonia", "Nueva Zelanda", "Níger", "Omán", "Pakistán", "Palaos", "Panamá",
        "Papúa Nueva Guinea", "Paraguay", "Países Bajos", "Perú", "Polinesia Francesa", "Polonia", "Portugal",
        "Puerto Rico", "RAE de Hong Kong (China)", "RAE de Macao (China)", "Reino Unido",
        "República Centroafricana", "República Democrática del Congo", "República Dominicana", "Reunión",
        "Ruanda", "Rumanía", "Rusia", "Samoa", "Samoa Americana", "San Bartolomé", "San Cristóbal y Nieves",
        "San Marino", "San Martín", "San Pedro y Miquelón", "San Vicente y las Granadinas", "Santa Elena",
        "Santa Lucía", "Santo Tomé y Príncipe", "Senegal", "Serbia", "Seychelles", "Sierra Leona", "Singapur",
        "Sint Maarten", "Siria", "Somalia", "Sri Lanka", "Sudáfrica", "Sudán", "Sudán del Sur", "Suecia",
        "Suiza", "Surinam", "Svalbard y Jan Mayen", "Sáhara Occidental", "Tailandia", "Taiwán", "Tanzania",
        "Tayikistán", "Territorio Británico del Océano Índico", "Territorios Australes Franceses",
        "Territorios Palestinos", "Timor-Leste", "Togo", "Tokelau", "Tonga", "Trinidad y Tobago",
        "Turkmenistán", "Turquía", "Tuvalu", "Túnez", "Ucrania", "Uganda", "Uruguay", "Uzbekistán", "Vanuatu",
        "Venezuela", "Vietnam", "Wallis y Futuna", "Yemen", "Yibuti", "Zambia", "Zimbabue"
         };
        }


        // GET: UsuarioController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Paises = ObtenerListaPaises();
            return View();
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClienteDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            await _serviceUsuario.AddAsync(dto);

            TempData["Mensaje"] = "Usuario creado exitosamente. Ahora podés iniciar sesión.";
            return RedirectToAction("Index", "Login");
        }

        public async Task<IActionResult> Details(int id)
        {
            var @object = await _serviceUsuario.FindByIdAsync(id);
            return View(@object);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var @object = await _serviceUsuario.FindByIdAsync(id);
            return View(@object);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClienteDTO dto)
        {
            await _serviceUsuario.UpdateAsync(id, dto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var @object = await _serviceUsuario.FindByIdAsync(id);
            return View(@object);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            await _serviceUsuario.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
