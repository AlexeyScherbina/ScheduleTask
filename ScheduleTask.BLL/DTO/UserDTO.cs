using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleTask.BLL.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public List<TaskDTO> Tasks { get; set; }
    }

}
