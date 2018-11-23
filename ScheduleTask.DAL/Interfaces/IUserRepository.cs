using System.Collections.Generic;
using System.Linq;
using ScheduleTask.DAL.Entities;

namespace ScheduleTask.DAL.Interfaces
{
    public interface IUserRepository
    {
        User GetById(int id);
        List<User> GetUsers();
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
    }

}