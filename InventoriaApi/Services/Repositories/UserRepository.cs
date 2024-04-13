using InventoriaApi.Models;
using InventoriaApi.Data;
using InventoriaApi.Services.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace InventoriaApi.Services.Repositories
{
    public class UserRepository: GenericRepository<User, InventoriaDBcontext>, IUserRepository
    {
        public UserRepository(InventoriaDBcontext DBcontext) : base(DBcontext)
        {
        }

        public async Task<bool> VerifyLogin(string studieEmail, string password)
        {
            User? user = ReadUserRecordByEmail(studieEmail);
            // verify user exists
            if (user != null)
            {
                // verify password
                if (VerifyPassword(password, user.PasswordHash, user.PasswordSalt ))
                {
                    return true;
                }
            }
            return false;
        }
        public User? ReadUserRecordByEmail(string studieEmail)
        {
            return _context.Users
               .Include(u => u.UserRoles)
                   .ThenInclude(ur => ur.Role)
               .FirstOrDefault(e => e.StudieEmail == studieEmail);
        }
        public async Task<User?> ReadUserRecordByUserID(int UserID)
        {
            return _context.Users
               .Include(u => u.UserRoles)
                   .ThenInclude(ur => ur.Role)
               .Include(u => u.Company)
               .FirstOrDefault(e => e.UserID == UserID);
        }
        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
        public async Task<List<User>> ReadAllUsersWithRoles()
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .Include(u => u.Company)
                .ToListAsync();
        }
    }
}