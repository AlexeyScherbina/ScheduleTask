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
        IRepository<Tasks> _taskRepository { get; set; }
        IRepository<User> _userRepository { get; set; }

        public TaskService(IRepository<Tasks> taskRepo, IRepository<User> userRepo)
        {
            _taskRepository = taskRepo;
            _userRepository = userRepo;
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

            _taskRepository.Create(t);
        }

        public void AssignDay(TaskDTO task)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TaskDTO, Tasks>()).CreateMapper();
            _taskRepository.Update(mapper.Map<TaskDTO, Tasks>(task));
        }

        public void AssignUser(int taskId, int userId)
        {
            Tasks t = _taskRepository.GetById(taskId);
            User u;
            if (userId == 0)
            {
                u = _userRepository.GetById(t.User.UserId);
                u.Tasks.Remove(t);
            }
            else
            {
                u = _userRepository.GetById(userId);
                t.User = u;
            }
            _taskRepository.Update(t);
        }

        public void DeleteTask(int id)
        {
            _taskRepository.Delete(id);
        }

        public TaskDTO GetById(int id)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Tasks, TaskDTO>()).CreateMapper();
            return mapper.Map<Tasks, TaskDTO>(_taskRepository.GetById(id));
        }

        public IEnumerable<TaskDTO> GetTasks()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Tasks, TaskDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Tasks>, IEnumerable<TaskDTO>>(_taskRepository.GetAll());
        }
    }
}
