using InventoriaApi.Models;

namespace InventoriaApi.Services.RepositoryInterfaces
{
    public interface IDataRackRepository : IGenericRepository<DataRack>
    {
        public Task<IEnumerable<DataRack>> ReadAllRecordsWithDetailsAsync();
    }
}
