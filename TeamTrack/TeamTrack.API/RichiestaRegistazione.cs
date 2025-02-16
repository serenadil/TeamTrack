using System.ComponentModel.DataAnnotations;
using TeamTrack.Dominio;

namespace TeamTrack.API
{
    public class RichiestaRegistazione
    {
        [Required(ErrorMessage = "L'Email è obbligatoria.")]
        [EmailAddress(ErrorMessage = "Formato Email non valido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La Password è obbligatoria.")]
        [MinLength(6, ErrorMessage = "La Password deve essere di almeno 6 caratteri.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Il Ruolo è obbligatorio.")]
        public Ruolo Ruolo { get; set; }

        [Required(ErrorMessage = "Il Nome è obbligatorio.")]
        public string Nome { get; set; }
    }
}
