namespace InventoriaApi.Models;

public class Equipment
{
    public int EquipmentID { get; set; }
    public string Name { get; set; }
    public string Model { get; set; }
    public string Type { get; set; }

    // Navigation property
    public List<EquipmentRackUnit> EquipmentRackUnits { get; set; }
}

