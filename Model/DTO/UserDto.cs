﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class UserDto
    {
        public string? user_id { get; set; }

        public string? user_name { get; set; }

        public string password { get; set; }

        public string? nickname { get; set; }

        public string? employee_id { get; set; }

        public DateTimeOffset create_at { get; set; }
    }
}
