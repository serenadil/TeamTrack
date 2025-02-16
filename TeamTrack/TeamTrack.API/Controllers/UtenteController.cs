using Microsoft.AspNetCore.Mvc;
using TeamTrack.API;
using TeamTrack.Dominio;
using TeamTrack.Servizi.Servizi;

namespace TeamTrack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UtenteController : ControllerBase
    {
        private readonly ServiziUtente _serviziUtente;

        public UtenteController(ServiziUtente serviziUtente)
        {
            _serviziUtente = serviziUtente;
        }

        /// <summary>
        /// Registra un nuovo utente.
        /// </summary>
        [HttpPost("Registrazione")]
        public IActionResult Registrazione([FromBody] RichiestaRegistazione request)
        {
            if (!Enum.IsDefined(typeof(Ruolo), request.Ruolo))
            {
                return BadRequest("Ruolo non valido");
            }

            // Eseguiamo la registrazione dell'utente e otteniamo l'ID
            var userId = _serviziUtente.Registrazione(request.Email, request.Password, request.Ruolo, request.Nome);

            if (userId == 0)
                return BadRequest("Registrazione fallita");

            return Ok(new { Message = "Registrazione avvenuta con successo!", UserId = userId });
        }

        /// <summary>
        /// Effettua il login di un utente.
        /// </summary>
        [HttpPost("Login")]
        public IActionResult Login([FromBody] RichiestaLogin request)
        {
            var userId = _serviziUtente.Autenticazione(request.Email, request.Password);

            if (userId == null)
                return Unauthorized("Credenziali non valide");

            // Restituiamo l'ID dell'utente autenticato
            return Ok(new { Message = "Login avvenuto con successo!", UserId = userId });
        }
    }
}
