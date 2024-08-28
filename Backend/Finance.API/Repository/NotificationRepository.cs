using Finance.API.Data;
using Finance.API.Dtos.Notification;
using Finance.API.Interfaces.Repositories;
using Finance.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Finance.API.Repository
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
            return await _context.Notifications.Where(n => n.Account.UserId == userId).ToListAsync();
        }

        public async Task<List<Notification>> GetAllOrderedByTimeAsync(int userId)
        {
            return await _context.Notifications.Where(n => n.Account.UserId == userId).OrderByDescending(n => n.CreatedAt).ToListAsync();
        }

        public async Task<Notification?> GetByIdAsync(int id, int userId)
        {
            return await _context.Notifications.Where(n => n.Id == id && n.Account.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task<Notification?> UpdateAsync(Notification notification, Account account)
        {
            notification.IsRead = true;
            await _context.SaveChangesAsync();
            return notification;
        }

        public async Task<Notification?> CreateAsync(Notification notification, Account account)
        {
            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();

            return notification;
        }

    }
}
