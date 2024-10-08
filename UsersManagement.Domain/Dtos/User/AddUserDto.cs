﻿
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersManagement.Domain.Dtos.User
{
    public class AddUserDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public Guid? CreatedBy { get; set; }
        public Guid RoleId { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public Guid UserId { get; set; }
    }
}
