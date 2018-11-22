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

    public class TaskRepository : ITaskRepository
    {

        private ApplicationDbContext db;

        public TaskRepository(ApplicationDbContext context)
        {
            db = context;
        }


        public Tasks GetById(int id)
        {
            return db.Tasks.FirstOrDefault(x => x.TaskId == id);
        }

        public List<Tasks> GetTasks()
        {
            return db.Tasks.ToList();
        }

        public void AddTask(Tasks task)
        {
            db.Tasks.Add(task);
            db.SaveChanges();
        }

        public void UpdateTask(Tasks task)
        {
            Tasks temp = db.Tasks.FirstOrDefault(x => x.TaskId == task.TaskId);
            if (task.Day != null) { temp.Day = task.Day; }
            if (task.Description != null) { temp.Description = task.Description; }
            if (task.Name != null) { temp.Name = task.Name; }

            if (task.User != null)
            {
                temp.User = task.User;
            }
            db.SaveChanges();
        }

        public void DeleteTask(int id)
        {
            Tasks temp = db.Tasks.FirstOrDefault(x => x.TaskId == id);
            if(temp.User != null) { temp.User.Tasks.Remove(temp); }
            db.Tasks.Remove(temp);
            db.SaveChanges();
        }

    }
}