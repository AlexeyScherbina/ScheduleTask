using ScheduleTask.DAL.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Task1.Models;
using Task1.Services;

namespace Task1.Controllers
{
    [Authorize]
    public class TaskController : ApiController
    {
        private IDataAccess _db;

        public TaskController(IDataAccess dataAccess)
        {
            _db = dataAccess;
        }

        //[HttpGet]
        public IEnumerable<Tasks> GetTasks()
        {
            return _db.Tasks.GetTasks();
        }

        public bool AddTask(Tasks task)
        {
            _db.Tasks.AddTask(task);
            return true;
        }

        //[HttpPut]
        public bool UpdateTask(Tasks task)
        {
            _db.Tasks.UpdateTask(task);
            return true;
        }

        //[HttpDelete]
        public bool DeleteTask(Tasks task)
        {
            _db.Tasks.DeleteTask(task.TaskId);
            return true;
        }

        
        public bool AssignUser(AssignViewModel avm)
        {
            _db.Tasks.AssignUser(avm.TaskId,avm.UserId);
            return true;
        }
    }
}
