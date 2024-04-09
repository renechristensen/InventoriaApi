using InventoriaApi.Models;
using InventoriaApi.Data;
using InventoriaApi.Services.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace InventoriaApi.Services.Repositories
{
    public class UserChangeLogRepository: GenericRepository<UserChangeLog, InventoriaDBcontext>, IUserChangeLogRepository
    {
        public UserChangeLogRepository(InventoriaDBcontext DBcontext) : base(DBcontext)
        {
        }
    }
}