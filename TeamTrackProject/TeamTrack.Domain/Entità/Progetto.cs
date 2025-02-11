namespace TeamTrack.Domain.Entity

{
    /// <summary>
    /// Rappresenta un progetto con informazioni come nome, accesso, utenti associati, attività e le date di inizio e fine.
    /// </summary>
    public class Progetto
    {
        /// <summary>
        /// Identificativo univoco del progetto.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Codice di accesso univoco per il progetto generato automaticamente.
        /// </summary>
        public string CodiceAccesso { get; set; }

        /// <summary>
        /// Password per accedere al progetto.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Nome del progetto.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Collezione di attività associate al progetto.
        /// </summary>
        public ICollection<TaskProgetto> Tasks { get; set; }

        /// <summary>
        /// Collezione di utenti associati al progetto.
        /// </summary>
        public ICollection<Utente> Users { get; set; }

        /// <summary>
        /// Identificativo dell'amministratore del progetto.
        /// </summary>
        public int AdminId { get; set; }

        /// <summary>
        /// L'amministratore del progetto.
        /// </summary>
        public Utente Admin { get; set; }

        /// <summary>
        /// Data di inizio del progetto.
        /// </summary>
        public DateTime DataInizioProgetto { get; set; }

        /// <summary>
        /// Data di fine del progetto.
        /// </summary>
        public DateTime DataFineProgetto { get; set; }


        /// <summary>
        /// Costruttore per creare un nuovo progetto con i dati forniti.
        /// Verifica che i dati siano validi e lancia eccezioni in caso contrario.
        /// </summary>
        /// <param name="name">Il nome del progetto. Non può essere null o vuoto.</param>
        /// <param name="password">La password per accedere al progetto. Non può essere null o vuota.</param>
        /// <param name="dataInizioProgetto">La data di inizio del progetto. Non può essere nel passato.</param>
        /// <param name="dataFineProgetto">La data di fine del progetto. Deve essere successiva alla data di inizio.</param>
        /// <param name="accessCode">Il codice di accesso del progetto. Se null o vuoto, verrà generato automaticamente.</param>
        /// <param name="adminId">L'ID dell'amministratore del progetto.</param>
        /// <exception cref="ArgumentException">Viene lanciata se uno dei parametri non è valido, ad esempio se il nome è null o vuoto, la password è null, o le date non sono corrette.</exception>
        /// 
        public Progetto(string name, string password, DateTime dataInizioProgetto, DateTime dataFineProgetto, string codiceAccesso, int adminId)
        {
          
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Il nome del progetto non può essere null o vuoto.", nameof(name));
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("La passwordnon può essere null o vuoto.", nameof(name));
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("La password del progetto non può essere null o vuota.", nameof(password));
            }
            if (dataInizioProgetto < DateTime.Now)
            {
                throw new ArgumentException("La data di inizio del progetto non può essere nel passato.", nameof(dataInizioProgetto));
            }
            if (dataFineProgetto <= dataInizioProgetto)
            {
                throw new ArgumentException("La data di fine del progetto deve essere successiva alla data di inizio.", nameof(dataFineProgetto));
            }
            Nome = name;
            Password = password;
            DataInizioProgetto = dataInizioProgetto;
            DataFineProgetto = dataFineProgetto;
            AdminId = adminId;
            CodiceAccesso = codiceAccesso;
            Tasks = new List<TaskProgetto>();
            Users = new List<Utente>();
        }

        public Progetto() { }

        /// <summary>
        /// Aggiunge una nuova attività al progetto.
        /// </summary>
        public void AggiungiTask(TaskProgetto task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task), "L'attività non può essere nulla.");
            }
            Tasks.Add(task);
        }

        /// <summary>
        /// Rimuove un'attività dal progetto se è presente.
        /// </summary>
        public void RimuoviTask(TaskProgetto task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task), "L'attività non può essere nulla.");
            }
            if (!Tasks.Contains(task))
            {
                throw new InvalidOperationException("L'attività non è associata a questo progetto.");
            }
            Tasks.Remove(task);
        }

        /// <summary>
        /// Aggiunge un nuovo utente al progetto.
        /// </summary>
        public void AggiungiUtente(Utente user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "L'utente non può essere nullo.");
            }
            Users.Add(user);
        }

        /// <summary>
        /// Rimuove un utente dal progetto se è presente.
        /// </summary>
        public void RimuoviUtente(Utente user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "L'utente non può essere nullo.");
            }
            if (!Users.Contains(user))
            {
                throw new InvalidOperationException("L'utente non è associato a questo progetto.");
            }
            Users.Remove(user);
        }
    }
}

