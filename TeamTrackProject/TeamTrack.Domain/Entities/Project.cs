namespace TeamTrack.Domain.Entity

{
    /// <summary>
    /// Rappresenta un progetto con informazioni come nome, accesso, utenti associati, attività e le date di inizio e fine.
    /// </summary>
    public class Project
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
        public ICollection<ProjectTask> Tasks { get; set; }

        /// <summary>
        /// Collezione di utenti associati al progetto.
        /// </summary>
        public ICollection<User> Users { get; set; }

        /// <summary>
        /// Identificativo dell'amministratore del progetto.
        /// </summary>
        public int AdminId { get; set; }

        /// <summary>
        /// L'amministratore del progetto.
        /// </summary>
        public User Admin { get; set; }

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
        public Project(string name, string password, DateTime dataInizioProgetto, DateTime dataFineProgetto, string codiceAccesso, int adminId)
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
            Tasks = new List<ProjectTask>();
            Users = new List<User>();
        }

        /// <summary>
        /// Aggiunge una nuova attività al progetto.
        /// </summary>
        /// <param name="task">L'attività da aggiungere al progetto.</param>
        public void AddTask(ProjectTask task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task), "L'attività non può essere null.");
            }

            // Aggiungi la task alla collezione di attività
            Tasks.Add(task);
        }

        /// <summary>
        /// Aggiunge un nuovo utente al progetto.
        /// </summary>
        /// <param name="user">L'utente da aggiungere al progetto.</param>
        public void AddUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "L'utente non può essere null.");
            }

            // Aggiungi l'utente alla collezione di utenti
            Users.Add(user);
        }
    }
}
}
