using InventoriaApi.Models;
using InventoriaApi.Data;
using InventoriaApi.Services.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace InventoriaApi.Services.Repositories
{
    public class RackUnitRepository: GenericRepository<RackUnit, InventoriaDBcontext>, IRackUnitRepository
    {
        public RackUnitRepository(InventoriaDBcontext DBcontext) : base(DBcontext)
        {
        }
    }
}