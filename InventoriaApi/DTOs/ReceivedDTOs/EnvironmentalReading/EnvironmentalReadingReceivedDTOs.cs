using System.ComponentModel.DataAnnotations;

namespace InventoriaApi.DTOs.ReceivedDTOs
{
    public class CreateEnvironmentalReadingDTO
    {
        [Required]
        public float? Temperature { get; set; }

        [Required]
        public float? Humidity { get; set; }

        public DateTime? ReadingTimestamp { get; set; } // Optional if set automatically
    }

    public class UpdateEnvironmentalReadingDTO
    {
        public float? Temperature { get; set; }
        public float? Humidity { get; set; }
        public DateTime? ReadingTimestamp { get; set; }
    }
}
