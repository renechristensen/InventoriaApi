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
        public AlertRepository(InventoriaDBcontext DBcontext) : base(DBcontext)
        {
        }

        public async Task<IEnumerable<Alert>> ReadAllRecordsWithDetailsAsync()
        {
            return await _context.Alerts
                .Include(a => a.AlertType)
                .Include(a => a.EnvironmentalReading)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
