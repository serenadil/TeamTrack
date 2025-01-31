
using TeamTrack.Domain.Entity;
using TeamTrack.Infrastructure;

namespace TeamTrack.Application.Repositories
{
    /// <summary>
    /// La classe ProjectTaskRepository gestisce l'interazione con il contesto del database per le entità ProjectTask.
    /// </summary>
    public class ProjectTaskRepository
    {
        private readonly TeamTrackDbContext _context;

        /// <summary>
        /// Inizializza una nuova istanza di ProjectTaskRepository con il contesto del database.
        /// </summary>
        /// <param name="context">Il contesto del database.</param>
        public ProjectTaskRepository(TeamTrackDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Ottiene tutte le attività associate a un progetto specifico identificato dall'ID del progetto.
        /// </summary>
        /// <param name="projectId">L'ID del progetto per cui ottenere le attività.</param>
        /// <returns>Una collezione di attività che appartengono al progetto specificato.</returns>
        public IEnumerable<ProjectTask> GetByProjectId(int projectId)
        {
            return _context.Tasks.Where(t => t.ProjectId == projectId).ToList();
        }

        /// <summary>
        /// Aggiunge una nuova attività al contesto del database e la salva.
        /// </summary>
        /// <param name="task">L'attività da aggiungere al database.</param>
        /// <returns>L'attività appena aggiunta.</returns>
        public ProjectTask Add(ProjectTask task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
            return task;
        }

        /// <summary>
        /// Aggiorna un'attività esistente nel contesto del database.
        /// </summary>
        /// <param name="task">L'attività da aggiornare.</param>
        public void Update(ProjectTask task)
        {
            _context.Tasks.Update(task);
            _context.SaveChanges();
        }

        /// <summary>
        /// Elimina un'attività dal database in base al suo ID.
        /// </summary>
        /// <param name="taskId">L'ID dell'attività da eliminare.</param>
        public void Delete(int taskId)
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


