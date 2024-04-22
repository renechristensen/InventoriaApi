
namespace InventoriaApi.DTOs.ResponseDTO
{
        public class ReservationDTO
        {
            public int ReservationID { get; set; }
            public int UserID { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public string Background { get; set; }
            public List<int> ReservedRackUnitIDs { get; set; } // Only IDs to avoid circular reference
        }
}