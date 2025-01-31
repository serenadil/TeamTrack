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
        public string Name { get; set; }

        /// <summary>
        /// Descrizione dell'attività.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Priorità dell'attività.
        /// </summary>
        public Priority Priority { get; set; }

        /// <summary>
        /// Data di inizio dell'attività.
        /// </summary>
        public DateTime StartingDate { get; set; }

        /// <summary>
        /// Data di fine dell'attività.
        /// </summary>
        public DateTime EndingDate { get; set; }

        /// <summary>
        /// Collezione di utenti associati all'attività.
        /// </summary>
        public ICollection<User> Users { get; set; }

        /// <summary>
        /// Identificativo del progetto associato all'attività.
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// Progetto associato all'attività.
        /// </summary>
        public Project Project { get; set; }

        /// <summary>
        /// Stato dell'attività (ToDo, In Progress, Done).
        /// </summary>
        public Status Status { get; set; }
    }

    /// <summary>
    /// Enumerazione per rappresentare le priorità delle attività.
    /// </summary>
    public enum Priority
    {
        /// <summary>
        /// Priorità alta.
        /// </summary>
        High,

        /// <summary>
        /// Priorità media.
        /// </summary>
        Medium,

        /// <summary>
        /// Priorità bassa.
        /// </summary>
        Low
    }

    /// <summary>
    /// Enumerazione per rappresentare gli stati di avanzamento delle attività.
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// L'attività è nella lista delle cose da fare (ToDo).
        /// </summary>
        ToDo,

        /// <summary>
        /// L'attività è in corso (Loading).
        /// </summary>
        Loading,

        /// <summary>
        /// L'attività è completata (Done).
        /// </summary>
        Done
    }
}

