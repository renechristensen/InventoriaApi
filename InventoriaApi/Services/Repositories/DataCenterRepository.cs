using InventoriaApi.Models;
using InventoriaApi.Data;
using InventoriaApi.Services.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoriaApi.Services.Repositories
{
    public class DataCenterRepository : GenericRepository<DataCenter, InventoriaDBcontext>, IDataCenterRepository
    {
        public DataCenterRepository(InventoriaDBcontext DBcontext) : base(DBcontext)
        {
        }

        public async Task<IEnumerable<DataCenter>> ReadAllRecordsWithDetailsAsync()
        {
            return await _context.DataCenters
                .Include(dc => dc.ServerRooms)
                .Include(dc => dc.Company)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
