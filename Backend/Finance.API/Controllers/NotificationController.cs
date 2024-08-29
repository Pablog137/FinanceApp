using Finance.API.Dtos.Notification;
using Finance.API.Exceptions;
using Finance.API.Extensions;
using Finance.API.Interfaces.Repositories;
using Finance.API.Interfaces.Services;
using Finance.API.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Finance.API.Controllers
{
    [Route("api/notification")]
    [ApiController]
    [Authorize]

    public class NotificationController : ControllerBase
    {

        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var userId = User.GetUserId();
                if (userId == null) return Unauthorized();

                var notifications = await _notificationService.GetAllAsync(userId.Value);
                return Ok(notifications.Select(n => n.toDto()));

            }
            catch (AccountNotFoundException e)
            {
                Log.Error(e, "Error getting all notifications");
                return NotFound(e.Message);

            }

        }


        [HttpGet("ordered")]
        public async Task<IActionResult> GetAllOrdered()
        {
            try
            {
                var userId = User.GetUserId();
                if (userId == null) return Unauthorized();

                var notifications = await _notificationService.GetAllOrderedByTimeAsync(userId.Value);
                return Ok(notifications.Select(n => n.toDto()));
            }
            catch (AccountNotFoundException e)
            {
                Log.Error(e, "Error getting all ordered notifications");
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

                var notification = await _notificationService.GetByIdAsync(id, userId.Value);

                if (notification == null) return NotFound();

                return Ok(notification.toDto());

            }
            catch (AccountNotFoundException e)
            {
                Log.Error(e, "Error getting notification by id");
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateNotificationDto notificationDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {

                var userId = User.GetUserId();
                if (userId == null) return Unauthorized();

                var notification = await _notificationService.CreateAsync(notificationDto, userId.Value);

                return CreatedAtAction(nameof(GetById), new { id = notification.Id }, notification.toDto());
            }
            catch (AccountNotFoundException e)
            {
                Log.Error(e, "Error creating notification");
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                Log.Error(e, "Error creating notification");
                return StatusCode(500, e);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id)
        {
            try
            {
                var userId = User.GetUserId();
                if (userId == null) return Unauthorized();

                var notification = await _notificationService.UpdateAsync(id, userId.Value);
                if(notification == null) return NotFound("Notification not found");
                return Ok(notification.toDto());

            }
            catch (AccountNotFoundException e)
            {
                Log.Error(e, "Error creating notification");
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                Log.Error(e, "Error updating notification");
                return StatusCode(500, e);
            }

        }



    }
}
