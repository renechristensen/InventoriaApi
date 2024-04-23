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

        public async Task<List<ReservedRackUnit>> GetReservedRackUnitsByReservationId(int reservationId)
        {
            return await _context.ReservedRackUnits
                .Where(rru => rru.ReservationID == reservationId)
                .Include(rru => rru.RackUnit)
                .ThenInclude(ru => ru.EquipmentRackUnits)
                    .ThenInclude(eru => eru.Equipment)
                .Include(rru => rru.Reservation)
                    .ThenInclude(res => res.User)
                .ToListAsync();
        }

    }
}