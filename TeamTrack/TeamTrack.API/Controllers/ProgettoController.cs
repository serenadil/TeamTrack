using Microsoft.AspNetCore.Mvc;
using TeamTrack.Dominio;
using TeamTrack.Servizi.Servizi;

namespace TeamTrack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgettiController : ControllerBase
    {
        private readonly ServiziProgetto _serviziProgetto;

        public ProgettiController(ServiziProgetto serviziProgetto)
        {
            _serviziProgetto = serviziProgetto;
        }

        [HttpPost("CreaProgetto")]
        public IActionResult CreaProgetto(string nome, string password, DateTime dataInizioProgetto, DateTime dataFineProgetto, int adminId)
        { 
                var progettoId = _serviziProgetto.creaProgetto(nome, password, dataInizioProgetto, dataFineProgetto, adminId);

            if (progettoId == 0)
                return BadRequest("Creazione del progetto fallita");

            return Ok(new { Message = "Creazione del progetto avvenuta con successo!", Id = progettoId });
        }

        [HttpPut("{id}")]
        public IActionResult AggiornaDataFineProgetto(int id, DateTime nuovaDataFine)
        {
            try
            {
                _serviziProgetto.AggiornaDataFineProgetto(id, nuovaDataFine);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Progetto> GetProgetto(int id)
        {
            var progetto = _serviziProgetto.GetProgetto(id);
            if (progetto == null)
            {
                return NotFound();
            }
            return Ok(progetto);
        }

        [HttpDelete("{id}")]
        public IActionResult EliminaProgetto(int id)
        {
            try
            {
                _serviziProgetto.eliminaProgetto(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
