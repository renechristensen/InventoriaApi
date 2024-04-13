using System.ComponentModel.DataAnnotations;

namespace InventoriaApi.DTOs.ReceivedDTOs
{
    public class CreateEquipmentRackUnitDTO
    {
        [Required]
        public int EquipmentID { get; set; }

        [Required]
        public int RackUnitID { get; set; }
    }

    public class UpdateEquipmentRackUnitDTO
    {
        public int EquipmentID { get; set; }
        public int RackUnitID { get; set; }
    }
}