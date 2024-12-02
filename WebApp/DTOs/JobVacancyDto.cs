using System.ComponentModel.DataAnnotations;

namespace WebApp.DTOs
{
    public class JobVacancyDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Job title is required.")]
        [StringLength(255, ErrorMessage = "Job title cannot be longer than 255 characters.")]
        public string JobTitle { get; set; } = null!;

        [StringLength(1000, ErrorMessage = "Job description cannot be longer than 1000 characters.")]
        public string? JobDescription { get; set; }

        [Required(ErrorMessage = "Vacancy status is required.")]
        [StringLength(50, ErrorMessage = "Vacancy status cannot be longer than 50 characters.")]
        [RegularExpression(@"^(Open|Closed|Pending)$", ErrorMessage = "Vacancy status must be either 'Open', 'Closed', or 'Pending'.")]
        public string VacancyStatus { get; set; } = null!;

        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive number.")]
        public decimal? Salary { get; set; }

        [StringLength(3, ErrorMessage = "Currency should be a 3-letter code.")]
        public string? Currency { get; set; }

        [StringLength(100, ErrorMessage = "Location cannot be longer than 100 characters.")]
        public string? Location { get; set; }

        public List<string> Skills { get; set; } = new();
    }
}
