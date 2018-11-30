using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScheduleTask.BLL.DTO;

namespace ScheduleTask.BLL.Interfaces
{
    public interface ITaskService
    {
        TaskDTO GetById(int id);
        IEnumerable<TaskDTO> GetTasks();
        void AddTask(TaskDTO task);
        void AssignDay(TaskDTO task);
        void DeleteTask(int id);
        void AssignUser(int taskId, int userId);
    }
}
