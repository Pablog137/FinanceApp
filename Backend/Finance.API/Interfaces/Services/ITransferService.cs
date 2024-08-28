using Finance.API.Dtos.Transfer;
using Finance.API.Models;

namespace Finance.API.Interfaces.Services
{
    public interface ITransferService
    {
        Task<Transfer> CreateTransferAsync(int userId, CreateTransferDto transferDto);

        Task<Transfer?> GetByIdAsync(int id, int userId);
    }
}
