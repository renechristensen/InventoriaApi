using InventoriaApi.Models;

namespace InventoriaApi.Services.RepositoryInterfaces
{
    public interface IDataCenterRepository : IGenericRepository<DataCenter>
    {
        public Task<IEnumerable<DataCenter>> ReadAllRecordsWithDetailsAsync();
    }
}
