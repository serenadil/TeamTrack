using TeamTrack.Domain.Entity;

namespace TeamTrack.Api.Util
{
    public class RichiestaRegistazione
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public Ruolo Ruolo { get; set; }
        public string Nome { get; set; }
    }
}
