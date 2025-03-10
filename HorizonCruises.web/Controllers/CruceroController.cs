using HorizonCruises.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HorizonCruises.web.Controllers
{
    public class CruceroController : Controller
    {
        private readonly IServiceCrucero _serviceCrucero;

        public CruceroController(IServiceCrucero serviceCrucero)
        {
            _serviceCrucero = serviceCrucero;
        }

        [HttpGet]
        public async Task<IActionResult> CruceroIndex()
        {
            var collection = await _serviceCrucero.ListAsync();
            return View(collection);
        }

        public async Task<ActionResult> CruceroDetails(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("CruceroIndex");
                }
                var @object = await _serviceCrucero.FindByIdAsync(id.Value);
                if (@object == null)
                {
                    throw new Exception("Crucero no existente");

                }

                return View(@object);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerImagen(int id)
        {
            var crucero = await _serviceCrucero.FindByIdAsync(id);

            if (crucero == null || crucero.Imagen == null)
            {
                return NotFound(); 
            }

            return File(crucero.Imagen, "image/jpeg");
        }

    }
}
