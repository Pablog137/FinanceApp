using API.Models;

namespace API.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        Task<Account?> GetById(int id);
        Task<List<Account>> GetAll();
    }
}
