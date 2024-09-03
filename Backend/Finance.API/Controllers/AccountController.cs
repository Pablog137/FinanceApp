using Finance.API.Extensions;
using Finance.API.Interfaces.Repositories;
using Finance.API.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finance.API.Controllers
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

        [HttpGet("get-by-userId")]
        public async Task<IActionResult> GetByUserId()
        {
            var id = User.GetUserId();
            if (id == null) return Unauthorized();

            var account = await _accountRepo.GetByUserIdAsyncOrThrowException(id.Value);

            return Ok(account.toDto());
        }


    }
}