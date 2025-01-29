using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTrack.Domain.Entity
{
    public class ProjectTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Priority Priority { get; set; }

        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }

        public ICollection<User> Users { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public Status Status { get; set; }
    }

    public enum Priority
    {
        High,
        Medium,
        Low
    }


    public enum Status

    {
        ToDo,
        Loading,
        Done
    }
}
