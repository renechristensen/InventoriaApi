using InventoriaApi.Models;

namespace InventoriaApi.Services.RepositoryInterfaces;
public interface IUserRepository : IGenericRepository<User>
{
    Task<bool> VerifyLogin(string email, string password);
    public User? ReadUserRecordByEmail(string studieEmail);
}
