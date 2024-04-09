using InventoriaApi.Models;
using InventoriaApi.Data;
using InventoriaApi.Services.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace InventoriaApi.Services.Repositories
{
    public class EnvironmentalReadingRepository: GenericRepository<EnvironmentalReading, InventoriaDBcontext>, IEnvironmentalReadingRepository
    {
        public EnvironmentalReadingRepository(InventoriaDBcontext DBcontext) : base(DBcontext)
        {
        }
    }
}