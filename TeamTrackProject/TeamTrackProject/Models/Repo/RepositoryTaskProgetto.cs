using Microsoft.EntityFrameworkCore;
using TeamTrackProject.Models.Dominio;
using TeamTrackProject.Models.Infrastrutture;

namespace TeamTrackProject.Models.Repo
{
    /// <summary>
    /// La classe ProjectTaskRepository gestisce l'interazione con il contesto del database per le entità ProjectTask.
    /// </summary>
    public class RepositoryTaskProgetto
    {
        private readonly TeamTrackDbContext _context;

        /// <summary>
        /// Inizializza una nuova istanza di ProjectTaskRepository con il contesto del database.
        /// </summary>
        /// <param name="context">Il contesto del database.</param>
        public RepositoryTaskProgetto(TeamTrackDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Ottiene tutte le attività associate a un progetto specifico identificato dall'ID del progetto.
        /// </summary>
        /// <param name="idProgetto">L'ID del progetto per cui ottenere le attività.</param>
        /// <returns>Una collezione di attività che appartengono al progetto specificato.</returns>
        public IEnumerable<TaskProgetto> GetByIdProgetto(int idProgetto)
        {
            return _context.Tasks.Where(t => t.IdProgetto == idProgetto).ToList();
        }


        /// <summary>
        /// Ottiene l'attivita dato un id.
        /// </summary>
        /// <param name="id">L'ID dell'attività.</param>
        /// <returns>L'attività corrispondente</returns>
        public TaskProgetto GetById(int taskId)
        {
            return _context.Tasks
                .Include(t => t.Progetto)
                .ThenInclude(p => p.Users)
                .Include(t => t.Utenti)
                .FirstOrDefault(t => t.Id == taskId);
        }

        /// <summary>
        /// Aggiunge una nuova attività al contesto del database e la salva.
        /// </summary>
        /// <param name="task">L'attività da aggiungere al database.</param>
        /// <returns>L'attività appena aggiunta.</returns>
        public TaskProgetto Aggiungi(TaskProgetto task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
            return task;
        }

        /// <summary>
        /// Aggiorna un'attività esistente nel contesto del database.
        /// </summary>
        /// <param name="task">L'attività da aggiornare.</param>
        public void Aggiorna(TaskProgetto task)
        {
            _context.Tasks.Update(task);
            _context.SaveChanges();
        }

        /// <summary>
        /// Elimina un'attività dal database in base al suo ID.
        /// </summary>
        /// <param name="taskId">L'ID dell'attività da eliminare.</param>
        public void Elimina(int taskId)
        {
            var task = _context.Tasks.Find(taskId);

            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
            }
        }
    }
}
