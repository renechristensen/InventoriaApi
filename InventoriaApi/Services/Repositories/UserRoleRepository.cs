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

    public async Task<bool> DeleteRecord(int userRoleId)
    {
        var userRole = await _context.UserRoles.FindAsync(userRoleId);
        if (userRole == null) return false;

        _context.UserRoles.Remove(userRole);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<UserRole>> ReadAllUserRoles()
    {
        return await _context.UserRoles
            .Include(ur => ur.Role)
            .ToListAsync();
    }

}
