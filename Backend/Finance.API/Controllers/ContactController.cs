using Finance.API.Dtos.Contact;
using Finance.API.Exceptions;
using Finance.API.Extensions;
using Finance.API.Helpers;
using Finance.API.Interfaces.Services;
using Finance.API.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Finance.API.Controllers
{
    [ApiController]
    [Route("api/contact")]
    [Authorize]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject queryObject)
        {
            try
            {
                var userId = User.GetUserId();
                if (userId == null) return Unauthorized();

                var contacts = await _contactService.GetAllAsync(userId.Value, queryObject);
                return Ok(contacts.Select(c => c.toDto()));
            }
            catch (AccountNotFoundException e)
            {
                Log.Error(e, e.Message);
                return NotFound(new { message = e.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                var userId = User.GetUserId();
                if (userId == null) return Unauthorized();

                var contact = await _contactService.GetByIdAsync(id, userId.Value);
                if (contact == null) return NotFound();

                return Ok(contact.toDto());
            }
            catch (AccountNotFoundException e)
            {
                Log.Error(e, e.Message);
                return NotFound(new { message = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddContact([FromBody] CreateContactDto contactDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var userId = User.GetUserId();
                if (userId == null) return Unauthorized();

                var contact = await _contactService.AddContactAsync(contactDto, userId.Value);
                if (contact == null) return BadRequest("Failed to create contact");

                return CreatedAtAction(nameof(GetById), new { id = contact.Id }, contact.toDto());
            }
            catch (AccountNotFoundException e)
            {
                Log.Error(e, e.Message);
                return NotFound(new { message = e.Message });
            }
            catch (ContactAlreadyExistsException e)
            {
                Log.Error(e, e.Message);
                return StatusCode(409, new { message = e.Message });
            }
            catch (Exception e)
            {
                Log.Error(e, "Error creating contact");
                return StatusCode(500, new { message = "Error creating contact" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var userId = User.GetUserId();
                if (userId == null) return Unauthorized();

                var contact = await _contactService.DeleteAsync(id, userId.Value);
                if (contact == null) return NotFound();
                return Ok(contact.toDto());

            }
            catch (Exception e)
            {
                Log.Error(e, "Error deleting contact");
                return StatusCode(500, new { message = e.Message });
            }

        }

    }
}
