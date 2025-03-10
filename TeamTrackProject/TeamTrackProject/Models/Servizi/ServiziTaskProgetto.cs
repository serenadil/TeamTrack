using TeamTrackProject.Models.Dominio;
using TeamTrackProject.Models.Repo;

namespace TeamTrackProject.Models.Servizi
{ /// <summary>
  /// Servizio per la gestione delle attività (Task) all'interno di un progetto.
  /// Fornisce metodi per la creazione, eliminazione e aggiornamento delle attività.
  /// </summary>
    public class ServiziTaskProgetto
    {
        private readonly RepositoryTaskProgetto _repositoryTaskProgetto;
        private readonly RepositoryProgetto _repositoryProgetto;
        private readonly ServiziUtente _serviziUtente;

        /// <summary>
        /// Costruttore del servizio per la gestione delle task di un progetto.
        /// </summary>
        /// <param name="repositoryTaskProgetto">Repository per le operazioni sulle task del progetto.</param>
        /// <param name="repositoryProgetto">Repository per le operazioni sul progetto.</param>
        /// <param name="serviziUtente">Servizio per la gestione degli utenti.</param>
        public ServiziTaskProgetto(RepositoryTaskProgetto repositoryTaskProgetto, RepositoryProgetto repositoryProgetto, ServiziUtente serviziUtente)
        {
            _repositoryTaskProgetto = repositoryTaskProgetto;
            _repositoryProgetto = repositoryProgetto;
            _serviziUtente = serviziUtente;
        }

        /// <summary>
        /// Crea una nuova attività per un progetto.
        /// </summary>
        /// <param name="id">ID del progetto a cui aggiungere la task.</param>
        /// <param name="nome">Nome della task.</param>
        /// <param name="descrizione">Descrizione della task.</param>
        /// <param name="prioritàTask">Priorità della task.</param>
        /// <param name="dataInizioTask">Data di inizio della task.</param>
        /// <param name="dataFineTask">Data di fine della task.</param>
        /// <param name="statoTask">Stato della task.</param>
        /// <param name="adminId">ID dell'utente amministratore che sta creando la task.</param>
        /// <returns>La task appena creata.</returns>
        public TaskProgetto CreaTaskProgetto(int id, string nome, string descrizione, Priorità prioritàTask, DateTime dataInizioTask, DateTime dataFineTask, Stato? statoTask, int adminId)
        {
            var admin = _serviziUtente.GetUtente(adminId);
            if (admin == null || admin.Ruolo != Ruolo.Admin)
                throw new ArgumentException("L'utente deve essere un Admin.", nameof(adminId));

            Progetto progetto = _repositoryProgetto.GetById(id) ?? throw new ArgumentException("Nessun progetto trovato");

            var taskProgetto = new TaskProgetto(nome, descrizione, progetto, prioritàTask, dataInizioTask, dataFineTask, statoTask);
            progetto.Tasks.Add(taskProgetto);

            _repositoryTaskProgetto.Aggiungi(taskProgetto);
            _repositoryProgetto.Aggiorna(progetto);

            return taskProgetto;
        }

        /// <summary>
        /// Elimina una task dal progetto.
        /// </summary>
        /// <param name="projectTaskId">ID della task da eliminare.</param>
        /// <returns>True se la task è stata eliminata correttamente, altrimenti false.</returns>
        public bool EliminaTaskProgetto(int projectTaskId)
        {
            var task = _repositoryTaskProgetto.GetById(projectTaskId);
            if (task == null) return false;

            var progetto = task.Progetto;
            progetto?.Tasks.Remove(task);

            _repositoryTaskProgetto.Elimina(projectTaskId);
            _repositoryProgetto.Aggiorna(progetto);
            return true;
        }

        /// <summary>
        /// Aggiunge un utente a una task esistente.
        /// </summary>
        /// <param name="taskId">ID della task a cui aggiungere l'utente.</param>
        /// <param name="userId">ID dell'utente da aggiungere alla task.</param>
        /// <returns>True se l'utente è stato aggiunto correttamente, altrimenti false.</returns>
        public bool AggiungiUtenteATask(int taskId, int userId)
        {
            var task = _repositoryTaskProgetto.GetById(taskId);
            if (task == null)
                throw new ArgumentException("Task non trovata.");

            if (task.Progetto == null)
                throw new ArgumentException("Il task non è associato a nessun progetto.");

            var utente = _serviziUtente.GetUtente(userId);
            if (utente == null)
                throw new ArgumentException("Utente non trovato.");

            if (task.Progetto.Users == null)
                throw new ArgumentException("Il progetto non ha utenti associati.");

            if (!task.Progetto.Users.Contains(utente))
                throw new ArgumentException("L'utente non fa parte del progetto.");

            if (task.Utenti == null)
                task.Utenti = new List<Utente>();

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
        /// <param name="taskId">ID della task da cui rimuovere l'utente.</param>
        /// <param name="userId">ID dell'utente da rimuovere dalla task.</param>
        /// <returns>True se l'utente è stato rimosso correttamente, altrimenti false.</returns>
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
        /// <param name="taskId">ID della task da aggiornare.</param>
        /// <param name="nuovaDescrizione">Nuova descrizione da assegnare alla task.</param>
        public void AggiornaDescrizioneTask(int taskId, string nuovaDescrizione)
        {
            var task = _repositoryTaskProgetto.GetById(taskId) ?? throw new ArgumentException("Task non trovata.");
            task.Descrizione = nuovaDescrizione;
            _repositoryTaskProgetto.Aggiorna(task);
        }

        /// <summary>
        /// Aggiorna la data di inizio e fine di una task.
        /// </summary>
        /// <param name="taskId">ID della task da aggiornare.</param>
        /// <param name="nuovaDataInizio">Nuova data di inizio della task.</param>
        /// <param name="nuovaDataFine">Nuova data di fine della task.</param>
        /// <exception cref="ArgumentException">Lanciata se la data di fine è precedente alla data di inizio.</exception>
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
        /// <param name="taskId">ID della task da aggiornare.</param>
        /// <param name="nuovoStato">Nuovo stato da assegnare alla task.</param>
        public void AggiornaStatoTask(int taskId, Stato nuovoStato)
        {
            var task = _repositoryTaskProgetto.GetById(taskId) ?? throw new ArgumentException("Task non trovata.");
            task.StatoTask = nuovoStato;
            _repositoryTaskProgetto.Aggiorna(task);
        }

        /// <summary>
        /// Ottieni tutte le task di un progetto specifico.
        /// </summary>
        /// <param name="progettoId">ID del progetto di cui recuperare le task.</param>
        /// <returns>Una sequenza di task associate al progetto specificato.</returns>
        public IEnumerable<TaskProgetto> GetTasksByProgetto(int progettoId)
        {
            var progetto = _repositoryProgetto.GetById(progettoId);
            if (progetto == null)
            {
                throw new ArgumentException("Progetto non trovato.");
            }
            return _repositoryTaskProgetto.GetByIdProgetto(progetto.Id);
        }

        /// <summary>
        /// Ottieni una task specifica tramite il suo ID.
        /// </summary>
        /// <param name="taskId">ID della task da recuperare.</param>
        /// <returns>La task con l'ID specificato.</returns>
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
        /// <param name="taskId">ID della task di cui ottenere gli utenti associati.</param>
        /// <returns>Una sequenza di utenti associati alla task.</returns>
        public IEnumerable<Utente> GetUtentiDaTask(int taskId)
        {
            var task = _repositoryTaskProgetto.GetById(taskId) ?? throw new ArgumentException("Task non trovata.");
            return task.Utenti;
        }


        /// <summary>
        /// Ottiene tutte le attività in cui un utente è coinvolto.
        /// </summary>
        /// <param name="userId">ID dell'utente.</param>
        /// <returns>Lista di Task associate all'utente.</returns>
        public IEnumerable<TaskProgetto> GetTaskUtente(int userId)
        {
            return _repositoryTaskProgetto.GetTasksByUserId(userId);
        }
    }
}
