using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using TeamTrack.Application.Servicies;
using TeamTrack.Api.Util;
using TeamTrack.Domain.Entity;

namespace TeamTrack.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProgettoController : ControllerBase
    {
        private readonly ServiziProgetto _serviziProgetto;

        public ProgettoController(ServiziProgetto serviziProgetto)
        {
            _serviziProgetto = serviziProgetto;
        }

        /// <summary>
        /// Crea un nuovo progetto.
        /// </summary>
        [HttpPost("CreaProgetto")]
        public IActionResult CreaProgetto([FromBody] RichiestaProgetto request)

        {
            var progetto = _serviziProgetto.creaProgetto(request.Nome, request.Password, request.DataInizioProgetto, request.DataFineProgetto);
            if (progetto == null)
                return BadRequest("Creazione fallita");
            return Ok(new { Message = "Creazione avvenuta con successo!"}, ProgettoId = progetto.id);
        }

        /// <summary>
        /// Elimina un progetto esistente.
        /// </summary>
        [HttpDelete("EliminaProgetto/{idProgetto}")]
        public IActionResult EliminaProgetto(int idProgetto)

        {
            bool eliminato = _serviziProgetto.eliminaProgetto(idProgetto);
            if (!eliminato)
                return BadRequest("Impossibile eliminare il progetto");
            return Ok(new { Message = "Eliminazione del progetto avvenuta con successo!" });
        }

        /// <summary>
        /// Aggiunge un utente ad un progetto esistente.
        /// </summary>
        [HttpPost("AggiungiUtente")]
        public IActionResult AggiungiUtente([FromBody] RichiestaUtente request)

        {
            bool aggiunto = _serviziProgetto.aggiungiUtente(request.utente, request.codiceAccesso, request.password);
            if (!aggiunto)
                return BadRequest("Impossibile aggiumgere l'utente al progetto");
            return Ok(new { Message = "Aggiunta dell'utente al progetto avvenuta con successo!" });

        }

        /// <summary>
        /// Aggiorna la data di fine di un progetto esistente.
        /// </summary>
        [HttpPut("AggiornaDataFine/{idProgetto}")]
        public IactionResult AggiornaDataFine(int idProgetto, [FromBody] DateTime nuovaDataFine)

        {
            try
            {
                _serviziProgetto.AggiornaDataFineProgetto(idProgetto, nuovaDataFine);
                return Ok(new { Message = "Data di fine progetto aggiornata con successo!" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }




    }
}
