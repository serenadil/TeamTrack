using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTrack.Domain.Entity
{
    public class User
    {
        public int Id { get; set; }  
        public string Name { get; set; }
        public string Email { get; set; }  

  
        public Role Role { get; set; }

        public ICollection<Project> Projects{ get; set; }

        public ICollection<ProjectTask> Tasks { get; set; }
    }
    public enum Role
    {
        Admin,  
        User    
    }
}
