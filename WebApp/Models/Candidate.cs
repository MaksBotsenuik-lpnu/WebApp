using System.Text.Json.Serialization;

namespace WebApp.Models;

public partial class Candidate
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? ContactInfo { get; set; }

    public string? Resume { get; set; }

    public string? WorkExperience { get; set; }

    public string? Education { get; set; }

    public string CurrentStatus { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<HiringEvent> HiringEvents { get; set; } = new List<HiringEvent>();

    public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();
}
