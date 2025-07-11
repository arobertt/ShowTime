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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetByEmailAsync(loginDto.Email);
            if (user == null)
                return null;
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password);
            if (!isPasswordValid)
                return null;
            return new LoginResponseDto
            {
                Email = user.Email,
                Role = user.Role,
            };
        }
        public async Task<LoginResponseDto?> RegisterAsync(RegisterDto registerDto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(registerDto.Email);
            if (existingUser != null)
                return null;

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
            registerDto.Password = hashedPassword;

            var newUser = new User
            {
                Email = registerDto.Email,
                Password = registerDto.Password,
                Role = 0
            };

            await _userRepository.AddAsync(newUser);
            return new LoginResponseDto
            {
                Email = newUser.Email,
                Role = newUser.Role
            };
        }

        public async Task<IList<UserGetDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(a => new UserGetDto
            {
                Id = a.Id,
                Email = a.Email,
                Role = a.Role == 0 ? "User" : "Admin"
            }).ToList();
        }

        public async Task DeleteUserAsync(int id)
        {
            try {
                var user = await _userRepository.GetByIdAsync(id);
                if (user == null)
                {
                    throw new KeyNotFoundException($"User with ID {id} not found.");
                }
                await _userRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting the user with ID {id}.", ex);
            }

        }

        public async Task<UserGetDto?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return null;
            return new UserGetDto
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role == 0 ? "User" : "Admin"
            };
        }

        public async Task<int> GetUserIdByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
                return -1;
            return user.Id;
        }

    }
}
