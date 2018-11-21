using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScheduleTask.DAL.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string FullName { get; set; }
        
        public virtual ICollection<Tasks> Tasks { get; set; }
    }
}