using System;
using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using ScheduleTask.BLL.DTO;
using ScheduleTask.BLL.Interfaces;
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (user == null)
            {
                return BadRequest("Empty data sent.");
            }

            if (user.FullName == null)
            {
                return BadRequest("FullName field can't be null");
            }

            UserDTO u = new UserDTO
            {
                FullName = user.FullName,
                Tasks = new List<TaskDTO>()
            };
            try
            {
                userService.AddUser(u);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Ok();
        }

        public IHttpActionResult DeleteUser(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                userService.DeleteUser(id);
            }
            catch(Exception e)
            {
                return InternalServerError(e);
            }
            return Ok();
        }
    }
}
