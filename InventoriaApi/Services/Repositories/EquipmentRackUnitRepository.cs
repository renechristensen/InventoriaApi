using InventoriaApi.Models;
using InventoriaApi.Data;
using InventoriaApi.Services.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace InventoriaApi.Services.Repositories
{
    public class EquipmentRackUnitRepository: GenericRepository<EquipmentRackUnit, InventoriaDBcontext>, IEquipmentRackUnitRepository
    {
        public EquipmentRackUnitRepository(InventoriaDBcontext DBcontext) : base(DBcontext)
        {
        }
    }
}