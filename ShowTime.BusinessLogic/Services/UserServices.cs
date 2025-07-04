using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShowTime.BusinessLogic.Abstractions;
using ShowTime.BusinessLogic.DTOs;
using ShowTime.DataAccess.Models;
using ShowTime.DataAccess.Repositories;

namespace ShowTime.BusinessLogic.Services
{
    public class UserService(IRepository<User> userRepository): IUserService
    {
        public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
        {
            return new LoginResponseDto
            {
                Role = 0
            };
        }
    }
}
