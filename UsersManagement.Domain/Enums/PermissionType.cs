using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersManagement.Domain.Enums
{
    public static  class PermissionType
    {

        #region User Controller
        public const string GetUserList = "5c35db79-2e05-465e-bd1b-680abb5149b2";
        public const string GetUser = "3e202f54-ba15-4b0b-b7cf-fb9605961f06";
        public const string AddUser = "4107d207-72fe-4d89-b9c1-df157d5b85b6";
        public const string DeleteUser = "361BC216-C1DA-43EB-8325-1AB1D929EDD5";
        public const string ActiveStatusUser = "f21eef4f-33d3-4c8a-be85-6129eb548ac9";
        public const string UpdateUser = "56f185b0-fc29-4c6c-b229-2c662c3cb9c2";


        #endregion

        #region Role Controller

        public const string GetRoleList = "520b33b0-5328-4a05-96e6-cdde111f44e2";
        public const string GetRole = "bdf03774-6e57-442e-a1a5-95abb65ab5c8";
        public const string AddRole = "c37e30f4-cfe6-4ac9-b75f-1eae7190a497";
        public const string DeleteRole = "863330f2-19d1-4d7e-ba20-81dd6863617d";
        public const string ActiveStatusRole = "81266c68-df8e-4228-b99b-05d5dc6cdf3e";
        public const string UpdateRole = "3e4c401d-cae2-4f6a-9c6d-498b72baa16f";
        #endregion

        #region Management Controller

        public const string GetPelicanUsersList = "75e077e2-a6ef-4538-ba2e-d6845f7e8e01";
        public const string GetPelicanUser = "3bf0e07b-a339-45db-8ef6-f7e29e69dd23";
        public const string AddPelicanUser = "1fb5c6b0-6637-485a-b44a-b25a5c63a810";
        public const string DeletePelicanUser = "82ff23e6-0f79-47db-bc72-93d3f11841ba";
        public const string UpdatePelicanUser = "cf586431-d7af-4cee-a3ee-7a0447b395b5";
        #endregion

        #region Log Controller
        public const string GetLogList = "344627c3-3fe6-48db-a8bb-0a4948b5d5eb";
        #endregion

    }

}
