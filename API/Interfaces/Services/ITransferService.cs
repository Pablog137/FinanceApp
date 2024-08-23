using API.Dtos.Transfer;
using API.Models;

namespace API.Interfaces.Services
{
    public interface ITransferService
    {
        Task<Transfer> CreateTransferAsync(int userId, CreateTransferDto transferDto);

        Task<Transfer?> GetTransferByIdAsync(int id, int userId);
    }
}
