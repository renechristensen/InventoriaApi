using System.ComponentModel.DataAnnotations;

namespace InventoriaApi.DTOs.ReceivedDTOs
{
    public class CreateReservedRackUnitDTO
    {
        [Required]
        public int ReservationID { get; set; }

        [Required]
        public int RackUnitID { get; set; }
    }

    public class UpdateReservedRackUnitDTO
    {
        public int ReservationID { get; set; }
        public int RackUnitID { get; set; }
    }
}
