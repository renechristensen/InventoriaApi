using InventoriaApi.Models;
using InventoriaApi.Data;
using InventoriaApi.Services.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace InventoriaApi.Services.Repositories
{
    public class EquipmentRepository: GenericRepository<Equipment, InventoriaDBcontext>, IEquipmentRepository
    {
        public EquipmentRepository(InventoriaDBcontext DBcontext) : base(DBcontext)
        {

        }
        public async Task<bool> AreRackUnitsAvailable(List<int> rackUnitIds)
        {
            return !await _context.EquipmentRackUnits
                                  .AnyAsync(equipmentRackUnit => rackUnitIds.Contains(equipmentRackUnit.RackUnitID));
        }

        public async Task<bool> DeleteEquipmentByRackUnitId(int rackUnitId)
        {
            // Fetch the equipment linked to the rack unit ID.
            var equipmentRackUnit = await _context.EquipmentRackUnits
                                                  .Include(e => e.Equipment)
                                                  .Where(e => e.RackUnitID == rackUnitId)
                                                  .Select(e => e.Equipment)
                                                  .FirstOrDefaultAsync();

            if (equipmentRackUnit != null)
            {
                _context.Equipments.Remove(equipmentRackUnit);  
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}