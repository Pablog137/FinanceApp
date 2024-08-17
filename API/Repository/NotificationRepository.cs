using API.Data;
using API.Dtos.Notification;
using API.Interfaces.Repositories;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace API.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly AppDbContext _context;
        public NotificationRepository(AppDbContext context)
        {
            _context = context;

        }

        public async Task<List<Notification>> GetAllAsync(int userId)
        {
            var notifications = await _context.Notifications.Where(n => n.Account.UserId == userId).ToListAsync();
            return notifications;
        }

        public Task<List<Notification>> GetAllOrderedByTimeAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<Notification> GetByIdAsync(int id, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<Notification> UpdateAsync(int id, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<Notification> CreateAsync(CreateNotificationDto notification, int userId)
        {
            throw new NotImplementedException();
        }

    }
}
