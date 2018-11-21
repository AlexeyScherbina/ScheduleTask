using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using AutoMapper;
using ScheduleTask.BLL.DTO;
using ScheduleTask.BLL.Interfaces;
using ScheduleTask.DAL.Entities;
using Task1.Models;

namespace Task1.Controllers
{
    [Authorize]
    public class TaskController : ApiController
    {
        private readonly ITaskService taskService;

        public TaskController(ITaskService ts)
        {
            taskService = ts;
        }

        public IEnumerable<TaskViewModel> GetTasks()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TaskDTO, TaskViewModel>()).CreateMapper();
            return mapper.Map<IEnumerable<TaskDTO>, IEnumerable<TaskViewModel>>(taskService.GetTasks());
        }

        public IHttpActionResult AddTask(TaskViewModel task)
        {
            if (task == null)
            {
                return BadRequest();
            }

            TaskDTO t = new TaskDTO
            {
                Name = task.Name,
                Description = task.Description,
                Day = task.Day,
                User = null
            };
            taskService.AddTask(t);
            return Ok();
        }

        public IHttpActionResult DeleteTask(TaskViewModel task)
        {
            if (task == null)
            {
                return BadRequest();
            }
            taskService.DeleteTask(task.TaskId);
            return Ok();
        }
    
        public IHttpActionResult AssignUser(AssignViewModel avm)
        {
            if (avm == null)
            {
                return BadRequest();
            }
            taskService.AssignUser(avm.TaskId,avm.UserId);
            return Ok();
        }
    }
}
