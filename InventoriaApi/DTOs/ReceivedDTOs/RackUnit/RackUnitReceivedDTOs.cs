using System.ComponentModel.DataAnnotations;

namespace InventoriaApi.DTOs.ReceivedDTOs
{
    public class CreateRackUnitDTO
    {
        [Required]
        public int DataRackID { get; set; }

        [Required]
        public int UnitNumber { get; set; }
    }

    public class UpdateRackUnitDTO
    {
        public int DataRackID { get; set; }
        public int UnitNumber { get; set; }
    }
}
