using Finance.API.Dtos.Transfer;
using Finance.API.Exceptions;
using Finance.API.Extensions;
using Finance.API.Interfaces.Services;
using Finance.API.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Finance.API.Controllers
{
    [Route("api/transfer")]
    [ApiController]
    public class TransferController : ControllerBase
    {

        private readonly ITransferService _transferService;

        public TransferController(ITransferService transferService)
        {
            _transferService = transferService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransfer([FromBody] CreateTransferDto transferDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {

                var userId = User.GetUserId();
                if (userId == null) return Unauthorized();

                var transfer = await _transferService.CreateTransferAsync(userId.Value, transferDto);

                return CreatedAtAction(nameof(GetTransferById), new { id = transfer.Id }, transfer.ToDto());
            }
            catch (AccountNotFoundException e)
            {
                Log.Error(e, e.Message);
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                Log.Error(e, "Error creating transfer");
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransferById([FromRoute] int id)
        {
            try
            {
                var userId = User.GetUserId();
                if (userId == null) return Unauthorized();

                var transfer = await _transferService.GetByIdAsync(id, userId.Value);
                return Ok(transfer);
            }
            catch (AccountNotFoundException e)
            {
                Log.Error(e, e.Message);
                return NotFound(e.Message);

            }
        }

    }
}
