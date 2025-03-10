using Microsoft.AspNetCore.Mvc;
using TeamTrackProject.Models.Dominio;
using TeamTrackProject.Models.Servizi;

namespace TeamTrackProject.Models.API
{
    /// <summary>
    /// Controller per la gestione dei progetti.
    /// Fornisce operazioni per creare, aggiornare, recuperare ed eliminare progetti.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProgettiController : ControllerBase
    {
        private readonly ServiziProgetto _serviziProgetto;
        private readonly ServiziUtente _serviziUtente;

        /// <summary>
        /// Costruttore del controller per l'iniezione delle dipendenze.
        /// </summary>
        /// <param name="serviziProgetto">Servizio per la gestione dei progetti</param>
        /// <param name="serviziUtente">Servizio per la gestione degli utenti</param>
        public ProgettiController(ServiziProgetto serviziProgetto, ServiziUtente serviziUtente)
        {
            _serviziProgetto = serviziProgetto;
            _serviziUtente = serviziUtente;
        }

        /// <summary>
        /// Crea un nuovo progetto.
        /// </summary>
        /// <param name="nome">Nome del progetto</param>
        /// <param name="password">Password per accedere al progetto</param>
        /// <param name="dataInizioProgetto">Data di inizio del progetto</param>
        /// <param name="dataFineProgetto">Data di fine del progetto</param>
        /// <param name="adminId">ID dell'amministratore del progetto</param>
        /// <returns>Risultato dell'operazione con l'ID del progetto creato</returns>
        [HttpPost("CreaProgetto")]
        public IActionResult CreaProgetto([FromForm]string nome, [FromForm] string password, [FromForm] DateTime dataInizioProgetto, [FromForm] DateTime dataFineProgetto, [FromForm] int adminId)
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

        /// <summary>
        /// Aggiorna la data di fine di un progetto.
        /// </summary>
        /// <param name="id">ID del progetto</param>
        /// <param name="nuovaDataFine">Nuova data di fine</param>
        /// <param name="userId">ID dell'utente che sta tentando di aggiornare</param>
        /// <returns>NoContent se l'operazione ha successo, errore in caso contrario</returns>
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

        /// <summary>
        /// Recupera un progetto se l'utente ha i permessi per visualizzarlo.
        /// </summary>
        /// <param name="id">ID del progetto</param>
        /// <param name="userId">ID dell'utente che richiede il progetto</param>
        /// <returns>Il progetto se l'utente è autorizzato, errore in caso contrario</returns>
        [HttpGet("{id}")]
        public ActionResult<Progetto> GetProgetto(int id, int userId)
        {
            Progetto progetto = _serviziProgetto.GetProgetto(id);
            Utente utente = _serviziUtente.GetUtente(userId);

            if (progetto == null || !utente.Progetti.Contains(progetto))
            {
                return Unauthorized("L'utente non è autorizzato a visualizzare questo progetto.");
            }

            return Ok(progetto);
        }

        /// <summary>
        /// Elimina un progetto se l'utente è l'amministratore.
        /// </summary>
        /// <param name="id">ID del progetto da eliminare</param>
        /// <param name="adminId">ID dell'utente che richiede l'eliminazione</param>
        /// <returns>NoContent se l'operazione ha successo, errore in caso contrario</returns>
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

        /// <summary>
        /// Aggiunge un utente a un progetto se il codice e la password sono corretti.
        /// </summary>
        /// <param name="id">ID dell'utente</param>
        /// <param name="codice">Codice di accesso al progetto</param>
        /// <param name="password">Password del progetto</param>
        /// <returns>True se l'iscrizione ha successo, false in caso contrario</returns>
        [HttpPost("iscrizione/{id}")]
        public bool AggiungiUtente( int id, [FromForm] string codice, [FromForm] string password)
        {
            var utente = _serviziUtente.GetUtente(id);
            return _serviziProgetto.aggiungiUtente(utente, codice, password);
        }


       

    }
}