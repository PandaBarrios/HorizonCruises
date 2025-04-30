using HorizonCruises.Application.DTOs;
using HorizonCruises.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HorizonCruises.web.Controllers
{
    public class ComplementoController : Controller
    {
        private readonly IServiceComplemento _serviceComplemento;
        private readonly ILogger<LoginController> _logger;

        public ComplementoController(IServiceComplemento serviceComplemento)
        {
            _serviceComplemento = serviceComplemento;
        }

        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> IndexAdmin()
        {
            var collection = await _serviceComplemento.ListAsync();
            return View(collection);
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create(ComplementoDTO dto)
        {
            try
            {
                if (dto == null)
                {
                    ModelState.AddModelError("", "Los datos del complemento son inválidos.");
                }

                if (string.IsNullOrWhiteSpace(dto.Nombre))
                {
                    ModelState.AddModelError("Nombre", "El nombre del complemento es obligatorio.");
                }

                if (!string.IsNullOrEmpty(dto.Descripcion) && dto.Descripcion.Length > 500)
                {
                    ModelState.AddModelError("Descripcion", "La descripción no puede exceder los 500 caracteres.");
                }

                if (dto.Precio <= 0)
                {
                    ModelState.AddModelError("Precio", "El precio debe ser mayor que cero.");
                }

                if (!ModelState.IsValid)
                {
                    TempData["ErrorMessages"] = string.Join("|", ModelState.Values
                                                        .SelectMany(x => x.Errors)
                                                        .Select(x => x.ErrorMessage));
                    return View(dto);
                }

                await _serviceComplemento.AddAsync(dto);

                TempData["SuccessMessage"] = "Complemento creado con éxito";

                return RedirectToAction("IndexAdmin");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error al procesar la solicitud. Intenta nuevamente.");
                return View(dto);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int id)
        {
            var complemento = await _serviceComplemento.FindByIdAsync(id);

            if (complemento == null)
            {
                return NotFound();  
            }

            return View(complemento);  
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(ComplementoDTO dto)
        {
            try
            {
                var complementoExistente = await _serviceComplemento.FindByIdAsync(dto.Id);

                if (complementoExistente == null)
                {
                    return NotFound();
                }

                if (string.IsNullOrWhiteSpace(dto.Nombre))
                {
                    ModelState.AddModelError("Nombre", "El nombre del complemento es obligatorio.");
                }

                if (!string.IsNullOrEmpty(dto.Descripcion) && dto.Descripcion.Length > 500)
                {
                    ModelState.AddModelError("Descripcion", "La descripción no puede exceder los 500 caracteres.");
                }

                if (dto.Precio <= 0)
                {
                    ModelState.AddModelError("Precio", "El precio debe ser mayor que cero.");
                }

                if (!ModelState.IsValid)
                {
                    TempData["ErrorMessages"] = string.Join("|", ModelState.Values
                                                  .SelectMany(x => x.Errors)
                                                  .Select(x => x.ErrorMessage));
                    return View(dto);
                }

                await _serviceComplemento.UpdateAsync(dto.Id, dto);

                TempData["SuccessMessage"] = "Complemento actualizado con éxito";

                return RedirectToAction("IndexAdmin");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessages"] = "Ocurrió un error al procesar la solicitud. Intenta nuevamente.";
                return View(dto);
            }
        }
    }
}
