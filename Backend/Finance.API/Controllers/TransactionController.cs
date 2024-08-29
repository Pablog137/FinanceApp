using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Finance.API.Data;
using Finance.API.Dtos.Transaction;
using Finance.API.Exceptions;
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

        public async Task<IActionResult> GetAllTransaction()
        {
            try
            {
                var userId = User.GetUserId();
                if (userId == null) return Unauthorized();

                var transactions = await _transactionService.GetAllAsync(userId.Value);

                return Ok(transactions.Select(t => t.toDto()));

            }
            catch (AccountNotFoundException e)
            {
                Log.Error(e, e.Message);
                return NotFound(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                var userId = User.GetUserId();
                if (userId == null) return Unauthorized();

                var transaction = await _transactionService.GetByIdAsync(id, userId.Value);

                if (transaction == null) return NotFound();

                return Ok(transaction.toDto());
            }
            catch
            (AccountNotFoundException e)
            {
                Log.Error(e, e.Message);
                return NotFound(e.Message);
            }
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

                if (transaction == null) return BadRequest("Failed to create transaction");

                return CreatedAtAction(nameof(GetById), new { id = transaction.Id }, transaction.toDto());
            }
            catch (AccountNotFoundException e)
            {
                Log.Error(e, e.Message);
                return NotFound(e.Message);
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