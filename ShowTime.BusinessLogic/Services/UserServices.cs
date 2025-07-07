using Microsoft.AspNetCore.Http;
using ShowTime.BusinessLogic.Abstractions;
using ShowTime.BusinessLogic.DTOs;
using ShowTime.DataAccess.Models;
using ShowTime.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.BusinessLogic.Services
{
    public class UserService: IUserService
    {
        private readonly IRepository<User> _userRepository;
        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await ((UserRepository)_userRepository).GetByEmailAsync(loginDto.Email);
            if (user == null || user.Password != loginDto.Password) // tine minte sa dai hash
                return null;

            return new LoginResponseDto
            {
                Email = user.Email,
                Role = 0
            };
        }
        public async Task<LoginResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            var existingUser = await ((UserRepository)_userRepository).GetByEmailAsync(registerDto.Email);
            if (existingUser != null)
                return null;

            var newUser = new User
            {
                Email = registerDto.Email,
                Password = registerDto.Password, // tine minte sa dai hash
                Role = 0
            };

            await _userRepository.AddAsync(newUser);
            return new LoginResponseDto
            {
                Email = newUser.Email,
                Role = 0
            };
        }
    }
}
