using System.ComponentModel.DataAnnotations;

namespace ScheduleTask.DAL.Entities
{

    public class Tasks
    {
        [Key]
        public int TaskId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Day { get; set; }
        public virtual User User { get; set; }
    }
}