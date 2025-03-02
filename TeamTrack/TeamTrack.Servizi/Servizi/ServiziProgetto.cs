using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTrack.Dominio;
using TeamTrack.Servizi.Repository;
namespace TeamTrack.Servizi.Servizi
{
    public class ServiziProgetto
    {
        private readonly RepositoryProgetto _repositoryProgetto;
        private readonly ServiziTaskProgetto _serviziTaskProgetto;
        private readonly ServiziUtente _serviziUtente;
        private readonly GeneratoreCodiciAccesso _generatoreCodiciAccesso;

        // Costruttore della classe ServiziProgetto
        public ServiziProgetto(RepositoryProgetto repositoryProgetto, ServiziUtente serviziUtente, GeneratoreCodiciAccesso generatore, ServiziTaskProgetto serviziTaskProgetto)
        {
            _repositoryProgetto = repositoryProgetto;
            _serviziTaskProgetto = serviziTaskProgetto;
            _serviziUtente = serviziUtente;
            _generatoreCodiciAccesso = generatore;
        }

        /// <summary>
        /// Crea un nuovo progetto verificando i parametri e chiamando il costruttore della classe Project.
        /// </summary>
        /// <param name="name">Il nome del progetto.</param>
        /// <param name="password">La password del progetto.</param>
        /// <param name="dataInizioProgetto">La data di inizio del progetto.</param>
        /// <param name="dataFineProgetto">La data di fine del progetto.</param>
        /// <param name="adminId">L'ID dell'amministratore del progetto.</param>
        /// <returns>Il progetto appena creato.</returns>
        /// <exception cref="ArgumentException">Se uno dei parametri non è valido.</exception>
        public Progetto creaProgetto(string name, string password, DateTime dataInizioProgetto, DateTime dataFineProgetto, int adminId)
        {
            var admin = _serviziUtente.GetUtente(adminId);
            if (admin == null || admin.Ruolo != Ruolo.Admin)
            {
                throw new ArgumentException("L'utente che sta creando il progetto deve essere un Admin.", nameof(adminId));
            }

            string codiceAccesso = _generatoreCodiciAccesso.GeneraCodiceUnico();

            var progetto = new Progetto(name, password, dataInizioProgetto, dataFineProgetto, codiceAccesso, adminId);

            _repositoryProgetto.Aggiungi(progetto);

            admin.AggiungiProgetto(progetto);
            _serviziUtente.salvaUtente(admin);

            return progetto;
        }

        /// <summary>
        /// Elimina un progetto e tutti gli utenti associati.
        /// </summary>
        /// <param name="id">L'ID del progetto da eliminare.</param>
        /// <returns>Un valore booleano che indica se l'eliminazione è riuscita.</returns>
        public Boolean eliminaProgetto(int id)
        {
            Progetto progetto = this.GetProgetto(id);
            if (progetto == null)
            {
                throw new ArgumentException("Nessun progetto trovato");
            }

            var utentiAssociati = progetto.Users.ToList();
            foreach (var utente in utentiAssociati)
            {
                utente.RimuoviProgetto(progetto);
                _serviziUtente.salvaUtente(utente);

                var taskProgetto = utente.Attivita.ToList();
                foreach (var a in taskProgetto)
                {
                    if (a.IdProgetto == progetto.Id)
                    {
                        utente.RimuoviTask(a);
                        _serviziUtente.salvaUtente(utente);
                        _serviziTaskProgetto.EliminaTaskProgetto(a.Id);
                    }
                }
            }

            _repositoryProgetto.Elimina(id);
            return true;
        }

        /// <summary>
        /// Aggiungi un utente a un progetto dato un codice di accesso e una password.
        /// </summary>
        /// <param name="user">L'utente da aggiungere al progetto.</param>
        /// <param name="codiceAccesso">Il codice di accesso del progetto.</param>
        /// <param name="password">La password del progetto.</param>
        /// <returns>Un valore booleano che indica se l'aggiunta è riuscita.</returns>
        public Boolean aggiungiUtente(Utente user, string codiceAccesso, string password)
        {
            Progetto progetto = this.GetProgetto(codiceAccesso);
            if (progetto == null)
            {
                throw new ArgumentException("Nessun progetto trovato per il codice di accesso");
            }

            if (progetto.Password == password)
            {
                if (!progetto.Users.Contains(user))
                {
                    progetto.AggiungiUtente(user);
                    _repositoryProgetto.Aggiorna(progetto);
                }

                if (!user.Progetti.Contains(progetto))
                {
                    user.AggiungiProgetto(progetto);
                    _serviziUtente.salvaUtente(user);
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Aggiorna la data di fine di un progetto esistente.
        /// </summary>
        /// <param name="progettoId">L'ID del progetto da aggiornare.</param>
        /// <param name="nuovaDataFine">La nuova data di fine del progetto.</param>
        /// <exception cref="ArgumentException">Se il progetto non esiste o la data non è valida.</exception>
        public void AggiornaDataFineProgetto(int progettoId, DateTime nuovaDataFine)
        {
            var progetto = _repositoryProgetto.GetById(progettoId);
            if (progetto == null)
            {
                throw new ArgumentException("Progetto non trovato.", nameof(progettoId));
            }

            if (nuovaDataFine <= progetto.DataInizioProgetto)
            {
                throw new ArgumentException("La data di fine deve essere successiva alla data di inizio.");
            }

            progetto.DataFineProgetto = nuovaDataFine;
            _repositoryProgetto.Aggiorna(progetto);
        }

        // Metodo per ottenere un progetto tramite codice di accesso
        public Progetto GetProgetto(string accessCode)
        {
            return _repositoryProgetto.GetByCodiceAccesso(accessCode);
        }

        // Metodo per ottenere un progetto tramite ID
        public Progetto GetProgetto(int id)
        {
            return _repositoryProgetto.GetById(id);
        }

        // Metodo per aggiornare un progetto nel repository
        public void AggiornaProgetto(Progetto progetto)
        {
            _repositoryProgetto.Aggiorna(progetto);
        }
    }
}
