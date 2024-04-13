using System.ComponentModel.DataAnnotations;

namespace InventoriaApi.DTOs.ReceivedDTOs
{
    public class CreateDataCenterDTO
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(255, ErrorMessage = "Name must be 255 characters or less")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(500, ErrorMessage = "Address must be 500 characters or less")]
        public string Address { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Company ID is required")]
        public int CompanyID { get; set; }
    }

    public class UpdateDataCenterDTO
    {
        [StringLength(255, ErrorMessage = "Name must be 255 characters or less")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Address must be 500 characters or less")]
        public string Address { get; set; }

        public string Description { get; set; }
    }
}
