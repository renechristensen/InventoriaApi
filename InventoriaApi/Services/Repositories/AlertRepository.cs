using InventoriaApi.Models;
using InventoriaApi.Data;
using InventoriaApi.Services.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoriaApi.Services.Repositories
{
    public class AlertRepository : GenericRepository<Alert, InventoriaDBcontext>, IAlertRepository
    {
        public AlertRepository(InventoriaDBcontext dbContext) : base(dbContext)
        {
        }
        public async Task<bool> EnvironmentalReadingExists(int id)
        {
            return await _context.EnvironmentalReadings.AnyAsync(er => er.EnvironmentalReadingID == id);
        }
        public async Task<IEnumerable<Alert>> ReadAllRecordsWithDetailsAsync()
        {
            return await _context.Alerts
                .Include(a => a.AlertType)
                .Include(a => a.EnvironmentalReading)
                    .ThenInclude(er => er.EnvironmentalSetting)
                        .ThenInclude(es => es.ServerRoom)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}

