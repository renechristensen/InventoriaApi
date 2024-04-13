using System.ComponentModel.DataAnnotations;

namespace InventoriaApi.DTOs.ReceivedDTOs
{
    public class CreateDataRacksChangeLogDTO
    {
        [Required]
        public int DataRackID { get; set; }

        [Required]
        public int ChangedByUserID { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Change type must be under 100 characters")]
        public string ChangeType { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "Description must be under 1000 characters")]
        public string ChangeDescription { get; set; }
    }

    public class UpdateDataRacksChangeLogDTO
    {
        public string ChangeType { get; set; }
        public string ChangeDescription { get; set; }
    }
}
