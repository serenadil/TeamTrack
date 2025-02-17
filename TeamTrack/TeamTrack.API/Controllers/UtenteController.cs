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
        public IActionResult Registrazione(  string email, string password, Ruolo ruolo, string nome)
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
        [HttpPost("Login")]
        public IActionResult Login(string email, string password)
        {
            var authResult = _serviziUtente.Autenticazione(email, password);

            if (!authResult.Success)
            {
                return Unauthorized(new { Message = authResult.Message });
            }

            return Ok(new
            {
                Message = authResult.Message,
                UserId = authResult.UserId
            });
        }
    }
}
