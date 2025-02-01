using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;



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
        public string Nome { get; set; }

        /// <summary>
        /// Indirizzo email dell'utente.
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// Password dell'utente.
        /// </summary>
        public string Password { get; private set; }

        /// <summary>
        /// Ruolo dell'utente (Amministratore o Utente normale).
        /// </summary>
        public Ruolo Ruolo { get; set; }

        /// <summary>
        /// Collezione di progetti associati all'utente.
        /// </summary>
        public ICollection<Project> Progetti { get; set; }

        /// <summary>
        /// Collezione di attività associate all'utente.
        /// </summary>
        public ICollection<ProjectTask> Attivita { get; set; }

        /// <summary>
        /// Costruttore che inizializza un nuovo utente con validazione di email e password.
        /// </summary>
        public User(string email, string password, Ruolo ruolo)
        {
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                throw new ArgumentException("L'email fornita non è valida.");
            }

            if (password.Length < 8)
            {
                throw new ArgumentException("La password deve contenere almeno 8 caratteri.");
            }

            Email = email;
            Password = password;
            Ruolo = ruolo;
        }
    }

    /// <summary>
    /// Enumerazione per rappresentare i ruoli degli utenti.
    /// </summary>
    public enum Ruolo
    {
        /// <summary>
        /// Ruolo di amministratore.
        /// </summary>
        Admin,

        /// <summary>
        /// Ruolo di utente normale.
        /// </summary>
        Partecipante
    }
}