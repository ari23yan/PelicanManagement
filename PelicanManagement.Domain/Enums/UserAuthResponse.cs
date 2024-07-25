﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelicanManagement.Domain.Enums
{
    public enum UserAuthResponse
    {
        Success,
        NotFound,
        WrongPassword,
        NotAvtive,
        EmailNotConfirmed,
        IsDeleted,
        TooManyTries
    }
}
