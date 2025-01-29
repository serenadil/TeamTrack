namespace TeamTrack.Domain.Entity
{
    public class Project
    {

        public int Id { get; set; }
        public string AccessCode { get; set; } = Guid.NewGuid().ToString();
        public string Password { get; set; }
        public string Name { get; set; }


        public ICollection<ProjectTask> Tasks { get; set; }


        public ICollection<User> Users { get; set; }


        public int AdminId { get; set; }
        public User Admin { get; set; }

        public DateTime DataInizioProgetto { get; set; }
        public DateTime DataFineProgetto { get; set; }
    }
}
