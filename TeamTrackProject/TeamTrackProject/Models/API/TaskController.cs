using Microsoft.AspNetCore.Mvc;
using TeamTrackProject.Models.Dominio;
using TeamTrackProject.Models.Servizi;

namespace TeamTrackProject.Models.API
{/// <summary>
 /// Controller per la gestione delle task in un progetto.
 /// Fornisce endpoint per creare, eliminare, aggiornare e visualizzare le task all'interno di un progetto.
 /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TaskProgettoController : ControllerBase
    {
        private readonly ServiziTaskProgetto _serviziTaskProgetto;
        private readonly ServiziProgetto _serviziProgetto;
        private readonly ServiziUtente _serviziUtente;

        /// <summary>
        /// Costruttore del controller, inizializza i servizi necessari.
        /// </summary>
        /// <param name="serviziTaskProgetto">Servizio per la gestione delle task.</param>
        /// <param name="serviziProgetto">Servizio per la gestione dei progetti.</param>
        /// <param name="serviziUtente">Servizio per la gestione degli utenti.</param>
        public TaskProgettoController(ServiziTaskProgetto serviziTaskProgetto, ServiziProgetto serviziProgetto, ServiziUtente serviziUtente)
        {
            _serviziTaskProgetto = serviziTaskProgetto;
            _serviziProgetto = serviziProgetto;
            _serviziUtente = serviziUtente;
        }

        /// <summary>
        /// Crea una nuova task per un progetto.
        /// Solo l'amministratore del progetto può creare task.
        /// </summary>
        /// <param name="progettoId">ID del progetto.</param>
        /// <param name="nome">Nome della task.</param>
        /// <param name="descrizione">Descrizione della task.</param>
        /// <param name="prioritàTask">Priorità della task.</param>
        /// <param name="dataInizioTask">Data di inizio della task.</param>
        /// <param name="dataFineTask">Data di fine della task.</param>
        /// <param name="statoTask">Stato della task.</param>
        /// <param name="userId">ID dell'utente che sta creando la task.</param>
        /// <returns>Ok se la task è stata creata, altrimenti BadRequest con il messaggio di errore.</returns>
        [HttpPost("{progettoId}")]
        public ActionResult<TaskProgetto> CreaTask(int progettoId, [FromForm] string nome, [FromForm] string descrizione, [FromForm] Priorità prioritàTask, [FromForm] DateTime dataInizioTask, [FromForm] DateTime dataFineTask, [FromForm] Stato statoTask, int userId)
        {
            try
            {
                var task = _serviziTaskProgetto.CreaTaskProgetto(progettoId, nome, descrizione, prioritàTask, dataInizioTask, dataFineTask, statoTask, userId);
                return Ok(task);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Elimina una task da un progetto.
        /// Solo l'amministratore del progetto può eliminare task.
        /// </summary>
        /// <param name="taskId">ID della task da eliminare.</param>
        /// <param name="userId">ID dell'utente che sta tentando di eliminare la task.</param>
        /// <returns>Una risposta NoContent se la task è stata eliminata, altrimenti NotFound o BadRequest con il messaggio di errore.</returns>
        [HttpDelete("{taskId}")]
        public ActionResult EliminaTask(int taskId, int userId)
        {
            var task = _serviziTaskProgetto.GetTaskById(taskId);
            if (task == null)
                return Unauthorized("Solo l'amministratore del progetto può eliminare task.");

            try
            {
                bool isDeleted = _serviziTaskProgetto.EliminaTaskProgetto(taskId);
                return isDeleted ? NoContent() : NotFound("Task non trovata.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Aggiunge un utente a una task esistente.
        /// Solo l'amministratore del progetto può aggiungere utenti a una task.
        /// </summary>
        /// <param name="taskId">ID della task.</param>
        /// <param name="adminId">ID dell'amministratore che sta aggiungendo l'utente.</param>
        /// <param name="userId">ID dell'utente da aggiungere alla task.</param>
        /// <returns>Una risposta NoContent se l'utente è stato aggiunto, altrimenti BadRequest con un messaggio di errore.</returns>
        [HttpPost("{taskId}/utente/{userId}")]
        public ActionResult AggiungiUtenteATask(int taskId, int adminId, int userId)
        {
            var task = _serviziTaskProgetto.GetTaskById(taskId);
            if (task == null)
                return Unauthorized("Solo l'amministratore del progetto può aggiungere utenti a una task.");

            try
            {
                bool isAdded = _serviziTaskProgetto.AggiungiUtenteATask(taskId, userId);
                return isAdded ? NoContent() : BadRequest("L'utente è già associato alla task.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Rimuove un utente da una task esistente.
        /// Solo l'amministratore del progetto può rimuovere utenti da una task.
        /// </summary>
        /// <param name="taskId">ID della task.</param>
        /// <param name="userId">ID dell'utente da rimuovere dalla task.</param>
        /// <param name="adminId">ID dell'amministratore che sta rimuovendo l'utente.</param>
        /// <returns>Una risposta NoContent se l'utente è stato rimosso, altrimenti BadRequest con un messaggio di errore.</returns>
        [HttpDelete("{taskId}/utente/{userId}")]
        public ActionResult RimuoviUtenteDaTask(int taskId, int userId, int adminId)
        {
            var task = _serviziTaskProgetto.GetTaskById(taskId);
            if (task == null)
                return Unauthorized("Solo l'amministratore del progetto può rimuovere utenti da una task.");

            try
            {
                bool isRemoved = _serviziTaskProgetto.RimuoviUtenteDaTask(taskId, userId);
                return isRemoved ? NoContent() : BadRequest("Utente non trovato nella task.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Aggiorna la descrizione di una task.
        /// Solo l'amministratore del progetto può aggiornare la descrizione di una task.
        /// </summary>
        /// <param name="taskId">ID della task.</param>
        /// <param name="nuovaDescrizione">Nuova descrizione della task.</param>
        /// <param name="userId">ID dell'utente che sta aggiornando la descrizione.</param>
        /// <returns>Una risposta NoContent se la descrizione è stata aggiornata, altrimenti BadRequest con un messaggio di errore.</returns>
        [HttpPut("{taskId}/descrizione")]
        public ActionResult AggiornaDescrizione(int taskId, string nuovaDescrizione, int userId)
        {
            var task = _serviziTaskProgetto.GetTaskById(taskId);
            if (task == null)
                return Unauthorized("Solo l'amministratore del progetto può modificare la descrizione della task.");

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
        /// Aggiorna la priorità di una task.
        /// Solo l'amministratore del progetto può aggiornare la priorità della task.
        /// </summary>
        /// <param name="taskId">ID della task.</param>
        /// <param name="stato">Nuovo stato della task.</param>
        /// <param name="userId">ID dell'utente che sta aggiornando la priorità.</param>
        /// <returns>Una risposta NoContent se la priorità è stata aggiornata, altrimenti BadRequest con un messaggio di errore.</returns>
        [HttpPut("{taskId}/priorita")]
        public ActionResult AggiornaPriorita(int taskId, Stato stato, int userId)
        {
            var task = _serviziTaskProgetto.GetTaskById(taskId);
            if (task == null)
                return Unauthorized("Solo l'amministratore del progetto può modificare la priorità della task.");

            try
            {
                _serviziTaskProgetto.AggiornaStatoTask(taskId, stato);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Aggiorna le date di inizio e fine di una task.
        /// Solo l'amministratore del progetto può aggiornare le date della task.
        /// </summary>
        /// <param name="taskId">ID della task.</param>
        /// <param name="dataInizio">Nuova data di inizio.</param>
        /// <param name="dataFine">Nuova data di fine.</param>
        /// <param name="userId">ID dell'utente che sta aggiornando le date.</param>
        /// <returns>Una risposta NoContent se le date sono state aggiornate, altrimenti BadRequest con un messaggio di errore.</returns>
        [HttpPut("{taskId}/date")]
        public ActionResult AggiornaDate(int taskId, DateTime dataInizio, DateTime dataFine, int userId)
        {
            var task = _serviziTaskProgetto.GetTaskById(taskId);
            if (task == null)
                return Unauthorized("Solo l'amministratore del progetto può modificare le date della task.");

            try
            {
                _serviziTaskProgetto.AggiornaDateTask(taskId, dataInizio, dataFine);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Ottiene tutte le task di un progetto specifico.
        /// Solo i partecipanti del progetto possono visualizzare le task.
        /// </summary>
        /// <param name="progettoId">ID del progetto.</param>
        /// <param name="userId">ID dell'utente che sta richiedendo le task.</param>
        /// <returns>Ok con la lista delle task, o BadRequest con il messaggio di errore.</returns>
        [HttpGet("progetto/{progettoId}")]
        public ActionResult<IEnumerable<TaskProgetto>> GetTasksByProgetto(int progettoId, int userId)
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
        /// Ottiene tutte le task di un utente specifico.
        /// </summary>
        /// <param name="userId">ID dell'utente che sta richiedendo le task.</param>
        /// <returns>Ok con la lista delle task.</returns>
        [HttpGet("utente/{id}")]
        public IActionResult GetTaskUtente(int id)
        {
            var tasks = _serviziTaskProgetto.GetTaskUtente(id);

            return Ok(tasks);
        }

    }
}