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
        public Task<Notification> CreateAsync(CreateNotificationDto notification)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Notification>> GetAllAsync()
        {
            throw new NotImplementedException();

        }

        public Task<List<Notification>> GetAllOrderedByTimeAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Notification> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Notification> UpdateAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
