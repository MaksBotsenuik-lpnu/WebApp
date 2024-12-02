using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Models;

public partial class RecruitmentAgencyContext : DbContext
{
    public RecruitmentAgencyContext()
    {
    }

    public RecruitmentAgencyContext(DbContextOptions<RecruitmentAgencyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Candidate> Candidates { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Employer> Employers { get; set; }

    public virtual DbSet<HiringEvent> HiringEvents { get; set; }

    public virtual DbSet<HiringEventStage> HiringEventStages { get; set; }

    public virtual DbSet<JobVacancy> JobVacancies { get; set; }

    public virtual DbSet<Recruiter> Recruiters { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySQL("Server=localhost;Database=recruitment_agency;User=root;Password=password;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Candidate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Candidate");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ContactInfo)
                .HasMaxLength(255)
                .HasColumnName("contact_info");
            entity.Property(e => e.CurrentStatus)
                .HasColumnType("enum('Active','Inactive','Looking for Opportunities')")
                .HasColumnName("current_status");
            entity.Property(e => e.Education)
                .HasColumnType("text")
                .HasColumnName("education");
            entity.Property(e => e.FirstName)
                .HasMaxLength(45)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(45)
                .HasColumnName("last_name");
            entity.Property(e => e.Resume)
                .HasColumnType("text")
                .HasColumnName("resume");
            entity.Property(e => e.WorkExperience)
                .HasColumnType("text")
                .HasColumnName("work_experience");

            entity.HasMany(d => d.Skills).WithMany(p => p.Candidates)
                .UsingEntity<Dictionary<string, object>>(
                    "CandidateSkill",
                    r => r.HasOne<Skill>().WithMany()
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("candidate_skill_fk_skill_id"),
                    l => l.HasOne<Candidate>().WithMany()
                        .HasForeignKey("CandidateId")
                        .HasConstraintName("candidate_skill_fk_candidate_id"),
                    j =>
                    {
                        j.HasKey("CandidateId", "SkillId").HasName("PRIMARY");
                        j.ToTable("CandidateSkill");
                        j.HasIndex(new[] { "CandidateId" }, "candidate_skill_fk_candidate_id_idx");
                        j.HasIndex(new[] { "SkillId" }, "candidate_skill_fk_skill_id_idx");
                        j.IndexerProperty<int>("CandidateId").HasColumnName("candidate_id");
                        j.IndexerProperty<int>("SkillId").HasColumnName("skill_id");
                    });
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Company");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CompanySize)
                .HasColumnType("enum('Small','Medium','Large')")
                .HasColumnName("company_size");
            entity.Property(e => e.Industry)
                .HasMaxLength(100)
                .HasColumnName("industry");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .HasColumnName("location");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
            entity.Property(e => e.Website)
                .HasMaxLength(255)
                .HasColumnName("website");
        });

        modelBuilder.Entity<Employer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Employer");

            entity.HasIndex(e => e.Email, "email_UNIQUE").IsUnique();

            entity.HasIndex(e => e.CompanyId, "employer_fk_company_id_idx");

            entity.HasIndex(e => e.Phone, "phone_UNIQUE").IsUnique();

            entity.HasIndex(e => e.Email, "unique_email").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(45)
                .HasColumnName("address");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.Email)
                .HasMaxLength(45)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(45)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(45)
                .HasColumnName("last_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");

            entity.HasOne(d => d.Company).WithMany(p => p.Employers)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("employer_fk_company_id");
        });

        modelBuilder.Entity<HiringEvent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("HiringEvent");

            entity.HasIndex(e => e.CandidateId, "hiring_event_fk_candidate_id");

            entity.HasIndex(e => e.VacancyId, "hiring_event_fk_job_vacancy_id");

            entity.HasIndex(e => e.RecruiterId, "hiring_event_fk_recruiter_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CandidateId).HasColumnName("candidate_id");
            entity.Property(e => e.Feedback)
                .HasColumnType("text")
                .HasColumnName("feedback");
            entity.Property(e => e.RecruiterId).HasColumnName("recruiter_id");
            entity.Property(e => e.VacancyId).HasColumnName("vacancy_id");

            entity.HasOne(d => d.Candidate).WithMany(p => p.HiringEvents)
                .HasForeignKey(d => d.CandidateId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("hiring_event_fk_candidate_id");

            entity.HasOne(d => d.Recruiter).WithMany(p => p.HiringEvents)
                .HasForeignKey(d => d.RecruiterId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("hiring_event_fk_recruiter_id");

            entity.HasOne(d => d.Vacancy).WithMany(p => p.HiringEvents)
                .HasForeignKey(d => d.VacancyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("hiring_event_fk_job_vacancy_id");
        });

        modelBuilder.Entity<HiringEventStage>(entity =>
        {
            entity.HasKey(e => new { e.HiringEventId, e.Stage, e.Status }).HasName("PRIMARY");

            entity.ToTable("HiringEventStage");

            entity.HasIndex(e => e.HiringEventId, "hiring_event_stage_fk_hiring_event_id");

            entity.Property(e => e.HiringEventId).HasColumnName("hiring_event_id");
            entity.Property(e => e.Stage)
                .HasColumnType("enum('Applied','Interview','Offer','Rejected')")
                .HasColumnName("stage");
            entity.Property(e => e.Status)
                .HasColumnType("enum('Pending','Accepted','Rejected')")
                .HasColumnName("status");
            entity.Property(e => e.EndDate)
                .HasColumnType("timestamp")
                .HasColumnName("end_date");
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("start_date");

            entity.HasOne(d => d.HiringEvent).WithMany(p => p.HiringEventStages)
                .HasForeignKey(d => d.HiringEventId)
                .HasConstraintName("hiring_event_stage_fk_hiring_event_id");
        });

        modelBuilder.Entity<JobVacancy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("JobVacancy");

            entity.HasIndex(e => e.RecruiterId, "recruiter_id_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Currency)
                .HasMaxLength(3)
                .HasColumnName("currency");
            entity.Property(e => e.JobDescription)
                .HasColumnType("text")
                .HasColumnName("job_description");
            entity.Property(e => e.JobTitle)
                .HasMaxLength(255)
                .HasColumnName("job_title");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .HasColumnName("location");
            entity.Property(e => e.RecruiterId).HasColumnName("recruiter_id");
            entity.Property(e => e.Requirements)
                .HasColumnType("text")
                .HasColumnName("requirements");
            entity.Property(e => e.Salary)
                .HasPrecision(10)
                .HasColumnName("salary");
            entity.Property(e => e.VacancyStatus)
                .HasDefaultValueSql("'Open'")
                .HasColumnType("enum('Open','Closed','Pending')")
                .HasColumnName("vacancy_status");

            entity.HasOne(d => d.Recruiter).WithMany(p => p.JobVacancies)
                .HasForeignKey(d => d.RecruiterId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("job_vacancy_fk_recruiter_id");

            entity.HasMany(d => d.Skills).WithMany(p => p.JobVacancies)
                .UsingEntity<Dictionary<string, object>>(
                    "JobVacancySkill",
                    r => r.HasOne<Skill>().WithMany()
                        .HasForeignKey("SkillId")
                        .HasConstraintName("job_vacancy_skill_fk_skill_id"),
                    l => l.HasOne<JobVacancy>().WithMany()
                        .HasForeignKey("JobVacancyId")
                        .HasConstraintName("job_vacancy_skill_fk_job_vacancy_id"),
                    j =>
                    {
                        j.HasKey("JobVacancyId", "SkillId").HasName("PRIMARY");
                        j.ToTable("JobVacancySkill");
                        j.HasIndex(new[] { "JobVacancyId" }, "job_vacancy_skill_fk_job_vacancy_id_idx");
                        j.HasIndex(new[] { "SkillId" }, "job_vacancy_skill_fk_skill_id_idx");
                        j.IndexerProperty<int>("JobVacancyId").HasColumnName("job_vacancy_id");
                        j.IndexerProperty<int>("SkillId").HasColumnName("skill_id");
                    });
        });

        modelBuilder.Entity<Recruiter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Recruiter");

            entity.HasIndex(e => e.CompanyId, "recruiter_fk_company_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.ContactInfo)
                .HasMaxLength(255)
                .HasColumnName("contact_info");
            entity.Property(e => e.FirstName)
                .HasMaxLength(45)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(45)
                .HasColumnName("last_name");
            entity.Property(e => e.Specialization)
                .HasMaxLength(100)
                .HasColumnName("specialization");
            entity.Property(e => e.SuccessfulClosures).HasColumnName("successful_closures");

            entity.HasOne(d => d.Company).WithMany(p => p.Recruiters)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("recruiter_fk_company_id");
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Skill");

            entity.HasIndex(e => e.Name, "name_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
