using Microsoft.AspNetCore.Mvc;
using TeamTrackProject.Models.Servizi;

namespace TeamTrackProject.Controllers
{
    public class ProgettoController : Controller
    {
        private readonly ServiziProgetto _serviziProgetto;

        public ProgettoController(ServiziProgetto serviziProgetto)
        {
            _serviziProgetto = serviziProgetto;
        }

        [HttpGet("progetto/dettagli/{id}")]
        public IActionResult Dettagli(int id)
        {
            var progetto = _serviziProgetto.GetProgetto(id);
            if (progetto == null)
            {
                return NotFound();
            }

            return View(progetto);
        }
    }
}

