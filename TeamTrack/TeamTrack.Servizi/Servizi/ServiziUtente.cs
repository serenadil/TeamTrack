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
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            var utente = new Utente(email, passwordHash, ruolo, nome);

            _repositoryUtente.Aggiungi(utente);

            return utente.Id; 
        }

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