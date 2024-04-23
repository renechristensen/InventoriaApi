using System.ComponentModel.DataAnnotations;

namespace InventoriaApi.DTOs.ReceivedDTOs
{
    public class CreateEquipmentDTO
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Model { get; set; }

        [Required]
        [StringLength(255)]
        public string Type { get; set; }
        public List<int> RackUnitIDs { get; set; }
    }

    public class UpdateEquipmentDTO
    {
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Model { get; set; }

        [StringLength(255)]
        public string Type { get; set; }
    }
}