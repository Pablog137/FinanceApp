using API.Dtos.Notification;
using API.Extensions;
using API.Interfaces.Repositories;
using API.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace API.Controllers
{
    [Route("api/notification")]
    [ApiController]
    [Authorize]

    public class NotificationController : ControllerBase
    {

        private readonly INotificationRepository _notificationRepo;
        public NotificationController(INotificationRepository notificationRepo)
        {
            _notificationRepo = notificationRepo;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = User.GetUserId();
            if (userId == null) return Unauthorized();

            var notifications = await _notificationRepo.GetAllAsync(userId.Value);
            return Ok(notifications.Select(n => n.toDto()));
        }


        [HttpGet("get-ordered")]
        public async Task<IActionResult> GetAllOrdered()
        {
            var userId = User.GetUserId();
            if (userId == null) return Unauthorized();

            var notifications = await _notificationRepo.GetAllOrderedByTimeAsync(userId.Value);
            return Ok(notifications.Select(n => n.toDto()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var userId = User.GetUserId();
            if (userId == null) return Unauthorized();

            var notification = await _notificationRepo.GetByIdAsync(id, userId.Value);

            if (notification == null) return NotFound();

            return Ok(notification.toDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateNotificationDto notificationDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.GetUserId();
            if (userId == null) return Unauthorized();

            var notification = await _notificationRepo.CreateAsync(notificationDto, userId.Value);

            return CreatedAtAction(nameof(GetById), new { id = notification.Id }, notification.toDto());
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id)
        {
            var userId = User.GetUserId();
            if (userId == null) return Unauthorized();

            var notification = await _notificationRepo.UpdateAsync(id, userId.Value);
            return Ok(notification.toDto());
        }



    }
}
