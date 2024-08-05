﻿using PelicanManagement.Domain.Dtos.Common.Pagination;
using PelicanManagement.Domain.Dtos.Common.ResponseModel;
using PelicanManagement.Domain.Dtos.Management.IdentityServer;
using PelicanManagement.Domain.Dtos.Role;
using PelicanManagement.Domain.Dtos.User;
using PelicanManagement.Domain.Entities.IdentityServer;
using PelicanManagement.Domain.Entities.Pelican;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelicanManagement.Application.Services.Interfaces
{
    public interface IManagementService
    {
        Task<ResponseDto<IEnumerable<User>>> GetPaginatedUsersList(PaginationDto request);
        Task<ResponseDto<IEnumerable<ApiUser>>> GetPelicanPaginatedUsersList(PaginationDto request);



        Task<ResponseDto<User>> GetUserByUserId(int userId);

        Task<ResponseDto<ApiUser>> GetUserByUserId(string userId);




        Task<ResponseDto<bool>> AddUser(AddIdentityUserDto requset, Guid operatorId);










    }
}
