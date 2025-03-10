using Microsoft.AspNetCore.Mvc;

namespace TeamTrackProject.Controllers
{
    public class ProgettoController : Controller
    {
        [HttpGet("progetto/dettagli/{id}")]
        public IActionResult Dettagli(int id)
        {
            
            var progetto = new { Id = id, Nome = $"Progetto {id}", Descrizione = "Descrizione del progetto..." };

            if (progetto == null)
            {
                return NotFound(); 
            }

            return View(progetto);
        }
    }
}
