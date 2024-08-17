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

        public async Task<List<Notification>> GetAllOrderedByTimeAsync(int userId)
        {
            var notifications = await _context.Notifications.Where(n => n.Account.UserId == userId).OrderByDescending(n => n.CreatedAt).ToListAsync();
            return notifications;
        }

        public async Task<Notification?> GetByIdAsync(int id, int userId)
        {
            var notification = await _context.Notifications.Where(n => n.Id == id && n.Account.UserId == userId).FirstOrDefaultAsync();
            return notification;
        }

        public async Task<Notification?> UpdateAsync(int id, int userId)
        {
            var account = _context.Accounts.Where(a => a.UserId == userId).FirstOrDefault();
            if (account == null) return null;

            var notification = _context.Notifications.Where(n => n.Id == id && n.AccountId == account.Id).FirstOrDefault();
            notification.IsRead = true;
            await _context.SaveChangesAsync();
            return notification;
        }

        public async Task<Notification?> CreateAsync(CreateNotificationDto notificationdto, int userId)
        {
            var account = await _context.Accounts.Where(a => a.UserId == userId).FirstOrDefaultAsync();
            if (account == null) return null;

            var notification = new Notification
            {
                AccountId = account.Id,
                Type = notificationdto.Type,
                Message = notificationdto.Message,
            };

            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();

            return notification;
        }

    }
}
