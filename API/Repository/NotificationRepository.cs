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
            var notifications = _context.Notifications.Where(n => n.Account.UserId == userId).OrderByDescending(n => n.CreatedAt).ToListAsync();
            return notifications;
        }

        public async Task<Notification?> GetByIdAsync(int id, int userId)
        {
            var notification = await _context.Notifications.Where(n => n.Id == id && n.Account.UserId == userId).FirstOrDefaultAsync();
            return notification;
        }

        public Task<Notification?> UpdateAsync(int id, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<Notification?> CreateAsync(CreateNotificationDto notification, int userId)
        {
            throw new NotImplementedException();
        }

    }
}
