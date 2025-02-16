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

        // POST: api/progetti
        [HttpPost]
        public IActionResult CreaProgetto([FromBody] RichiestaCreazioneProgetto request)
        {
            try
            {
                var progetto = _serviziProgetto.creaProgetto(
                    request.Name,
                    request.Password,
                    request.DataInizioProgetto,
                    request.DataFineProgetto,
                    request.AdminId);

                return CreatedAtAction("GetProgetto", new { id = progetto.Id }, progetto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/progetti/5
        [HttpPut("{id}")]
        public IActionResult AggiornaDataFineProgetto(int id, [FromBody] DateTime nuovaDataFine)
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

        // GET: api/progetti/5
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

        // DELETE: api/progetti/5
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
