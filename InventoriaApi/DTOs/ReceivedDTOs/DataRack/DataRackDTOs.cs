using System;
using System.ComponentModel.DataAnnotations;

namespace InventoriaApi.DTOs.ReceivedDTOs
{
    public class CreateDataRackDTO
    {
        [Required]
        public int ServerRoomID { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Rack placement must be 255 characters or less")]
        public string RackPlacement { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Total units must be at least 1")]
        public int TotalUnits { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Available units cannot be negative")]
        public int AvailableUnits { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Status must be 50 characters or less")]
        public string Status { get; set; }
    }

    public class UpdateDataRackDTO
    {
        [StringLength(255, ErrorMessage = "Rack placement must be 255 characters or less")]
        public string RackPlacement { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Total units must be at least 1")]
        public int TotalUnits { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Available units cannot be negative")]
        public int AvailableUnits { get; set; }

        [StringLength(50, ErrorMessage = "Status must be 50 characters or less")]
        public string Status { get; set; }
    }
}