using System.ComponentModel.DataAnnotations;

namespace InventoriaApi.DTOs.ReceivedDTOs;
public class AssignUserRoleDTO
{
    public int UserId { get; set; }
    public int RoleId { get; set; }
}