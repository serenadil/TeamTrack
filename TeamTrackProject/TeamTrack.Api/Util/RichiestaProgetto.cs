using TeamTrack.Domain.Entity;

namespace TeamTrack.Api.Util
{
    public class RichiestaProgetto
    {
        public string Nome { get; set; }
        public string Password { get; set; }
        public DateTime DataInizioProgetto { get; set; }
        public DateTime DataFineProgetto { get; set; }

    }
}