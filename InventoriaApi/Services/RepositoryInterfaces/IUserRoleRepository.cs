using InventoriaApi.Models;

namespace InventoriaApi.Services.RepositoryInterfaces;
public interface IUserRoleRepository : IGenericRepository<UserRole>
{
    public Task<List<UserRole>> ReadAllUserRolesByUserID(int UserID);
    public Task<bool> UserHasRole(int userId, int roleId);
    public Task AssignRoleToUser(int userId, int roleId);
}
