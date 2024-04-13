using System.ComponentModel.DataAnnotations;

namespace InventoriaApi.DTOs.ReceivedDTOs
{
    public class CreateAlertTypeDTO
    {
        [Required]
        [StringLength(100)]
        public string TypeName { get; set; }

        [StringLength(255)]
        public string Description { get; set; }
    }

    public class UpdateAlertTypeDTO
    {
        [StringLength(100)]
        public string TypeName { get; set; }

        [StringLength(255)]
        public string Description { get; set; }
    }
}
