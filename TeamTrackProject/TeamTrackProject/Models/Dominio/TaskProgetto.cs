using System.Text.Json.Serialization;

namespace TeamTrackProject.Models.Dominio
{/// <summary>
 /// Rappresenta un'attività di un progetto con informazioni come nome, descrizione, priorità e stato.
 /// </summary>
    public class TaskProgetto
    {
        /// <summary>
        /// Identificativo univoco dell'attività.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome dell'attività.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Descrizione dell'attività.
        /// </summary>
        public string Descrizione { get; set; }

        /// <summary>
        /// Priorità dell'attività (Alta, Media, Bassa).
        /// </summary>
        public Priorità? PrioritàTask { get; set; }

        /// <summary>
        /// Data di inizio dell'attività.
        /// </summary>
        public DateTime DataInizioTask { get; set; }

        /// <summary>
        /// Data di fine dell'attività.
        /// </summary>
        public DateTime DataFineTask { get; set; }

        /// <summary>
        /// Collezione di utenti associati all'attività.
        /// </summary>
        public ICollection<Utente> Utenti { get; set; }

        /// <summary>
        /// Identificativo del progetto associato all'attività.
        /// </summary>
        public int IdProgetto { get; set; }

        /// <summary>
        /// Progetto associato all'attività.
        /// </summary>
        [JsonIgnore]
        public Progetto Progetto { get; set; }

        /// <summary>
        /// Stato dell'attività (DaFare, InCorso, Completata).
        /// </summary>
        public Stato? StatoTask { get; set; }

        public TaskProgetto(string nome, string descrizione, Progetto progetto, Priorità prioritàTask, DateTime dataInizioTask, DateTime dataFineTask, Stato? statoTask)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentException("Il nome della task non può essere null o vuoto.", nameof(nome));
            }
            if (string.IsNullOrWhiteSpace(descrizione))
            {
                throw new ArgumentException("La descrizione della task non può essere null o vuoto.", nameof(descrizione));
            }
            if (dataInizioTask < DateTime.Now)
            {
                throw new ArgumentException("La data di inizio della task non può essere nel passato.", nameof(dataInizioTask));
            }
            if (dataFineTask <= dataInizioTask)
            {
                throw new ArgumentException("La data di fine della task deve essere successiva alla data di inizio.", nameof(dataFineTask));
            }

            if (statoTask == null)
            {
                throw new ArgumentNullException("La priorità della task non può essere null.", nameof(statoTask));
            }
            Nome = nome;
            Descrizione = descrizione;
            Progetto = progetto;
            IdProgetto = progetto.Id;
            DataInizioTask = dataInizioTask;
            DataFineTask = dataFineTask;
            PrioritàTask = prioritàTask;
            StatoTask = statoTask;
            Utenti = new List<Utente>();
        }

        public TaskProgetto()
        {
            Utenti = new List<Utente>();
        }
    }

    /// <summary>
    /// Enumerazione per rappresentare le priorità delle attività.
    /// </summary>
    public enum Priorità
    {
        /// <summary>
        /// Priorità alta.
        /// </summary>
        Alta,

        /// <summary>
        /// Priorità media.
        /// </summary>
        Media,

        /// <summary>
        /// Priorità bassa.
        /// </summary>
        Bassa
    }

    /// <summary>
    /// Enumerazione per rappresentare gli stati di avanzamento delle attività.
    /// </summary>
    public enum Stato
    {
        /// <summary>
        /// L'attività è nella lista delle cose da fare.
        /// </summary>
        DaFare,

        /// <summary>
        /// L'attività è in corso.
        /// </summary>
        InCorso,

        /// <summary>
        /// L'attività è completata.
        /// </summary>
        Completata
    }

}
