using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TeamTrack.Domain.Entity;

namespace TeamTrack.Infrastructure
{
    public class TeamTrackDbContext : DbContext
    {
        public TeamTrackDbContext(DbContextOptions<TeamTrackDbContext> options) : base(options) { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> Tasks { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasMany(p => p.Tasks)
                .WithOne(t => t.Project)
                .HasForeignKey(t => t.ProjectId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Projects)
                .WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>("UserProject",
                j => j.HasOne<Project>().WithMany().HasForeignKey("ProjectId"),
                j => j.HasOne<User>().WithMany().HasForeignKey("UserId")
                );


            modelBuilder.Entity<User>()
                .HasMany(u => u.Tasks)
                .WithMany(t => t.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserTask",
                    j => j.HasOne<ProjectTask>().WithMany().HasForeignKey("TaskId"),
                    j => j.HasOne<User>().WithMany().HasForeignKey("UserId")
                );

            modelBuilder.Entity<Project>()
                .HasOne(p => p.Admin)   // Un progetto ha un Admin
                .WithMany()              // Un Admin può gestire più progetti (oppure .WithMany(u => u.Projects) se vuoi che sia bidirezionale)
                .HasForeignKey(p => p.AdminId)  // Usa AdminId come chiave esterna
                .OnDelete(DeleteBehavior.Restrict); // Evita la cancellazione cascata

           base.OnModelCreating(modelBuilder);


        }
    }
}
