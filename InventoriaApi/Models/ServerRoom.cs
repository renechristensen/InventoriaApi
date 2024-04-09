namespace InventoriaApi.Models;
public class ServerRoom
{
    public int ServerRoomID { get; set; }
    public int DataCenterID { get; set; }
    public string ServerRoomName { get; set; }
    public int RackCapacity { get; set; }
    public DateTime StartupDate { get; set; }

    // Navigation properties
    public DataCenter DataCenter { get; set; }
    public List<DataRack> DataRacks { get; set; }
    public EnvironmentalSetting EnvironmentalSetting { get; set; }
}
