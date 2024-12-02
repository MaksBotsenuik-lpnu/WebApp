using System.ComponentModel.DataAnnotations;
using AutoMapper;
using WebApp.Models;

namespace WebApp.DTOs
{
    public class UpdateRecruiterDto
    {
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        public string? FirstName { get; set; } // Nullable, необов'язкове

        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        public string? LastName { get; set; } // Nullable, необов'язкове

        [StringLength(100, ErrorMessage = "Contact info cannot exceed 100 characters.")]
        public string? ContactInfo { get; set; }

        [StringLength(100, ErrorMessage = "Specialization cannot exceed 100 characters.")]
        public string? Specialization { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Successful closures must be a non-negative number.")]
        public int? SuccessfulClosures { get; set; }  // Nullable, необов'язкове

        [StringLength(100, ErrorMessage = "Company name cannot exceed 100 characters.")]
        public string? CompanyName { get; set; }  // Nullable, необов'язкове
    }
}
