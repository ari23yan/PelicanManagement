﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Security
{
    public interface IPasswordHasher
    {
        string EncodePasswordMd5(string pass);

    }
}
