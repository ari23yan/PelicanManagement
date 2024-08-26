using Microsoft.EntityFrameworkCore;
using UsersManagement.Data.Context;
using UsersManagement.Domain.Dtos.Common.Pagination;
using UsersManagement.Domain.Dtos.Common.ResponseModel;
using UsersManagement.Domain.Dtos.Management.IdentityServer;
using UsersManagement.Domain.Entities.IdentityServer;
using UsersManagement.Domain.Entities.UsersManagement.Account;
using UsersManagement.Domain.Enums;
using UsersManagement.Domain.Interfaces;
using UsersManagement.Domain.Interfaces.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersManagement.Data.Repositories.GenericRepositories;

namespace UsersManagement.Data.Repositories.Management
{
    public class IdentityServerRepository : IdentityServerGenericRepository<Domain.Entities.IdentityServer.User>, IIdentityServerRepository
    {
        private readonly IPelicanRepository _pelicanRepository;
        public IdentityServerRepository(IdentityServerDbContext context, IPelicanRepository pelicanRepository) : base(context)
        {
            _pelicanRepository = pelicanRepository;
        }

        public async Task<Domain.Entities.IdentityServer.User> Get(int userId)
        {
            return await Context.Users.FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<Domain.Entities.IdentityServer.User> GetByUsername(string username)
        {
            return await Context.Users.FirstOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<ListResponseDto<Domain.Entities.IdentityServer.User>> GetPaginatedUsersList(PaginationDto paginationRequest, UserType type)
        {
            var responseDto = new ListResponseDto<Domain.Entities.IdentityServer.User>();
            var pelicanUsernames = (await _pelicanRepository.GetAllAsync())
                                  .Select(x => x.UserName)
                                  .ToHashSet();
            var skipCount = (paginationRequest.PageNumber - 1) * paginationRequest.PageSize;

            IQueryable<Domain.Entities.IdentityServer.User> query = type == UserType.Pelican
                ? Context.Users.Where(x => pelicanUsernames.Contains(x.UserName))
                : Context.Users.Where(x => !pelicanUsernames.Contains(x.UserName));

         
            if (!string.IsNullOrWhiteSpace(paginationRequest.Searchkey))
            {
                query = type == UserType.Pelican
                    ? query.Where(u => u.LastName.Contains(paginationRequest.Searchkey))
                    : query.Where(u => u.LastName.Contains(paginationRequest.Searchkey));
            }


            query = paginationRequest.FilterType == FilterType.Asc
                ? query.OrderBy(u => u.Id)
                : query.OrderByDescending(u => u.Id);

            responseDto.TotalCount = await query.CountAsync();
            responseDto.List = await query.Skip(skipCount)
                                          .Take(paginationRequest.PageSize)
                                          .ToListAsync();
            return responseDto;
        }
    }
}
