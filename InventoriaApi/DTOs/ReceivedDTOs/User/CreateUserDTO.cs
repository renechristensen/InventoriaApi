using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace InventoriaApi.DTOs.ReceivedDTOs;

public class CreateUserDTO
{
    [Required(ErrorMessage = "Display name is required")]
    public string Displayname { get; set; }

    [Required(ErrorMessage = "Studieemail is required")]
    [EmailAddress(ErrorMessage = "Studieemail should be an email")]
    public string StudieEmail { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Please use a more complex password (at least 8 characters, including 1 number and 1 special character)")]
    public string Password { get; set; }

    [Required(ErrorMessage = "CompanyID is required")]
    public int CompanyID { get; set; }
}
