using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using ScheduleTask.DAL.Entities;

namespace ScheduleTask.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }

}