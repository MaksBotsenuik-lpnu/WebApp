using System.ComponentModel.DataAnnotations;

namespace WebApp.DTOs
{
    public class CreateRecruiterDto
    {
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        public string LastName { get; set; } = null!;

        [StringLength(100, ErrorMessage = "Contact info cannot exceed 100 characters.")]
        public string? ContactInfo { get; set; }

        [StringLength(100, ErrorMessage = "Specialization cannot exceed 100 characters.")]
        public string? Specialization { get; set; }

        [StringLength(100, ErrorMessage = "Company name cannot exceed 100 characters.")]
        public string? CompanyName { get; set; }  // Опціональне поле назви компанії
    }
}