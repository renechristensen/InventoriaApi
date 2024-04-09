using InventoriaApi.Models;
using InventoriaApi.Data;
using InventoriaApi.Services.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace InventoriaApi.Services.Repositories
{
    public class RoleRepository: GenericRepository<Role, InventoriaDBcontext>, IRoleRepository
    {
        public RoleRepository(InventoriaDBcontext DBcontext) : base(DBcontext)
        {
        }
    }
}