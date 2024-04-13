using System.ComponentModel.DataAnnotations;

namespace InventoriaApi.DTOs.ReceivedDTOs
{
    public class CreateCompanyDTO
    {
        [Required(ErrorMessage = "Company name is required")]
        [StringLength(255, ErrorMessage = "Company name must be 255 characters or less")]
        public string Name { get; set; }

        public string Description { get; set; }
    }

    public class UpdateCompanyDTO
    {
        [StringLength(255, ErrorMessage = "Company name must be 255 characters or less")]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
