using System;
using System.ComponentModel.DataAnnotations;

namespace InventoriaApi.DTOs.ReceivedDTOs
{
    public class CreateAlertDTO
    {
        [Required(ErrorMessage = "Alert Type ID is required")]
        public int AlertTypeID { get; set; }

        [Required(ErrorMessage = "Threshold exceeded status is required")]
        public bool ThresholdExceeded { get; set; }

        [Required(ErrorMessage = "Environmental Reading ID is required")]
        public int EnvironmentalReadingID { get; set; }

        [Required(ErrorMessage = "Alert timestamp is required")]
        public DateTime AlertTimestamp { get; set; }

        public bool Resolved { get; set; }
    }

    public class UpdateAlertDTO
    {
        public bool? ThresholdExceeded { get; set; }
        public bool? Resolved { get; set; }
    }
}
