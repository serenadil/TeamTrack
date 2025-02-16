
using TeamTrack.Dominio;
using TeamTrack.Infrastrutture;
using Microsoft.EntityFrameworkCore;

namespace TeamTrack.Servizi.Repository
{
    /// <summary>
    /// La classe ProjectRepository gestisce l'interazione con il contesto del database per le entità Project.
    /// </summary>
    public class RepositoryProgetto
    {
        private readonly TeamTrackDbContext _context;

        /// <summary>
        /// Inizializza una nuova istanza di ProjectRepository con il contesto del database.
        /// </summary>
        /// <param name="context">Il contesto del database.</param>
        public RepositoryProgetto(TeamTrackDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Ottiene tutti i progetti dal database, includendo le relative attività e gli utenti.
        /// </summary>
        /// <returns>Una collezione di tutti i progetti con le attività e gli utenti associati.</returns>
        public IEnumerable<Progetto> GetAll()
        {
            return _context.Progetti
                           .Include(p => p.Tasks)
                           .Include(p => p.Users)
                           .ToList();
        }

        /// <summary>
        /// Ottiene un progetto specifico tramite il suo ID, includendo le attività e gli utenti associati.
        /// </summary>
        /// <param name="id">L'ID del progetto da recuperare.</param>
        /// <returns>Il progetto corrispondente all'ID, o null se non trovato.</returns>
        public Progetto GetById(int id)
        {
            return _context.Progetti
                           .Include(p => p.Tasks)
                           .Include(p => p.Users)
                           .FirstOrDefault(p => p.Id == id);
        }


        /// <summary>
        /// Ottiene un progetto specifico tramite il suo codice di accesso, includendo le attività e gli utenti associati.
        /// </summary>
        /// <param name="codiceAccesso">Il codice di accesso del progetto da recuperare.</param>
        /// <returns>Il progetto corrispondente al codice di accesso, o null se non trovato.</returns>
        public Progetto GetByCodiceAccesso(string codiceAccesso)
        {
            return _context.Progetti
                           .Include(p => p.Tasks)
                           .Include(p => p.Users)
                           .FirstOrDefault(p => p.CodiceAccesso == codiceAccesso);
        }

        /// <summary>
        /// Aggiunge un nuovo progetto al contesto del database e lo salva.
        /// </summary>
        /// <param name="project">Il progetto da aggiungere.</param>
        /// <returns>Il progetto appena aggiunto.</returns>
        public Progetto Aggiungi(Progetto project)
        {
            _context.Progetti.Add(project);
            _context.SaveChanges();
            return project;
        }

        /// <summary>
        /// Aggiorna un progetto esistente nel contesto del database.
        /// </summary>
        /// <param name="project">Il progetto da aggiornare.</param>
        public void Aggiorna(Progetto project)
        {
            _context.Progetti.Update(project);
            _context.SaveChanges();
        }

        /// <summary>
        /// Elimina un progetto dal database in base al suo ID.
        /// </summary>
        /// <param name="id">L'ID del progetto da eliminare.</param>
        public void Elimina(int id)
        {
            var project = _context.Progetti.Find(id);
            if (project != null)
            {
                _context.Progetti.Remove(project);
                _context.SaveChanges();
            }
        }
    }
}
