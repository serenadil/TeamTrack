using System;
using System.ComponentModel.DataAnnotations;
using TeamTrack.Dominio;
namespace TeamTrack.Web.Models
{
    public class RichiestaRegistazione
    {
        [Required(ErrorMessage = "Il nome è obbligatorio")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "L'email è obbligatoria")]
        [EmailAddress(ErrorMessage = "Formato email non valido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La password è obbligatoria")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Il ruolo è obbligatorio")]
        public Ruolo Ruolo { get; set; }

    }
}