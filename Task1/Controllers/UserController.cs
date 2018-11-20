using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using ScheduleTask.DAL.Interfaces;
using Task1.Models;
using Task1.Services;

namespace Task1.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        private IDataAccess _db;

        public UserController(IDataAccess dataAccess)
        {
            _db = dataAccess;
        }

        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return _db.Users.GetUsers();
        }

        [HttpPost]
        public bool AddUser(User user)
        {
            _db.Users.AddUser(user);
            return true;
        }

        [HttpPut]
        public bool UpdateUser(User user)
        {
            _db.Users.UpdateUser(user);
            return true;
        }

        [HttpDelete]
        public bool DeleteUser(User user)
        {
            _db.Users.DeleteUser(user.UserId);
            return true;
        }
    }
}
