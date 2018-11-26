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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TaskDTO t = new TaskDTO
            {
                Name = task.Name,
                Description = task.Description,
                Day = task.Day,
                User = null
            };
            try
            {
                taskService.AddTask(t);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Ok();
        }

        public IHttpActionResult DeleteTask(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                taskService.DeleteTask(id);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
            return Ok();
        }
    
        public IHttpActionResult AssignUser(AssignViewModel avm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                taskService.AssignUser(avm.TaskId, avm.UserId);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
            return Ok();
        }

        public IHttpActionResult AssignDay(TaskViewModel task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TaskDTO t = new TaskDTO
            {
                TaskId = task.TaskId,
                Day = task.Day,
                Description = task.Description,
                Name = task.Name
            };

            try
            {
                taskService.AssignDay(t);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
            return Ok();
        }
    }
}
