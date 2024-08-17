using API.Extensions;
using API.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            return Ok(notifications);
        }


        [HttpGet("get-ordered")]
        public async Task<IActionResult> GetAllOrdered()
        {
            var userId = User.GetUserId();
            if (userId == null) return Unauthorized();

            var notifications = await _notificationRepo.GetAllOrderedByTimeAsync(userId.Value);
            return Ok(notifications);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var userId = User.GetUserId();
            if (userId == null) return Unauthorized();

            var notification = await _notificationRepo.GetByIdAsync(id, userId.Value);

            if (notification == null) return NotFound();

            return Ok(notification);
        }



    }
}
