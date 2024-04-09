namespace InventoriaApi.Models;

public class Role
{
    public int RoleID { get; set; }
    public string RoleName { get; set; }

    // Navigation property
    public List<UserRole> UserRoles { get; set; }
    public List<RackAccessPermission> RackAccessPermissions { get; set; }
}

