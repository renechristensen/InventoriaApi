namespace InventoriaApi.Models;
public class DataCenter
{
    public int DataCenterID { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
    public int CompanyID { get; set; }

    // Navigation properties
    public Company Company { get; set; }
    public List<ServerRoom> ServerRooms { get; set; }
}
