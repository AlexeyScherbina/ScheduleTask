using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScheduleTask.DAL.Entities;
using ScheduleTask.DAL.Interfaces;

namespace ScheduleTask.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private IApplicationDbContext db;

        public UserRepository(IApplicationDbContext context)
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

            if (temp.Tasks != null)
            {
                foreach (var t in temp.Tasks)
                {
                    t.User = null;
                }
            }

            db.Users.Remove(temp);
            db.SaveChanges();
        }
    }
}
