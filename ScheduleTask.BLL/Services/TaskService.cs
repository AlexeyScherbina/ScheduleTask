using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ScheduleTask.BLL.DTO;
using ScheduleTask.BLL.Interfaces;
using ScheduleTask.DAL.Interfaces;
using ScheduleTask.DAL;
using ScheduleTask.DAL.Entities;


namespace ScheduleTask.BLL.Services
{
    public class TaskService : ITaskService
    {
        IDataAccess Database { get; set; }

        public TaskService(IDataAccess db)
        {
            Database = db;
        }

        public void AddTask(TaskDTO task)
        {
            Tasks t = new Tasks
            {
                Name = task.Name,
                Description = task.Description,
                Day = task.Day,
                User = null
            };

            Database.Tasks.AddTask(t);
            Database.Save();
        }

        public void AssignDay(TaskDTO task)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TaskDTO, Tasks>()).CreateMapper();
            Database.Tasks.UpdateTask(mapper.Map<TaskDTO, Tasks>(task));
            Database.Save();
        }

        public void AssignUser(int taskId, int userId)
        {
            Tasks t = Database.Tasks.GetById(taskId);
            User u;
            if (userId == 0)
            {
                u = Database.Users.GetById(t.User.UserId);
                u.Tasks.Remove(t);
            }
            else
            {
                u = Database.Users.GetById(userId);
                t.User = u;
            }
            Database.Tasks.UpdateTask(t);
            Database.Save();
        }

        public void DeleteTask(int id)
        {
            Database.Tasks.DeleteTask(id);
            Database.Save();
        }

        public TaskDTO GetById(int id)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Tasks, TaskDTO>()).CreateMapper();
            return mapper.Map<Tasks, TaskDTO>(Database.Tasks.GetById(id));
        }

        public IEnumerable<TaskDTO> GetTasks()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Tasks, TaskDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Tasks>, IEnumerable<TaskDTO>>(Database.Tasks.GetTasks());
        }
    }
}
