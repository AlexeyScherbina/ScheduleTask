using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Task1.Models;

namespace Task1.Services
{

    public interface ITaskService
    {
        Tasks GetById(int id);
        List<Tasks> GetTasks();
        void AddTask(Tasks task);
        void UpdateTask(Tasks task);
        void DeleteTask(int id);
        void AssignUser(int taskId, int userId);
    }

    public class TaskService : ITaskService
    {

        private ApplicationDbContext db = new ApplicationDbContext();

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
            temp.Day = task.Day;
            temp.Description = task.Description;
            temp.Name = task.Name;
            db.SaveChanges();
        }

        public void DeleteTask(int id)
        {
            Tasks temp = db.Tasks.FirstOrDefault(x => x.TaskId == id);
            temp.User.Tasks.Remove(temp);
            db.Tasks.Remove(temp);
            db.SaveChanges();
        }

        public void AssignUser(int taskId, int userId)
        {
            Tasks t = db.Tasks.FirstOrDefault(x => x.TaskId == taskId);
            User u = db.Users.FirstOrDefault(x => x.UserId == userId);
            t.User = u;
            db.SaveChanges();
        }
    }
}