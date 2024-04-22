using InventoriaApi.Models;
using InventoriaApi.Data;
using InventoriaApi.Services.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace InventoriaApi.Services.Repositories
{
    public class ReservationRepository: GenericRepository<Reservation, InventoriaDBcontext>, IReservationRepository
    {
        public ReservationRepository(InventoriaDBcontext DBcontext) : base(DBcontext)
        {
        }
        public async Task<bool> IsRackUnitAvailable(List<int> rackUnitIds, DateTime startDate, DateTime endDate)
        {
            return !await _context.ReservedRackUnits
                                  .Where(r => rackUnitIds.Contains(r.RackUnitID) &&
                                              r.Reservation.StartDate.Date < endDate &&
                                              r.Reservation.EndDate.Date > startDate)
                                  .AnyAsync();
        }

        public async Task<List<Reservation>> GetReservationsByUserId(int userId)
        {
            return await _context.Reservations
                .Where(r => r.UserID == userId)
                .Include(r => r.ReservedRackUnits)
                .ToListAsync();
        }

    }
}