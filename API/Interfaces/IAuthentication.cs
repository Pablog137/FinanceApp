using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.Users;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface IAuthentication
    {
        Task<IActionResult> Register([FromBody] RegisterDto registerDto);
        Task<IActionResult> Login([FromBody] LoginDto loginDto);
        Task<IActionResult> LogOut();

    }
}