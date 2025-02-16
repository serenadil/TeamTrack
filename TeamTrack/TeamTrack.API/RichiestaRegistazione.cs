using TeamTrack.Dominio;

namespace TeamTrack.API
{
    public class RichiestaRegistazione
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public Ruolo Ruolo { get; set; }
        public string Nome { get; set; }
    }
}
