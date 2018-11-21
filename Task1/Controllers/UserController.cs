using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.WebSockets;
using AutoMapper;
using ScheduleTask.BLL.DTO;
using ScheduleTask.BLL.Interfaces;
using ScheduleTask.DAL.Entities;
using Task1.Models;

namespace Task1.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        private readonly IUserService userService;

        public UserController(IUserService us)
        {
            userService = us;
        }

        public IEnumerable<UserViewModel> GetUsers()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, UserViewModel>()).CreateMapper();
            return mapper.Map<IEnumerable<UserDTO>, IEnumerable<UserViewModel>>(userService.GetUsers());
        }

        public IHttpActionResult AddUser(UserViewModel user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            UserDTO u = new UserDTO
            {
                FullName = user.FullName,
                Tasks = new List<TaskDTO>()
            };
            userService.AddUser(u);
            return Ok();
        }

        public IHttpActionResult DeleteUser(UserViewModel user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            userService.DeleteUser(user.UserId);
            return Ok();
        }
    }
}
