using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Data;
using API.Dtos.Transaction;
using API.Interfaces.Repositories;
using API.Mappers;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/transaction")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepo;
        public TransactionController(ITransactionRepository transactionRepo)
        {
            _transactionRepo = transactionRepo;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllTransaction()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null) return Unauthorized();

            if (!int.TryParse(userIdClaim, out int userId))
            {
                return BadRequest("Invalid user ID format.");
            }

            var transactions = await _transactionRepo.GetAllTransaction(userId);

            if (transactions == null) return NotFound("No transactions found.");

            return Ok(transactions.Select(t => t.toDto()));

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null) return Unauthorized();

            if (!int.TryParse(userIdClaim, out int userId))
            {
                return BadRequest("Invalid user ID format.");
            }

            var transaction = await _transactionRepo.GetById(id, userId);

            if (transaction == null) return NotFound();

            return Ok(transaction.toDto());
        }

        [HttpPost("add-transaction")]
        public async Task<IActionResult> AddTransaction([FromBody] CreateTransactionDto transactionDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null) return Unauthorized();

            if (!int.TryParse(userIdClaim, out int userId))
            {
                return BadRequest("Invalid user ID format.");
            }

            var transaction = await _transactionRepo.AddTransaction(transactionDto, userId);

            if (transaction == null) return NotFound("Account not found.");


            return CreatedAtAction(nameof(GetById), new { id = transaction.Id }, transaction);


        }
    }
}