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

        public async Task<List<Notification>> GetAllAsync(Account account)
        {
            return await _context.Notifications.Where(n => n.Account.Id == account.Id).ToListAsync();
        }

        public async Task<List<Notification>> GetAllOrderedByTimeAsync(Account account)
        {
            return await _context.Notifications.Where(n => n.Account.Id == account.Id).OrderByDescending(n => n.CreatedAt).ToListAsync();
        }

        public async Task<Notification?> GetByIdAsync(int id, Account account)
        {
            return await _context.Notifications.Where(n => n.Id == id && n.Account.Id == account.Id).FirstOrDefaultAsync();
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
