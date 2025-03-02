using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTrack.Dominio;
using TeamTrack.Infrastrutture;

namespace TeamTrack.Servizi.Repository
{ /// <summary>
  /// La classe UserRepository gestisce l'interazione con il contesto del database per le entità User.
  /// </summary>
    public class RepositoryUtente
    {
        private readonly TeamTrackDbContext _context;

        /// <summary>
        /// Inizializza una nuova istanza di UserRepository con il contesto del database.
        /// </summary>
        /// <param name="context">Il contesto del database.</param>
        public RepositoryUtente(TeamTrackDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Ottiene tutti gli utenti dal database.
        /// </summary>
        /// <returns>Una collezione di tutti gli utenti nel database.</returns>
        public IEnumerable<Utente> GetAll()
        {
            return _context.Utenti.ToList();
        }

        /// <summary>
        /// Ottiene un singolo utente tramite l'ID.
        /// </summary>
        /// <param name="id">L'ID dell'utente da recuperare.</param>
        /// <returns>L'utente corrispondente all'ID, o null se non trovato.</returns>
        public Utente GetById(int id)
        {
            return _context.Utenti
                .Include(u => u.Progetti)  
                .FirstOrDefault(u => u.Id == id);
        }

        /// <summary>
        /// Ottiene un singolo utente tramite l'email.
        /// </summary>
        /// <param name="id">L'email dell'utente da recuperare.</param>
        /// <returns>L'utente corrispondente all'email, o null se non trovato.</returns>
        public Utente GetByEmail(string email)
        {
            return _context.Utenti.Include(u => u.Progetti).FirstOrDefault(u => u.Email == email);
        }


        /// <summary>
        /// Aggiunge un nuovo utente al contesto del database e lo salva.
        /// </summary>
        /// <param name="user">L'utente da aggiungere.</param>
        /// <returns>L'utente appena aggiunto.</returns>
        public Utente Aggiungi(Utente user)
        {
            _context.Utenti.Add(user);
            _context.SaveChanges();
            return user;
        }

        /// <summary>
        /// Aggiorna un utente esistente nel contesto del database.
        /// </summary>
        /// <param name="user">L'utente da aggiornare.</param>
        public void Aggiona(Utente user)
        {
            _context.Utenti.Update(user);
            _context.SaveChanges();
        }

        /// <summary>
        /// Elimina un utente dal database tramite l'ID.
        /// </summary>
        /// <param name="userId">L'ID dell'utente da eliminare.</param>
        public void Elimina(int userId)
        {
            var user = _context.Utenti.Find(userId);
            if (user != null)
            {
                _context.Utenti.Remove(user);
                _context.SaveChanges();
            }
        }
    }
}
