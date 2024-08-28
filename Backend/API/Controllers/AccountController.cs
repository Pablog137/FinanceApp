using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Interfaces.Repositories;
using API.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace API.Controllers
{
    [Route("api/account")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {

        private readonly IAccountRepository _accountRepo;
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepo = accountRepository;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var accounts = await _accountRepo.GetAllAsync();

            return Ok(accounts.Select(a => a.toDto()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var account = await _accountRepo.GetByIdAsync(id);

            if (account == null) return NotFound("Account not found.");

            return Ok(account.toDto());
        }

    }
}