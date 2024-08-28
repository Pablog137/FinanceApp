using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Finance.API.Data;
using Finance.API.Dtos.Transaction;
using Finance.API.Extensions;
using Finance.API.Interfaces.Repositories;
using Finance.API.Interfaces.Services;
using Finance.API.Mappers;
using Finance.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Finance.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/transaction")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllTransaction()
        {

            var userId = User.GetUserId();
            if (userId == null) return Unauthorized();

            var transactions = await _transactionService.GetAllTransactionsAsync(userId.Value);

            if (transactions == null) return NotFound("No transactions found.");

            return Ok(transactions.Select(t => t.toDto()));

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var userId = User.GetUserId();
            if (userId == null) return Unauthorized();

            var transaction = await _transactionService.GetTransactionByIdAsync(id, userId.Value);

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

                var transaction = await _transactionService.AddTransactionAsync(transactionDto, userId.Value);

                if (transaction == null) return NotFound("Account not found.");


                return CreatedAtAction(nameof(GetById), new { id = transaction.Id }, transaction.toDto());
            }
            catch (InvalidOperationException e)
            {
                Log.Error(e, "Account not found creating transaction");
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                Log.Error(e, "Error creating transaction");
                return StatusCode(500, e.Message);
            }

        }
    }
}