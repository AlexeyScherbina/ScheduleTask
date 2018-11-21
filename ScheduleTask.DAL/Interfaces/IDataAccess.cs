using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ScheduleTask.DAL.Interfaces
{
    public interface IDataAccess : IDisposable
    {
        ITaskRepository Tasks { get; }
        IUserRepository Users { get; }
        void Save();
    }
}
