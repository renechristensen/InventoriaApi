namespace InventoriaApi.Models;

public class DataRack
{
    public int DataRackID { get; set; }
    public int ServerRoomID { get; set; }
    public string RackPlacement { get; set; }
    public int TotalUnits { get; set; }
    public int AvailableUnits { get; set; }
    public string Status { get; set; }
    public DateTime CreationDate { get; set; }

    // Navigation properties
    public ServerRoom ServerRoom { get; set; }
    public ICollection<RackUnit> RackUnits { get; set; }
    public ICollection<DataRacksChangeLog> DataRacksChangeLogs { get; set; }
}

