using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScheduleTask.DAL.Interfaces;
using ScheduleTask.DAL.Repositories;


namespace ScheduleTask.DAL
{
    public class DataAccess : IDataAccess
    {
        private readonly ApplicationDbContext _db;
        private ITaskRepository _taskRepository;
        private IUserRepository _userRepository;

        public DataAccess(ITaskRepository taskRepo, IUserRepository userRepo)
        {
            _db = new ApplicationDbContext();
            _taskRepository = taskRepo;
            _userRepository = userRepo;
        }
        public ITaskRepository Tasks
        {
            get
            {
                /*if (_taskRepository == null)
                    _taskRepository = new TaskRepository(_db);*/
                return _taskRepository;
            }
        }

        public IUserRepository Users
        {
            get
            {
                /*if (_userRepository == null)
                    _userRepository = new UserRepository(_db);*/
                return _userRepository;
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
