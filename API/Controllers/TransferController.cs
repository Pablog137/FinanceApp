using API.Dtos.Transfer;
using API.Extensions;
using API.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace API.Controllers
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

                return CreatedAtAction(nameof(GetTransferById), new { id = transfer.Id }, transfer);
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
            var userId = User.GetUserId();
            if (userId == null) return Unauthorized();

            var transfer = await _transferService.GetTransferByIdAsync(id, userId.Value);
            return Ok(transfer);
        }

    }
}
