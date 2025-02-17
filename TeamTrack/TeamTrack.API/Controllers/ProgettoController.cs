using Microsoft.AspNetCore.Mvc;
using TeamTrack.Dominio;
using TeamTrack.Servizi.Servizi;
namespace TeamTrack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgettiController : ControllerBase
    {
        private readonly ServiziProgetto _serviziProgetto;
        private readonly ServiziUtente _serviziUtente; 

        public ProgettiController(ServiziProgetto serviziProgetto, ServiziUtente serviziUtente)
        {
            _serviziProgetto = serviziProgetto;
            _serviziUtente = serviziUtente;
        }

        [HttpPost("CreaProgetto")]
        public IActionResult CreaProgetto(string nome, string password, DateTime dataInizioProgetto, DateTime dataFineProgetto, int adminId)
        {
            try
            {
                var progetto = _serviziProgetto.creaProgetto(nome, password, dataInizioProgetto, dataFineProgetto, adminId);

                return Ok(new { Message = "Creazione del progetto avvenuta con successo!", ProgettoId = progetto.Id });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id}")]
        public IActionResult AggiornaDataFineProgetto(int id, DateTime nuovaDataFine, int userId)
        {
            try
            {
                Progetto progetto = _serviziProgetto.GetProgetto(id);
                if (userId != progetto.AdminId)
                {
                    return Unauthorized("Solo l'amministratore del progetto può aggiornare le date.");
                }

                _serviziProgetto.AggiornaDataFineProgetto(id, nuovaDataFine);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Progetto> GetProgetto(int id, int userId)
        {
            Progetto progetto = _serviziProgetto.GetProgetto(id);
            Utente utente = _serviziUtente.GetUtente(userId);

            if (progetto == null || !utente.Progetti.Contains(progetto)  )
            {
                return Unauthorized("L'utente non è autorizzato a visualizzare questo progetto.");
            }

            return Ok(progetto);
        }

   
        [HttpDelete("{id}")]
        public IActionResult EliminaProgetto(int id, int adminId)
        {
            try
            {
                Progetto progetto = _serviziProgetto.GetProgetto(id);
                if (progetto == null || adminId != progetto.AdminId)
                {
                    return Unauthorized("Solo l'amministratore del progetto può eliminarlo.");
                }

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

