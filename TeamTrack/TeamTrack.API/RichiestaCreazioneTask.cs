using TeamTrack.Dominio;

namespace TeamTrack.API
{
    public class RichiestaCreazioneTask
    {

        public string IdProgetto { get; set; }
        public string Nome { get; set; }
        public string Descrizione { get; set; }
        public Priorità PrioritàTask { get; set; }
        public DateTime DataInizioTask { get; set; }
        public DateTime DataFineTask { get; set; }
        public Stato StatoTask { get; set; }
        public int AdminId { get; set; }
    }
    


}
