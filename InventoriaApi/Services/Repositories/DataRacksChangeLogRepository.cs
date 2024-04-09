using InventoriaApi.Models;
using InventoriaApi.Data;
using InventoriaApi.Services.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace InventoriaApi.Services.Repositories
{
    public class DataRacksChangeLogRepository: GenericRepository<DataRacksChangeLog, InventoriaDBcontext>, IDataRacksChangeLogRepository
    {
        public DataRacksChangeLogRepository(InventoriaDBcontext DBcontext) : base(DBcontext)
        {
        }
    }
}