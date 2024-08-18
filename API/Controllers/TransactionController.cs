using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Data;
using API.Dtos.Transaction;
using API.Extensions;
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

            var userId = User.GetUserId();
            if (userId == null) return Unauthorized();

            var transactions = await _transactionRepo.GetAllTransactionAsync(userId.Value);

            if (transactions == null) return NotFound("No transactions found.");

            return Ok(transactions.Select(t => t.toDto()));

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var userId = User.GetUserId();
            if (userId == null) return Unauthorized();

            var transaction = await _transactionRepo.GetByIdAsync(id, userId.Value);

            if (transaction == null) return NotFound();

            return Ok(transaction.toDto());
        }

        [HttpPost("add-transaction")]
        public async Task<IActionResult> AddTransaction([FromBody] CreateTransactionDto transactionDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {

                var userId = User.GetUserId();
                if (userId == null) return Unauthorized();

                var transaction = await _transactionRepo.AddTransactionAsync(transactionDto, userId.Value);

                if (transaction == null) return NotFound("Account not found.");


                return CreatedAtAction(nameof(GetById), new { id = transaction.Id }, transaction.toDto());
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }



        }
    }
}