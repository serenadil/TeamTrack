using Microsoft.EntityFrameworkCore;
using TeamTrack.Domain.Entity;


namespace TeamTrack.Infrastructure
{
    /// <summary>
    /// Rappresenta il contesto del database di TeamTrack, che gestisce le entità di progetto, attività e utenti.
    /// </summary>
    public class TeamTrackDbContext : DbContext
    {
        /// <summary>
        /// Inizializza una nuova istanza di TeamTrackDbContext con le opzioni specificate.
        /// </summary>
        /// <param name="options">Opzioni per configurare il contesto del database.</param>
        public TeamTrackDbContext(DbContextOptions<TeamTrackDbContext> options) : base(options) { }

        /// <summary>
        /// Ottiene o imposta il set di entità per i progetti.
        /// </summary>
        public DbSet<Progetto> Projects { get; set; }

        /// <summary>
        /// Ottiene o imposta il set di entità per le attività.
        /// </summary>
        public DbSet<TaskProgetto> Tasks { get; set; }

        /// <summary>
        /// Ottiene o imposta il set di entità per gli utenti.
        /// </summary>
        public DbSet<Utente> Users { get; set; }

        /// <summary>
        /// Configura il modello del database, definendo le relazioni tra le entità.
        /// </summary>
        /// <param name="modelBuilder">L'oggetto ModelBuilder utilizzato per configurare il modello.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configura la relazione tra il progetto e le attività (uno a molti)
            modelBuilder.Entity<Progetto>()
                .HasMany(p => p.Tasks)          // Un progetto può avere molte attività
                .WithOne(t => t.Progetto)        // Un'attività è legata a un solo progetto
                .HasForeignKey(t => t.IdProgetto); // La chiave esterna è ProjectId

            // Configura la relazione molti a molti tra utenti e progetti
            modelBuilder.Entity<Utente>()
                .HasMany(u => u.Progetti)       // Un utente può partecipare a molti progetti
                .WithMany(p => p.Users)         // Un progetto può avere molti utenti
                .UsingEntity<Dictionary<string, object>>("UserProject",  // Definisce una tabella di collegamento
                    j => j.HasOne<Progetto>().WithMany().HasForeignKey("ProjectId"),  // Collega l'utente al progetto
                    j => j.HasOne<Utente>().WithMany().HasForeignKey("UserId")        // Collega il progetto all'utente
                );

            // Configura la relazione molti a molti tra utenti e attività
            modelBuilder.Entity<Utente>()
                .HasMany(u => u.Attivita)         // Un utente può essere associato a molte attività
                .WithMany(t => t.Utenti)        // Un'attività può avere molti utenti
                .UsingEntity<Dictionary<string, object>>(
                    "UserTask",  // Definisce una tabella di collegamento
                    j => j.HasOne<TaskProgetto>().WithMany().HasForeignKey("TaskId"),  // Collega l'utente all'attività
                    j => j.HasOne<Utente>().WithMany().HasForeignKey("UserId")        // Collega l'attività all'utente
                );

            // Configura la relazione tra il progetto e il suo amministratore (uno a molti)
            modelBuilder.Entity<Progetto>()
                .HasOne(p => p.Admin)        // Un progetto ha un solo amministratore
                .WithMany()                   // Un amministratore può gestire molti progetti
                .HasForeignKey(p => p.AdminId) // La chiave esterna è AdminId
                .OnDelete(DeleteBehavior.Restrict); // Impedisce la cancellazione cascata dell'amministratore

            // Chiama il metodo base per garantire che la configurazione venga applicata correttamente
            base.OnModelCreating(modelBuilder);
        }
    }
}
