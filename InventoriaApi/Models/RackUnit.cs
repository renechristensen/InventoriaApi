namespace InventoriaApi.Models;

public class RackUnit
{
    public int RackUnitID { get; set; }
    public int DataRackID { get; set; }
    public int UnitNumber { get; set; }

    // Navigation properties
    public DataRack DataRack { get; set; }
    public ICollection<ReservedRackUnit> ReservedRackUnits { get; set; }
    public ICollection<EquipmentRackUnit> EquipmentRackUnits { get; set; }
}
