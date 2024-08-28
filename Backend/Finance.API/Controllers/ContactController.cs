using Finance.API.Dtos.Contact;
using Finance.API.Extensions;
using Finance.API.Helpers;
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
            var userId = User.GetUserId();
            if (userId == null) return Unauthorized();

            var contacts = await _contactService.GetAllAsync(userId.Value, queryObject);
            if (contacts == null) return NotFound();
            return Ok(contacts.Select(c => c.toDto()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var userId = User.GetUserId();
            if (userId == null) return Unauthorized();

            var contact = await _contactService.GetByIdAsync(id, userId.Value);
            if (contact == null) return NotFound();

            return Ok(contact.toDto());
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
                if (contact == null) return BadRequest();

                return CreatedAtAction(nameof(GetById), new { id = contact.Id }, contact.toDto());
            }
            catch (Exception e)
            {
                Log.Error(e, "Error creating contact");
                return StatusCode(500, e.Message);
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
                return StatusCode(500, e.Message);
            }

        }

    }
}
