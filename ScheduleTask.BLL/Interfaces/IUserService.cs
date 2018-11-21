using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScheduleTask.BLL.DTO;


namespace ScheduleTask.BLL.Interfaces
{
    public interface IUserService
    {
        UserDTO GetById(int id);
        IEnumerable<UserDTO> GetUsers();
        void AddUser(UserDTO user);
        void DeleteUser(int id);
    }
}
