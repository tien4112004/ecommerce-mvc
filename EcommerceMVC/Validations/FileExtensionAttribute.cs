using System.ComponentModel.DataAnnotations;

namespace EcommerceMVC.Data.Validations;

public class FileExtensionAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName);
            string[] validExtensions = { ".jpg", ".jpeg", ".png" };
            if (!validExtensions.Contains(extension.ToLower()))
            {
                return new ValidationResult("Invalid file extension. Please upload a valid file.");
            }
        }
        
        return ValidationResult.Success;
    }
}