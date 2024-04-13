using InventoriaApi.Models;

namespace InventoriaApi.Services.RepositoryInterfaces
{
    public interface ICompanyRepository : IGenericRepository<Company>
    {
        public Task<IEnumerable<Company>> ReadAllRecordsWithDetailsAsync();
    }
}
