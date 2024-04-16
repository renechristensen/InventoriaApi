using System;
using System.ComponentModel.DataAnnotations;

namespace InventoriaApi.DTOs.ReceivedDTOs
{
    public class CreateServerRoomDTO
    {
        [Required]
        public int DataCenterID { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Server room name must be 255 characters or less")]
        public string ServerRoomName { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Rack capacity must be at least 1")]
        public int RackCapacity { get; set; }

        [Required]
        public DateTime StartupDate { get; set; }  
    }

    public class UpdateServerRoomDTO
    {
        [StringLength(255, ErrorMessage = "Server room name must be 255 characters or less")]
        public string ServerRoomName { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Rack capacity must be at least 1")]
        public int RackCapacity { get; set; }

        public DateTime? StartupDate { get; set; }
    }
}
