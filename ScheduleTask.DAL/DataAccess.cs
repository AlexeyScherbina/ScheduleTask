using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScheduleTask.DAL.Interfaces;
using Task1.Services;

namespace ScheduleTask.DAL
{
    public class DataAccess : IDataAccess
    {
        private readonly ApplicationDbContext _db;
        private TaskService _taskService;
        private UserService _userService;

        public DataAccess()
        {
            _db = new ApplicationDbContext();
        }
        public ITaskService Tasks
        {
            get
            {
                if (_taskService == null)
                    _taskService = new TaskService(_db);
                return _taskService;
            }
        }

        public IUserService Users
        {
            get
            {
                if (_userService == null)
                    _userService = new UserService(_db);
                return _userService;
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
