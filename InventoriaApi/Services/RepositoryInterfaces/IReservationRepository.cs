using InventoriaApi.Models;

namespace InventoriaApi.Services.RepositoryInterfaces;
public interface IReservationRepository : IGenericRepository<Reservation>
{
    public Task<bool> IsRackUnitAvailable(List<int> rackUnitIds, DateTime startDate, DateTime endDate);
    Task<List<Reservation>> GetReservationsByUserId(int userId);
}
