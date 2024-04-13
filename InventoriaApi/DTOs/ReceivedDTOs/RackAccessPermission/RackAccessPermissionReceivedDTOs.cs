using System.ComponentModel.DataAnnotations;

namespace InventoriaApi.DTOs.ReceivedDTOs
{
    public class CreateRackAccessPermissionDTO
    {
        [Required]
        public int DataRackID { get; set; }

        [Required]
        public int RoleID { get; set; }
    }

    public class UpdateRackAccessPermissionDTO
    {
        public int DataRackID { get; set; }
        public int RoleID { get; set; }
    }
}