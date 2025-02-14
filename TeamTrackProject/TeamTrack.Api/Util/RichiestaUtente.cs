using TeamTrack.Domain.Entity;

namespace TeamTrack.Api.Util
{
    public class RichiestaUtente
    {
        public RichiestaUtente utente { get; set; }
        public string codiceAccesso { get; set; }
        public string password { get; set }
    }
}
