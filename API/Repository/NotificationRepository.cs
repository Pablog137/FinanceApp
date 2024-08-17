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
        public Task<Notification> Create(CreateNotificationDto notification)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Notification>> GetAll()
        {
            throw new NotImplementedException();

        }

        public Task<List<Notification>> GetAllOrderedByTime()
        {
            throw new NotImplementedException();
        }

        public Task<Notification> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Notification> Update(int id)
        {
            throw new NotImplementedException();
        }
    }
}
