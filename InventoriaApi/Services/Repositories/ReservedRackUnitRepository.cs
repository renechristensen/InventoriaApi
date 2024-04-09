using InventoriaApi.Models;
using InventoriaApi.Data;
using InventoriaApi.Services.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace InventoriaApi.Services.Repositories
{
    public class ReservedRackUnitRepository: GenericRepository<ReservedRackUnit, InventoriaDBcontext>, IReservedRackUnitRepository
    {
        public ReservedRackUnitRepository(InventoriaDBcontext DBcontext) : base(DBcontext)
        {
        }
    }
}