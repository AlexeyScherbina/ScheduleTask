using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleTask.BLL.DTO
{
    public class TaskDTO
    {
        public int TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Day { get; set; }
        public UserDTO User { get; set; }
    }
}
