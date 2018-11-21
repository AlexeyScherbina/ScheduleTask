using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScheduleTask.DAL.Interfaces;


namespace ScheduleTask.DAL
{
    public class DataAccess : IDataAccess
    {
        private readonly ApplicationDbContext _db;
        private TaskRepository _taskService;
        private UserRepository _userService;

        public DataAccess()
        {
            _db = new ApplicationDbContext();
        }
        public ITaskRepository Tasks
        {
            get
            {
                if (_taskService == null)
                    _taskService = new TaskRepository(_db);
                return _taskService;
            }
        }

        public IUserRepository Users
        {
            get
            {
                if (_userService == null)
                    _userService = new UserRepository(_db);
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
