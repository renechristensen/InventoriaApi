namespace InventoriaApi.Models;

public class RackAccessPermission
{
    public int RackAccessPermissionID { get; set; }
    public int DataRackID { get; set; }
    public int RoleID { get; set; }

    // Navigation properties
    public DataRack DataRack { get; set; }
    public Role Role { get; set; }
}
