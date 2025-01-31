using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTrack.Domain.Entity.TeamTrack.Domain.Entity;

namespace TeamTrack.Domain.Entity
{
    /// <summary>
    /// Rappresenta un utente con informazioni come nome, email e ruolo.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Identificativo univoco dell'utente.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome dell'utente.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Indirizzo email dell'utente.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Ruolo dell'utente (Admin o User).
        /// </summary>
        public Role Role { get; set; }

        /// <summary>
        /// Collezione di progetti associati all'utente.
        /// </summary>
        public ICollection<Project> Projects { get; set; }

        /// <summary>
        /// Collezione di attività associate all'utente.
        /// </summary>
        public ICollection<ProjectTask> Tasks { get; set; }
    }

    /// <summary>
    /// Enumerazione per rappresentare i ruoli degli utenti.
    /// </summary>
    public enum Role
    {
        /// <summary>
        /// Ruolo di amministratore.
        /// </summary>
        Admin,

        /// <summary>
        /// Ruolo di utente normale.
        /// </summary>
        User
    }
}
