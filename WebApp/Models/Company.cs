using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebApp.Models;

public partial class Company
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Industry { get; set; }

    public string? CompanySize { get; set; }

    public string? Location { get; set; }

    public string? Website { get; set; }

    public virtual ICollection<Employer> Employers { get; set; } = new List<Employer>();

    [JsonIgnore]
    public virtual ICollection<Recruiter> Recruiters { get; set; } = new List<Recruiter>();
}
