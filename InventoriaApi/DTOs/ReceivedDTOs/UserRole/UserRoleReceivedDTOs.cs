using System.ComponentModel.DataAnnotations;

namespace InventoriaApi.DTOs.ReceivedDTOs
{
    public class AssignUserRoleDTO
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int RoleId { get; set; }
    }
}
