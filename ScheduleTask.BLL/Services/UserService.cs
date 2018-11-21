using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ScheduleTask.BLL.DTO;
using ScheduleTask.BLL.Interfaces;
using ScheduleTask.DAL.Entities;
using ScheduleTask.DAL.Interfaces;


namespace ScheduleTask.BLL.Services
{
    public class UserService : IUserService
    {
        IDataAccess Database { get; set; }

        public UserService(IDataAccess db)
        {
            Database = db;
        }

        public void AddUser(UserDTO user)
        {
            User u = new User
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Tasks = new List<Tasks>()
            };
            Database.Users.AddUser(u);
            Database.Save();
        }

        public void DeleteUser(int id)
        {
            Database.Users.DeleteUser(id);
            Database.Save();
        }

        public UserDTO GetById(int id)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return mapper.Map<User, UserDTO>(Database.Users.GetById(id));
           
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(Database.Users.GetUsers());
            
        }
    }
}
