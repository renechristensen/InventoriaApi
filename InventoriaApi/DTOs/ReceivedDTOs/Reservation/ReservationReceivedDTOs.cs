using System.ComponentModel.DataAnnotations;

namespace InventoriaApi.DTOs.ReceivedDTOs
{

        public class CreateReservationDTO
        {
            [Required]
            public int UserID { get; set; }

            [Required]
            public DateTime StartDate { get; set; }

            [Required]
            public DateTime EndDate { get; set; }

            public string Background { get; set; }

            [Required]
            public List<int> RackUnitIDs { get; set; }
        }


    public class UpdateReservationDTO
    {
        public int UserID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Background { get; set; }
    }
}