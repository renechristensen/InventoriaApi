using InventoriaApi.Models;

namespace InventoriaApi.Services.RepositoryInterfaces;
public interface IServerRoomRepository : IGenericRepository<ServerRoom>
{
    public Task<IEnumerable<ServerRoom>> ReadAllRecordsWithDetailsAsync();
}
