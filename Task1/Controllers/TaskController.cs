using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Task1.Models;
using Task1.Services;

namespace Task1.Controllers
{
    public class TaskController : ApiController
    {
        private ITaskService _ts;

        public TaskController(ITaskService ts)
        {
            _ts = ts;
        }

        //[HttpGet]
        public IEnumerable<Tasks> GetTasks()
        {
            return _ts.GetTasks();
        }

                public bool AddTask(Tasks task)
        {
            _ts.AddTask(task);
            return true;
        }

        //[HttpPut]
        public bool UpdateTask(Tasks task)
        {
            _ts.UpdateTask(task);
            return true;
        }

        //[HttpDelete]
        public bool DeleteTask(Tasks task)
        {
            _ts.DeleteTask(task.TaskId);
            return true;
        }

        
        public bool AssignUser(AssignViewModel avm)
        {
            _ts.AssignUser(avm.TaskId,avm.UserId);
            return true;
        }
    }
}
