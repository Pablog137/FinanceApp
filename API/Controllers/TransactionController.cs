using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Data;
using API.Dtos.Transaction;
using API.Mappers;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/transaction")]
    public class TransactionController : ControllerBase
    {
        private readonly AppDbContext _context;
        public TransactionController(AppDbContext context)
        {
            _context = context;

        }


        [HttpGet("get-all")]
        [Authorize]
        public async Task<IActionResult> GetAllTransaction()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null) return Unauthorized();

            if (!int.TryParse(userIdClaim, out int userId))
            {
                return BadRequest("Invalid user ID format.");
            }

            var transactions = await _context.Transactions.Where(t => t.Account_Id == userId).ToListAsync();

            if (transactions == null) return NotFound();

            return Ok(transactions.Select(t => t.toDto()));

        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null) return Unauthorized();

            if (!int.TryParse(userIdClaim, out int userId))
            {
                return BadRequest("Invalid user ID format.");
            }

            var transaction = await _context.Transactions.FirstOrDefaultAsync(t => t.Id == id && t.Account_Id == userId);

            if (transaction == null) return NotFound();

            return Ok(transaction.toDto());
        }

        [HttpPost("add-transaction")]
        [Authorize]
        public async Task<IActionResult> AddTransaction([FromBody] CreateTransactionDto transactionDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null) return Unauthorized();

            if (!int.TryParse(userIdClaim, out int userId))
            {
                return BadRequest("Invalid user ID format.");
            }

            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == transactionDto.Account_Id && a.User_Id == userId);

            if (account == null) return NotFound("Account not found.");

            var transaction = new Transaction
            {
                Account_Id = transactionDto.Account_Id,
                Type = transactionDto.Type,
                Amount = transactionDto.Amount,
                Description = transactionDto.Description,
                Date = DateTime.Now
            };

            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = transaction.Id }, transaction);


        }
    }
}