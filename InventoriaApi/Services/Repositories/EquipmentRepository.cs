using InventoriaApi.Models;
using InventoriaApi.Data;
using InventoriaApi.Services.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace InventoriaApi.Services.Repositories
{
    public class EquipmentRepository: GenericRepository<Equipment, InventoriaDBcontext>, IEquipmentRepository
    {
        public EquipmentRepository(InventoriaDBcontext DBcontext) : base(DBcontext)
        {
        }
    }
}