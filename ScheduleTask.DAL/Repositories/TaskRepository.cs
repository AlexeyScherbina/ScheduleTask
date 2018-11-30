using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScheduleTask.DAL.Entities;
using ScheduleTask.DAL.Interfaces;

namespace ScheduleTask.DAL.Repositories
{
    public class TaskRepository : IRepository<Tasks>
    {

        private IApplicationDbContext db;

        public TaskRepository(IApplicationDbContext context)
        {
            db = context;
        }


        public Tasks GetById(int id)
        {
            return db.Tasks.FirstOrDefault(x => x.TaskId == id);
        }

        public IEnumerable<Tasks> GetAll()
        {
            return db.Tasks.ToList();
        }

        public void Create(Tasks task)
        {
            db.Tasks.Add(task);
            db.SaveChanges();
        }

        public void Update(Tasks task)
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

        public void Delete(int id)
        {
            Tasks temp = db.Tasks.FirstOrDefault(x => x.TaskId == id);
            if (temp.User != null) { temp.User.Tasks.Remove(temp); }
            db.Tasks.Remove(temp);
            db.SaveChanges();
        }

    }
}
