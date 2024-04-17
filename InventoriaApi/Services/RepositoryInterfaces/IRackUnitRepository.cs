using InventoriaApi.Models;

namespace InventoriaApi.Services.RepositoryInterfaces
{
    public interface IRackUnitRepository : IGenericRepository<RackUnit>
    {
        public Task<List<RackUnit>> GetAllRackUnitsByDataRackId(int dataRackId);
    }
}
