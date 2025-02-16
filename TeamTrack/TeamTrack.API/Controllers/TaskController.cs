using Microsoft.AspNetCore.Mvc;
using TeamTrack.Dominio;
using TeamTrack.Servizi.Servizi;

namespace TeamTrack.API.Controllers
{
    /// <summary>
    /// Controller per la gestione delle task in un progetto.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TaskProgettoController : ControllerBase
    {
        private readonly ServiziTaskProgetto _serviziTaskProgetto;

        public TaskProgettoController(ServiziTaskProgetto serviziTaskProgetto)
        {
            _serviziTaskProgetto = serviziTaskProgetto;
        }

        /// <summary>
        /// Crea una nuova attività all'interno di un progetto.
        /// </summary>
        [HttpPost]
        public ActionResult<TaskProgetto> CreaTask([FromBody] RichiestaCreazioneTask request)
        {
            try
            {
                var task = _serviziTaskProgetto.CreaTaskProgetto(
                    request.IdProgetto,
                    request.Nome,
                    request.Descrizione,
                    request.PrioritàTask,
                    request.DataInizioTask,
                    request.DataFineTask,
                    request.StatoTask,
                    request.AdminId
                );

                return CreatedAtAction(nameof(GetTaskById), new { taskId = task.Id }, task);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Elimina una task da un progetto.
        /// </summary>
        [HttpDelete("{taskId}")]
        public ActionResult EliminaTask(int taskId)
        {
            try
            {
                bool isDeleted = _serviziTaskProgetto.EliminaTaskProgetto(taskId);
                if (isDeleted)
                    return NoContent();  
                else
                    return NotFound("Task non trovata.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Aggiunge un utente a una task.
        /// </summary>
        [HttpPost("{taskId}/utente/{userId}")]
        public ActionResult AggiungiUtenteATask(int taskId, int userId)
        {
            try
            {
                bool isAdded = _serviziTaskProgetto.AggiungiUtenteATask(taskId, userId);
                if (isAdded)
                    return NoContent(); 
                else
                    return BadRequest("L'utente è già associato alla task.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Rimuove un utente da una task.
        /// </summary>
        [HttpDelete("{taskId}/utente/{userId}")]
        public ActionResult RimuoviUtenteDaTask(int taskId, int userId)
        {
            try
            {
                bool isRemoved = _serviziTaskProgetto.RimuoviUtenteDaTask(taskId, userId);
                if (isRemoved)
                    return NoContent();
                else
                    return BadRequest("Utente non trovato nella task.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Aggiorna la descrizione di una task.
        /// </summary>
        [HttpPut("{taskId}/descrizione")]
        public ActionResult AggiornaDescrizione(int taskId, [FromBody] string nuovaDescrizione)
        {
            try
            {
                _serviziTaskProgetto.AggiornaDescrizioneTask(taskId, nuovaDescrizione);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Aggiorna le date di inizio e fine di una task.
        /// </summary>
        [HttpPut("{taskId}/date")]
        public ActionResult AggiornaDate(int taskId, DateTime DataInizio, DateTime DataFine)
        {
            try
            {
                _serviziTaskProgetto.AggiornaDateTask(taskId, DataInizio, DataFine);
                return NoContent();  
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Aggiorna lo stato di una task.
        /// </summary>
        [HttpPut("{taskId}/stato")]
        public ActionResult AggiornaStato(int taskId, [FromBody] Stato nuovoStato)
        {
            try
            {
                _serviziTaskProgetto.AggiornaStatoTask(taskId, nuovoStato);
                return NoContent(); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Ottieni tutte le task di un progetto.
        /// </summary>
        [HttpGet("progetto/{progettoId}")]
        public ActionResult<IEnumerable<TaskProgetto>> GetTasksByProgetto(string progettoId)
        {
            try
            {
                var tasks = _serviziTaskProgetto.GetTasksByProgetto(progettoId);
                return Ok(tasks);  
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Ottieni una task specifica tramite il suo ID.
        /// </summary>
        [HttpGet("{taskId}")]
        public ActionResult<TaskProgetto> GetTaskById(int taskId)
        {
            try
            {
                var task = _serviziTaskProgetto.GetTaskById(taskId);
                return Ok(task);  
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Restituisce tutti gli utenti associati a una task.
        /// </summary>
        [HttpGet("{taskId}/utenti")]
        public ActionResult<IEnumerable<Utente>> GetUtentiDaTask(int taskId)
        {
            try
            {
                var utenti = _serviziTaskProgetto.GetUtentiDaTask(taskId);
                return Ok(utenti); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}