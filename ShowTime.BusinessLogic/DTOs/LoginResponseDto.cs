using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.BusinessLogic.DTOs
{
    public class LoginResponseDto
    {
        public string Email { get; set; } = string.Empty;
        public int Role { get; set; }
    }
}
