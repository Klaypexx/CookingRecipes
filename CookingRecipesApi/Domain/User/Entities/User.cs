﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.User.Entities
{
    public class User
    {
        public User()
        { 
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public int Password { get; set; }

    }
}
