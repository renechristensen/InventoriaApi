using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace InventoriaApi.DTOs.ReceivedDTOs;

public class UpdateUserDTO
{
    public string? Displayname { get; set; }

    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string? StudieEmail { get; set; }

    // Password is optional for update, but validated if provided
    [CustomPasswordValidation(ErrorMessage = "The password must be at least 8 characters long and include at least 1 number, 1 uppercase character, and 1 special character if provided.")]
    public string? Password { get; set; }
}

public class CustomPasswordValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var password = value as string;

        // If no password is provided, it's considered valid since the field is optional
        if (string.IsNullOrEmpty(password))
        {
            return ValidationResult.Success;
        }

        // Define your password complexity rules here
        var hasNumber = new Regex(@"[0-9]+");
        var hasUpperChar = new Regex(@"[A-Z]+");
        var hasMinimum8Chars = new Regex(@".{8,}");
        var hasSpecialCharacter = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

        var isValid = hasNumber.IsMatch(password) && hasUpperChar.IsMatch(password) &&
                      hasMinimum8Chars.IsMatch(password) && hasSpecialCharacter.IsMatch(password);

        if (!isValid)
        {
            return new ValidationResult("The password must be at least 8 characters long and contain at least 1 number, 1 uppercase character, and 1 special character.");
        }

        return ValidationResult.Success;
    }
}
