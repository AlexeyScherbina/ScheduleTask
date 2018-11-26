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
        IRepository<User> _userRepository { get; set; }

        public UserService(IRepository<User> userRepo)
        {
            _userRepository = userRepo;
        }

        public void AddUser(UserDTO user)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            _userRepository.Create(mapper.Map<UserDTO, User>(user));
        }

        public void DeleteUser(int id)
        {
            _userRepository.Delete(id);
        }

        public UserDTO GetById(int id)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return mapper.Map<User, UserDTO>(_userRepository.GetById(id));
           
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(_userRepository.GetAll());
            
        }
    }
}
