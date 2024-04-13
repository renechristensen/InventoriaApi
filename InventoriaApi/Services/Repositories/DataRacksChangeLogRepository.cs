using InventoriaApi.Models;
using InventoriaApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using InventoriaApi.Services.RepositoryInterfaces;

namespace InventoriaApi.Services.Repositories
{
    public class DataRacksChangeLogRepository : GenericRepository<DataRacksChangeLog, InventoriaDBcontext>, IDataRacksChangeLogRepository
    {
        public DataRacksChangeLogRepository(InventoriaDBcontext dbContext) : base(dbContext)
        {
        }

        // Example of a custom method to get logs with details
        public async Task<IEnumerable<DataRacksChangeLog>> GetDetailedLogs()
        {
            return await _context.DataRacksChangeLogs
                .Include(log => log.DataRack)
                .Include(log => log.ChangedByUser)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
