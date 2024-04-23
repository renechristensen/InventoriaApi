using InventoriaApi.Models;

namespace InventoriaApi.Services.RepositoryInterfaces
{
    public interface IEquipmentRepository : IGenericRepository<Equipment>
    {
        public Task<bool> AreRackUnitsAvailable(List<int> rackUnitIds);
        Task<bool> DeleteEquipmentByRackUnitId(int rackUnitId); 
    }
}
