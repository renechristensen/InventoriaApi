using InventoriaApi.Data;
using InventoriaApi.Models;
using InventoriaApi.Services.Repositories;
using InventoriaApi.Services.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

public class UserRoleRepository : GenericRepository<UserRole, InventoriaDBcontext>, IUserRoleRepository
{
    public UserRoleRepository(InventoriaDBcontext DBcontext) : base(DBcontext)
    {
    }

    public async Task<List<UserRole>> ReadAllUserRolesByUserID(int userID)
    {
        return await _context.UserRoles
        .Where(ur => ur.UserID == userID)
            .Include(ur => ur.Role)
            .ToListAsync();
    }
    public async Task<bool> UserHasRole(int userId, int roleId)
    {
        return await _context.UserRoles.AnyAsync(ur => ur.UserID == userId && ur.RoleID == roleId);
    }
    public async Task AssignRoleToUser(int userId, int roleId)
    {
        var userRole = new UserRole
        {
            UserID = userId,
            RoleID = roleId
        };

        await _context.UserRoles.AddAsync(userRole);
        await _context.SaveChangesAsync();
    }
}
