﻿using System;
using System.Collections.Generic;

using TeamTrack.Application.Repositories;
using TeamTrack.Application.Servicies;
using TeamTrack.Domain.Entity;
namespace TeamTrack.Application.Services
{
    public class ServiziProgetto
    {
        private readonly RepositoryProgetto _repositoryProgetto;
        private readonly ServiziTaskProgetto _serviziTaskProgetto;
        private readonly ServiziUtente _serviziUtente;
        private readonly GeneratoreCodiciAccesso _generatoreCodiciAccesso;

      
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
        /// 
        public Progetto CreaProgetto(string name, string password, DateTime dataInizioProgetto, DateTime dataFineProgetto, int adminId)
        {
            var admin = _serviziUtente.GetUtente(adminId);
            if (admin == null || admin.Ruolo != Ruolo.Admin)
            {
                throw new ArgumentException("L'utente che sta creando il progetto deve essere un Admin.", nameof(adminId));
            }
            string codiceAccesso = _generatoreCodiciAccesso.GeneraCodiceUnico();
            var progetto = new Progetto(name, password, dataInizioProgetto, dataFineProgetto, codiceAccesso, adminId);
            return _repositoryProgetto.Aggiungi(progetto);
        }



        public Boolean eliminaProgetto(int id)
        {
            Progetto progetto = this.GetProgetto(id);
            if (progetto == null)
            {
                throw new ArgumentException("Nessun progetto trovato");
            }
            _repositoryProgetto.Elimina(id);
            return true;        
        }


        public Boolean aggiungiUtente(Utente user, string codiceAccesso, string password)
        {
            Progetto progetto = this.GetProgetto(codiceAccesso);
            if(progetto == null)
            {
                throw new ArgumentException("Nessun progetto trovato per il codice di accesso");
            }
            if (progetto.Password == password)
            {
                progetto.AggiungiUtente(user);
                _repositoryProgetto.Aggiorna(progetto);
                return true;
            }return false;
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


        public Progetto GetProgetto(string accessCode)
        {
            return _repositoryProgetto.GetByCodiceAccesso(accessCode);
        }

        public Progetto GetProgetto(int id)
        {
            return _repositoryProgetto.GetById(id);
        }

        public void AggiornaProgetto (Progetto progetto)
        {
            _repositoryProgetto.Aggiorna(progetto);
        }



    }
}