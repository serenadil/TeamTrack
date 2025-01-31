using TeamTrack.Domain.Entity;
using TeamTrack.Infrastructure;

namespace TeamTrack.Application.Repositories
{
    /// <summary>
    /// La classe UserRepository gestisce l'interazione con il contesto del database per le entità User.
    /// </summary>
    public class UserRepository
    {
        private readonly TeamTrackDbContext _context;

        /// <summary>
        /// Inizializza una nuova istanza di UserRepository con il contesto del database.
        /// </summary>
        /// <param name="context">Il contesto del database.</param>
        public UserRepository(TeamTrackDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Ottiene tutti gli utenti dal database.
        /// </summary>
        /// <returns>Una collezione di tutti gli utenti nel database.</returns>
        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        /// <summary>
        /// Ottiene un singolo utente tramite l'ID.
        /// </summary>
        /// <param name="id">L'ID dell'utente da recuperare.</param>
        /// <returns>L'utente corrispondente all'ID, o null se non trovato.</returns>
        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        /// <summary>
        /// Aggiunge un nuovo utente al contesto del database e lo salva.
        /// </summary>
        /// <param name="user">L'utente da aggiungere.</param>
        /// <returns>L'utente appena aggiunto.</returns>
        public User Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        /// <summary>
        /// Aggiorna un utente esistente nel contesto del database.
        /// </summary>
        /// <param name="user">L'utente da aggiornare.</param>
        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        /// <summary>
        /// Elimina un utente dal database tramite l'ID.
        /// </summary>
        /// <param name="userId">L'ID dell'utente da eliminare.</param>
        public void Delete(int userId)
        {
            var user = _context.Users.Find(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
    }
}
