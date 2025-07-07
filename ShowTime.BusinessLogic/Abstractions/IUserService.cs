using Microsoft.AspNetCore.Http;
using ShowTime.BusinessLogic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.BusinessLogic.Abstractions
{
    public interface IUserService
    {
        Task<LoginResponseDto> LoginAsync(LoginDto loginDto);
        Task<LoginResponseDto> RegisterAsync(RegisterDto registerDto);
    }
}
