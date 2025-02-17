using System.Xml.Linq;
using TeamTrack.Dominio;
using TeamTrack.Servizi.Repository;

namespace TeamTrack.Servizi.Servizi
{
    /// <summary>
    /// Servizio per la gestione delle attività (Task) all'interno di un progetto.
    /// Fornisce metodi per la creazione, eliminazione e aggiornamento delle attività.
    /// </summary>
    public class ServiziTaskProgetto
    {
        private readonly RepositoryTaskProgetto _repositoryTaskProgetto;
        private readonly RepositoryProgetto _repositoryProgetto;  // Usa la repository per il progetto
        private readonly ServiziUtente _serviziUtente;

        public ServiziTaskProgetto(RepositoryTaskProgetto repositoryTaskProgetto, RepositoryProgetto repositoryProgetto, ServiziUtente serviziUtente)
        {
            _repositoryTaskProgetto = repositoryTaskProgetto;
            _repositoryProgetto = repositoryProgetto;  // Usa la repository per ottenere il progetto
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

            // Ottieni il progetto tramite il RepositoryProgetto
            Progetto progetto = _repositoryProgetto.GetById(int.Parse(id)) ?? throw new ArgumentException("Nessun progetto trovato");

            var taskProgetto = new TaskProgetto(nome, descrizione, progetto, prioritàTask, dataInizioTask, dataFineTask, statoTask);
            progetto.Tasks.Add(taskProgetto);

            _repositoryTaskProgetto.Aggiungi(taskProgetto);
            _repositoryProgetto.Aggiorna(progetto);  // Aggiorna il progetto tramite la repository

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
            _repositoryProgetto.Aggiorna(progetto);  // Aggiorna il progetto tramite la repository
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

        /// <summary>
        /// Ottieni tutte le task di un progetto specifico.
        /// </summary>
        public IEnumerable<TaskProgetto> GetTasksByProgetto(string progettoId)
        {
            var progetto = _repositoryProgetto.GetById(int.Parse(progettoId));
            if (progetto == null)
            {
                throw new ArgumentException("Progetto non trovato.");
            }
            return _repositoryTaskProgetto.GetByIdProgetto(progetto.Id);
        }

        /// <summary>
        /// Ottieni una task specifica tramite il suo ID.
        /// </summary>
        public TaskProgetto GetTaskById(int taskId)
        {
            var task = _repositoryTaskProgetto.GetById(taskId);
            if (task == null)
            {
                throw new ArgumentException("Task non trovata.");
            }
            return task;
        }

        /// <summary>
        /// Restituisce tutti gli utenti associati a una task.
        /// </summary>
        public IEnumerable<Utente> GetUtentiDaTask(int taskId)
        {
            var task = _repositoryTaskProgetto.GetById(taskId) ?? throw new ArgumentException("Task non trovata.");
            return task.Utenti;
        }
    }
}
