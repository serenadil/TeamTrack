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

        public int Registrazione(string email, string password, Ruolo ruolo, string nome)
        {
            // Creiamo l'utente con la password cifrata
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            var utente = new Utente(email, passwordHash, ruolo, nome);

            // Aggiungiamo l'utente al repository e ritorniamo l'ID
            _repositoryUtente.Aggiungi(utente);

            return utente.Id; // Restituiamo l'ID dell'utente appena creato
        }

        public bool Autenticazione(string email, string password)
        {
            var utente = this.GetUtente(email);
            if (utente == null || !BCrypt.Net.BCrypt.Verify(password, utente.Password))
            {
                return false;
            }

            return true;
        }

        public Utente GetUtente(int id)
        {
            return _repositoryUtente.GetById(id);
        }

        public Utente GetUtente(string email)
        {
            return _repositoryUtente.GetByEmail(email);
        }
    }
}