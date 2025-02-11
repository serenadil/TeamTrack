using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTrack.Application.Repositories;
using TeamTrack.Application.Services;
using TeamTrack.Domain.Entity;

namespace TeamTrack.Application.Servicies
{
    /// <summary>
    /// Servizio per la gestione delle attività (Task) all'interno di un progetto.
    /// Fornisce metodi per la creazione, eliminazione e aggiornamento delle attività.
    /// </summary>
    public class ServiziTaskProgetto
    {
        private readonly RepositoryTaskProgetto _repositoryTaskProgetto;
        private readonly ServiziProgetto _serviziProgetto;
        private readonly ServiziUtente _serviziUtente;

        public ServiziTaskProgetto(RepositoryTaskProgetto repositoryTaskProgetto, ServiziProgetto serviziProgetto, ServiziUtente serviziUtente)
        {
            _repositoryTaskProgetto = repositoryTaskProgetto;
            _serviziProgetto = serviziProgetto;
            _serviziUtente = serviziUtente;
        }

        /// <summary>
        /// Crea una nuova attività per un progetto.
        /// </summary>
        public TaskProgetto CreaTaskProgetto(string id, string nome, string descrizione, Priorità prioritàTask, DateTime dataInizioTask, DateTime dataFineTask, Stato? statoTask, int adminId)
        {
            var admin = _serviziUtente.GetUtente(adminId);
            if (admin == null || admin.Ruolo != Ruolo.Admin)
                throw new ArgumentException("L'utente deve essere un Admin.", nameof(adminId));

            Progetto progetto = _serviziProgetto.GetProgetto(id) ?? throw new ArgumentException("Nessun progetto trovato");

            var taskProgetto = new TaskProgetto(nome, descrizione, progetto, prioritàTask, dataInizioTask, dataFineTask, statoTask);
            progetto.Tasks.Add(taskProgetto);

            _repositoryTaskProgetto.Aggiungi(taskProgetto);
            _serviziProgetto.AggiornaProgetto(progetto);

            return taskProgetto;
        }

        /// <summary>
        /// Elimina una task dal progetto.
        /// </summary>
        public bool EliminaTaskProgetto(int projectTaskId)
        {
            var task = _repositoryTaskProgetto.GetById(projectTaskId);
            if (task == null) return false;

            var progetto = task.Progetto;
            progetto?.Tasks.Remove(task);

            _repositoryTaskProgetto.Elimina(projectTaskId);
            return true;
        }

        /// <summary>
        /// Aggiunge un utente a una task esistente.
        /// </summary>
        public bool AggiungiUtenteATask(int taskId, int userId)
        {
            var task = _repositoryTaskProgetto.GetById(taskId) ?? throw new ArgumentException("Task non trovata.");
            var utente = _serviziUtente.GetUtente(userId) ?? throw new ArgumentException("Utente non trovato.");

            if (!task.Progetto.Users.Contains(utente))
                throw new ArgumentException("L'utente non fa parte del progetto.");

            if (!task.Utenti.Contains(utente))
            {
                task.Utenti.Add(utente);
                _repositoryTaskProgetto.Aggiorna(task);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Rimuove un utente da una task.
        /// </summary>
        public bool RimuoviUtenteDaTask(int taskId, int userId)
        {
            var task = _repositoryTaskProgetto.GetById(taskId) ?? throw new ArgumentException("Task non trovata.");
            var utente = _serviziUtente.GetUtente(userId) ?? throw new ArgumentException("Utente non trovato.");

            if (task.Utenti.Contains(utente))
            {
                task.Utenti.Remove(utente);
                _repositoryTaskProgetto.Aggiorna(task);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Aggiorna la descrizione di una task.
        /// </summary>
        public void AggiornaDescrizioneTask(int taskId, string nuovaDescrizione)
        {
            var task = _repositoryTaskProgetto.GetById(taskId) ?? throw new ArgumentException("Task non trovata.");
            task.Descrizione = nuovaDescrizione;
            _repositoryTaskProgetto.Aggiorna(task);
        }

        /// <summary>
        /// Aggiorna la data di inizio e fine di una task.
        /// </summary>
        public void AggiornaDateTask(int taskId, DateTime nuovaDataInizio, DateTime nuovaDataFine)
        {
            if (nuovaDataFine <= nuovaDataInizio)
                throw new ArgumentException("La data di fine deve essere successiva alla data di inizio.");

            var task = _repositoryTaskProgetto.GetById(taskId) ?? throw new ArgumentException("Task non trovata.");
            task.DataInizioTask = nuovaDataInizio;
            task.DataFineTask = nuovaDataFine;
            _repositoryTaskProgetto.Aggiorna(task);
        }

        /// <summary>
        /// Aggiorna lo stato di una task.
        /// </summary>
        public void AggiornaStatoTask(int taskId, Stato nuovoStato)
        {
            var task = _repositoryTaskProgetto.GetById(taskId) ?? throw new ArgumentException("Task non trovata.");
            task.StatoTask = nuovoStato;
            _repositoryTaskProgetto.Aggiorna(task);
        }
    }
}
