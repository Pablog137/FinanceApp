﻿using API.Dtos.Notification;
using API.Extensions;
using API.Interfaces.Repositories;
using API.Interfaces.Services;
using API.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace API.Controllers
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
            var userId = User.GetUserId();
            if (userId == null) return Unauthorized();

            var notifications = await _notificationService.GetAllAsync(userId.Value);
            return Ok(notifications.Select(n => n.toDto()));
        }


        [HttpGet("get-ordered")]
        public async Task<IActionResult> GetAllOrdered()
        {
            var userId = User.GetUserId();
            if (userId == null) return Unauthorized();

            var notifications = await _notificationService.GetAllOrderedByTimeAsync(userId.Value);
            return Ok(notifications.Select(n => n.toDto()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var userId = User.GetUserId();
            if (userId == null) return Unauthorized();

            var notification = await _notificationService.GetByIdAsync(id, userId.Value);

            if (notification == null) return NotFound();

            return Ok(notification.toDto());
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
                return Ok(notification.toDto());

            }
            catch (Exception e)
            {
                Log.Error(e, "Error updating notification");
                return StatusCode(500, e);
            }

        }



    }
}
