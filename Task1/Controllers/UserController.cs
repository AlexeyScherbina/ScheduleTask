using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Task1.Models;
using Task1.Services;

namespace Task1.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        private IUserService _us;

        public UserController(IUserService us)
        {
            _us = us;
        }

        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return _us.GetUsers();
        }

        [HttpPost]
        public bool AddUser(User user)
        {
            _us.AddUser(user);
            return true;
        }

        [HttpPut]
        public bool UpdateUser(User user)
        {
            _us.UpdateUser(user);
            return true;
        }

        [HttpDelete]
        public bool DeleteUser(User user)
        {
            _us.DeleteUser(user.UserId);
            return true;
        }
    }
}
