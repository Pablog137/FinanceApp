using API.Models;

namespace API.Interfaces.Repositories
{
    public interface ITransferRepository
    {
        Task CreateTransferAsync(Transfer transfer);
        Task<Transfer> GetByIdAsync(int id, int userId);
    }
}
