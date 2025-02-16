namespace TeamTrack.API
{
    public class RichiestaCreazioneProgetto
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime DataInizioProgetto { get; set; }
        public DateTime DataFineProgetto { get; set; }
        public int AdminId { get; set; }
    }

}