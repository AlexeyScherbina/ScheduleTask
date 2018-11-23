using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using ScheduleTask.DAL.Entities;

namespace ScheduleTask.DAL.Interfaces
{

    public interface ITaskRepository
    {
        Tasks GetById(int id);
        List<Tasks> GetTasks();
        void AddTask(Tasks task);
        void UpdateTask(Tasks task);
        void DeleteTask(int id);
    }

}