using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTrack.Domain.Entity.TeamTrack.Domain.Entity;
namespace TeamTrack.Domain.Entity
{
    /// <summary>
    /// Rappresenta un'attività di un progetto con informazioni come nome, descrizione, priorità e stato.
    /// </summary>
    public class ProjectTask
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
        public ICollection<User> Utenti { get; set; }

        /// <summary>
        /// Identificativo dell'amministratore del progetto.
        /// </summary>
        public int AdminId { get; set; }

        /// <summary>
        /// L'amministratore del progetto.
        /// </summary>
        public User Admin { get; set; }

        /// <summary>
        /// Identificativo del progetto associato all'attività.
        /// </summary>
        public int IdProgetto { get; set; }

        /// <summary>
        /// Progetto associato all'attività.
        /// </summary>
        public Project Progetto { get; set; }

        /// <summary>
        /// Stato dell'attività (DaFare, InCorso, Completata).
        /// </summary>
        public Stato? StatoTask { get; set; }
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

    public ProjectTask(string nome, string descrizione, Priorità prioritàTask, DateTime dataInizioTask, DateTime dataFineTask, Priorità? prioritàTask, Stato? statoTask, int adminId)
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
        if (prioritàTask == null)
        {
            throw new ArgumentNullException("La priorità della task non può essere null.", nameof(prioritàTask));
        }
        if (statoTask == null)
        {
            throw new ArgumentNullException("La priorità della task non può essere null.", nameof(statoTask));
        }
        Nome = nome;
        Descrizione = descrizione;
        DataInizioTask = dataInizioTask;
        DataFineTask = dataFineTask;
        PrioritàTask = prioritàTask;
        StatoTask = statoTask;
        AdminId = adminId;
        Users = new List<User>();
    }

