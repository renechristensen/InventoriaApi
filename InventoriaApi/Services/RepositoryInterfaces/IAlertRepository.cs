using InventoriaApi.Models;

namespace InventoriaApi.Services.RepositoryInterfaces
{
    public interface IAlertRepository : IGenericRepository<Alert>
    {
        public Task<IEnumerable<Alert>> ReadAllRecordsWithDetailsAsync();
    }
}
