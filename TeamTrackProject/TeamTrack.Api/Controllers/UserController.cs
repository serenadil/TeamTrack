using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using TeamTrack.Application.Servicies;
using TeamTrack.Api.Util;
using TeamTrack.Domain.Entity;
namespace TeamTrack.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ServiziUtente _serviziUtente;

        public UserController(ServiziUtente serviziUtente)
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
            var utente = _serviziUtente.Registrazione(request.Email, request.Password, request.Ruolo, request.Nome);

            if (utente == null)
                return BadRequest("Registrazione fallita");

            return Ok(new { Message = "Registrazione avvenuta con successo!", UserId = utente.Id });
        }

        /// <summary>
        /// Effettua il login di un utente.
        /// </summary>
        [HttpPost("Login")]
        public IActionResult Login([FromBody] RichiestaLogin request)
        {
            bool autenticato = _serviziUtente.Autenticazione(request.Email, request.Password);

            if (!autenticato)
                return Unauthorized("Credenziali non valide");

            return Ok(new { Message = "Login avvenuto con successo!" });
        }



    }
}
