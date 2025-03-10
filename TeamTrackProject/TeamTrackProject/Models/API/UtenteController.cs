using Microsoft.AspNetCore.Mvc;
using TeamTrackProject.Models.Dominio;
using TeamTrackProject.Models.Servizi;

namespace TeamTrackProject.Models.API
{
    /// <summary>
    /// Controller per la gestione degli utenti.
    /// Permette la registrazione, il login e la gestione dei progetti associati agli utenti.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UtenteController : ControllerBase
    {
        private readonly ServiziUtente _serviziUtente;

        /// <summary>
        /// Costruttore della classe UtenteController.
        /// </summary>
        /// <param name="serviziUtente">Servizio per la gestione degli utenti.</param>
        public UtenteController(ServiziUtente serviziUtente)
        {
            _serviziUtente = serviziUtente;
        }

        /// <summary>
        /// Registra un nuovo utente.
        /// </summary>
        /// <param name="email">Email dell'utente.</param>
        /// <param name="password">Password dell'utente.</param>
        /// <param name="ruolo">Ruolo dell'utente (Admin, User, etc.).</param>
        /// <param name="nome">Nome dell'utente.</param>
        [HttpPost("Registrazione")]
        public IActionResult Registrazione([FromForm] string email, [FromForm] string password, [FromForm] Ruolo ruolo, [FromForm] string nome)
        {
            if (!Enum.IsDefined(typeof(Ruolo), ruolo))
            {
                return BadRequest("Ruolo non valido");
            }

            var userId = _serviziUtente.Registrazione(email, password, ruolo, nome);

            if (userId == 0)
                return BadRequest("Registrazione fallita");

            return Ok(new { Message = "Registrazione avvenuta con successo!", UserId = userId });
        }


        /// <summary>
        /// Effettua il login di un utente.
        /// </summary>
        /// <param name="email">Email dell'utente.</param>
        /// <param name="password">Password dell'utente.</param>
        [HttpPost("Login")]
        public IActionResult Login([FromForm] string email, [FromForm] string password)
        {
            var authResult = _serviziUtente.Autenticazione(email, password);

            if (!authResult.Success)
            {
                return Unauthorized(new { authResult.Message });
            }

            return Ok(new
            {
                authResult.Message,
                authResult.UserId
            });
        }

        /// <summary>
        /// Ottiene la lista dei progetti associati a un utente specifico.
        /// </summary>
        /// <param name="id">ID dell'utente.</param>
        [HttpGet("progetti/{id}")]
        public ICollection<Progetto> GetProgettiUtente(int id)
        {
            return _serviziUtente.getProgettiUtente(id);
        }

        /// <summary>
        /// Restituisce i dati dell'utente.
        /// </summary>
        /// <param name="id">ID dell'utente.</param>
        [HttpGet("utente/{id}")]
        public IActionResult GetUtente(int id)
        {
            var utente = _serviziUtente.GetUtente(id);

            if (utente == null)
            {
                return NotFound(); 
            }

            return Ok(utente); 
        }

    }
}