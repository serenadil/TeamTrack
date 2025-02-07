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
    public class Utente
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
        public string Email { get; set; }

        /// <summary>
        /// Password dell'utente.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Ruolo dell'utente (Amministratore o Utente normale).
        /// </summary>
        public Ruolo Ruolo { get; set; }

        /// <summary>
        /// Collezione di progetti associati all'utente.
        /// </summary>
        public ICollection<Progetto> Progetti { get; set; }

        /// <summary>
        /// Collezione di attività associate all'utente.
        /// </summary>
        public ICollection<TaskProgetto> Attivita { get; set; }

        /// <summary>
        /// Costruttore che inizializza un nuovo utente con validazione di email e password.
        /// </summary>
        public Utente(string email, string password, Ruolo ruolo, string nome)
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
            Nome = nome;
        }


        /// <summary>
        /// Aggiunge un progetto all'utente se non è già presente.
        /// </summary>
        public void AggiungiProgetto(Progetto progetto)
        {
            if (progetto == null)
            {
                throw new ArgumentNullException(nameof(progetto), "Il progetto non può essere nullo.");
            }

            if (Progetti.Contains(progetto))
            {
                throw new InvalidOperationException("Il progetto è già associato a questo utente.");
            }

            Progetti.Add(progetto);
        }

        /// <summary>
        /// Rimuove un progetto dall'utente se è presente.
        /// </summary>
        public void RimuoviProgetto(Progetto progetto)
        {
            if (progetto == null)
            {
                throw new ArgumentNullException(nameof(progetto), "Il progetto non può essere nullo.");
            }

            if (!Progetti.Contains(progetto))
            {
                throw new InvalidOperationException("Il progetto non è associato a questo utente.");
            }

            Progetti.Remove(progetto);
        }

        /// <summary>
        /// Aggiunge un'attività all'utente se non è già presente.
        /// </summary>
        public void AggiungiTask(TaskProgetto task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task), "L'attività non può essere nulla.");
            }

            if (Attivita.Contains(task))
            {
                throw new InvalidOperationException("L'attività è già associata a questo utente.");
            }

            Attivita.Add(task);
        }

        /// <summary>
        /// Rimuove un'attività dall'utente se è presente.
        /// </summary>
        public void RimuoviTask(TaskProgetto task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task), "L'attività non può essere nulla.");
            }

            if (!Attivita.Contains(task))
            {
                throw new InvalidOperationException("L'attività non è associata a questo utente.");
            }

            Attivita.Remove(task);
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