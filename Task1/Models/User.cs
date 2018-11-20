﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Task1.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string FullName { get; set; }
        
        public virtual ICollection<Tasks> Tasks { get; set; }
    }
}