using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelicanManagement.Domain.Enums
{
    public enum ActivityLogType : long
    {
        CreateUser = 1,
        DeleteUser,
        UpdateUser,
        ActiveOrDeActiveUser,


        CreateRole,
        UpdateRole,
        DeleteRole,
        ActiveOrDeActiveRole=9,


        CreatePelicanUser,
        DeletePelicanUser,
        UpdatePelicanUser,



        CreateTeriageUser,
        DeleteTeriageUser,
        UpdateTeriageUser,

    }
}
