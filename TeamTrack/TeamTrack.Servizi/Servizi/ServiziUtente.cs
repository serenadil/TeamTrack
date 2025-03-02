using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTrack.Dominio;
using TeamTrack.Servizi.Repository;

namespace TeamTrack.Servizi.Servizi
{
    public class ServiziUtente
    {
        private readonly RepositoryUtente _repositoryUtente;


        public ServiziUtente(RepositoryUtente repository)
        {
            _repositoryUtente = repository;
        }

        /// <summary>
        /// Esegui la registrazione di un nuovo utente.
        /// </summary>
        /// <param name="email">L'email dell'utente.</param>
        /// <param name="password">La password dell'utente.</param>
        /// <param name="ruolo">Il ruolo dell'utente.</param>
        /// <param name="nome">Il nome dell'utente.</param>
        /// <returns>L'ID dell'utente appena registrato.</returns>
        public int Registrazione(string email, string password, Ruolo ruolo, string nome)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            var utente = new Utente(email, passwordHash, ruolo, nome);

            _repositoryUtente.Aggiungi(utente);

            return utente.Id;
        }

        /// <summary>
        /// Esegui l'autenticazione di un utente.
        /// </summary>
        /// <param name="email">L'email dell'utente.</param>
        /// <param name="password">La password dell'utente.</param>
        /// <returns>Un tuple che contiene lo stato dell'autenticazione, un messaggio e l'ID dell'utente (se autenticato).</returns>
        public (bool Success, string Message, int? UserId) Autenticazione(string email, string password)
        {
            var utente = _repositoryUtente.GetByEmail(email);

            if (utente == null)
            {
                return (false, "Utente non trovato", null);
            }

            if (!BCrypt.Net.BCrypt.Verify(password, utente.Password))
            {
                return (false, "Password errata", null);
            }

            return (true, $"Benvenuto, {utente.Nome}!", utente.Id);
        }

        /// <summary>
        /// Recupera un utente tramite il suo ID.
        /// </summary>
        /// <param name="id">L'ID dell'utente da recuperare.</param>
        /// <returns>L'oggetto utente corrispondente all'ID fornito.</returns>
        public Utente GetUtente(int id)
        {
            return _repositoryUtente.GetById(id);
        }

        /// <summary>
        /// Recupera un utente tramite la sua email.
        /// </summary>
        /// <param name="email">L'email dell'utente da recuperare.</param>
        /// <returns>L'oggetto utente corrispondente all'email fornita.</returns>
        public Utente GetUtente(string email)
        {
            return _repositoryUtente.GetByEmail(email);
        }

        /// <summary>
        /// Recupera la lista di progetti associati a un utente.
        /// </summary>
        /// <param name="id">L'ID dell'utente di cui recuperare i progetti.</param>
        /// <returns>Una collezione di progetti associati all'utente.</returns>
        public ICollection<Progetto> getProgettiUtente(int id)
        {
            ICollection<Progetto> p = this.GetUtente(id).Progetti;
            return p;
        }

        /// <summary>
        /// Salva o aggiorna i dati di un utente nel repository.
        /// </summary>
        /// <param name="utente">L'oggetto utente da salvare o aggiornare.</param>
        public void salvaUtente(Utente utente)
        {
            _repositoryUtente.Aggiona(utente);
        }
    }
}
