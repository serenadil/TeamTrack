using TeamTrack.Dominio;

namespace TeamTrack.API
{
    public class RichiestaProgetto
    {
        public string Nome { get; set; }
        public string Password { get; set; }
        public DateTime DataInizioProgetto { get; set; }
        public DateTime DataFineProgetto { get; set; }

    }
}