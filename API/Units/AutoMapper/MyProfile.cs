﻿using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper
{
    public class MyProfile : Profile
    {
        public MyProfile()
        {
            CreateMap<SysUser, UserDto>();
        }
    }
}
