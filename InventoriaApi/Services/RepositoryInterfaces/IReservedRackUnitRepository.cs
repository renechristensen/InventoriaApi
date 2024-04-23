using InventoriaApi.Models;

namespace InventoriaApi.Services.RepositoryInterfaces;
public interface IReservedRackUnitRepository : IGenericRepository<ReservedRackUnit>
{
    Task<List<ReservedRackUnit>> GetReservedRackUnitsByReservationId(int reservationId);
}
