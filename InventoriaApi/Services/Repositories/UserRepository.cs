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
            return _context.Users.Where(e => e.StudieEmail == studieEmail).FirstOrDefault();
        }
        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

    }
}