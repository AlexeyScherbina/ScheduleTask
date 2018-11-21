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

    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext db;

        public UserRepository(ApplicationDbContext context)
        {
            db = context;
        }

        public User GetById(int id)
        {
            return db.Users.FirstOrDefault(x => x.UserId == id);
        }

        public List<User> GetUsers()
        {
            return db.Users.ToList();
        }

        public void AddUser(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            User temp = db.Users.FirstOrDefault(x => x.UserId == user.UserId);
            temp.FullName = user.FullName;
            db.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            User temp = db.Users.FirstOrDefault(x => x.UserId == id);
            foreach (var t in temp.Tasks)
            {
                t.User = null;
            }
            db.Users.Remove(temp);
            db.SaveChanges();
        }
    }
}