using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task1.Models
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public List<TaskViewModel> Tasks { get; set; }
    }
}