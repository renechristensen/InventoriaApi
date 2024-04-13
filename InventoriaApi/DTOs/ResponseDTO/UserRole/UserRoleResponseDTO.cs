namespace InventoriaApi.DTOs.ResponseDTO
{
    public class UserRoleDTO
    {
        public int UserRoleID { get; set; }
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; } // Assumed to join the Role data to get the name
    }
}