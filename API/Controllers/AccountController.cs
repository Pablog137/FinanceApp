using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly AppDbContext _context;
        public AccountController(AppDbContext context)
        {
            _context = context;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var accounts = await _context.Accounts.Include(a => a.User).Include(t => t.Transactions).Select(a => a.toDto()).ToListAsync();
            return Ok(accounts);
        }

    }
}