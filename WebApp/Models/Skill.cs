using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebApp.Models;

public partial class Skill
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Candidate> Candidates { get; set; } = new List<Candidate>();

    [JsonIgnore]
    public virtual ICollection<JobVacancy> JobVacancies { get; set; } = new List<JobVacancy>();
}
