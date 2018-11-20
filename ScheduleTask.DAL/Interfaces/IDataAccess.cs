using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1.Services;

namespace ScheduleTask.DAL.Interfaces
{
    public interface IDataAccess : IDisposable
    {
        ITaskService Tasks { get; }
        IUserService Users { get; }
        void Save();
    }
}
