using InventoriaApi.Models;

namespace InventoriaApi.Services.RepositoryInterfaces
{
    public interface IEnvironmentalSettingRepository : IGenericRepository<EnvironmentalSetting>
    {
        public Task<List<EnvironmentalSetting>> ReadAllRecordsWithServerRoom();
    }
}
