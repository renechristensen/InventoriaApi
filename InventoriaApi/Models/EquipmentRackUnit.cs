namespace InventoriaApi.Models;

public class EquipmentRackUnit
{
    public int EquipmentRackUnitID { get; set; }
    public int EquipmentID { get; set; }
    public int RackUnitID { get; set; }

    // Navigation properties
    public Equipment Equipment { get; set; }
    public RackUnit RackUnit { get; set; }
}
