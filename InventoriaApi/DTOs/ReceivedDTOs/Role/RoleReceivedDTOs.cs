using System.ComponentModel.DataAnnotations;

namespace InventoriaApi.DTOs.ReceivedDTOs
{
    public class CreateRoleDTO
    {
        [Required]
        [StringLength(255, ErrorMessage = "Role name must be 255 characters or less")]
        public string RoleName { get; set; }
    }

    public class UpdateRoleDTO
    {
        [StringLength(255, ErrorMessage = "Role name must be 255 characters or less")]
        public string RoleName { get; set; }
    }
}
