using API.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/notification")]
    [ApiController]
    
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
            var notifications = await _notificationRepo.GetAllAsync();
            return Ok(notifications);
        }





    }
}
