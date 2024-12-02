using System.Collections.Generic;

namespace WebApp.DTOs
{
    public class GetCandidateDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? ContactInfo { get; set; }
        public string? Resume { get; set; }
        public string? WorkExperience { get; set; }
        public string? Education { get; set; }
        public string CurrentStatus { get; set; } = null!;

        public List<string> Skills { get; set; } = new();
    }
}