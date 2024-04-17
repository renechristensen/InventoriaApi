using InventoriaApi.Models;
using InventoriaApi.Data;
using InventoriaApi.Services.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoriaApi.Services.Repositories
{
    public class RackUnitRepository : GenericRepository<RackUnit, InventoriaDBcontext>, IRackUnitRepository
    {
        public RackUnitRepository(InventoriaDBcontext DBcontext) : base(DBcontext)
        {
        }

        // Fetch all rack units for a specific data rack including related data
        public async Task<List<RackUnit>> GetAllRackUnitsByDataRackId(int dataRackId)
        {
            return await _context.RackUnits
                .Where(ru => ru.DataRackID == dataRackId)
                .Include(ru => ru.ReservedRackUnits)
                    .ThenInclude(rru => rru.Reservation)
                        .ThenInclude(res => res.User)
                .Include(ru => ru.EquipmentRackUnits)
                    .ThenInclude(eru => eru.Equipment)
                .ToListAsync();
        }
    }
}
