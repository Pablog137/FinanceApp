using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Finance.API.Dtos.Users;
using Microsoft.AspNetCore.Mvc;

namespace Finance.API.Interfaces
{
    public interface IAuthentication
    {
        Task<IActionResult> Register([FromBody] RegisterDto registerDto);
        Task<IActionResult> Login([FromBody] LoginDto loginDto);
        Task<IActionResult> LogOut();

    }
}