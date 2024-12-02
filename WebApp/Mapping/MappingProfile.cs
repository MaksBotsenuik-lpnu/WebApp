using AutoMapper;
using WebApp.Models;
using WebApp.DTOs;

namespace WebApp.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Мапінг Recruiter -> RecruiterDto
            CreateMap<Recruiter, RecruiterDto>()
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => new CompanyDto
                {
                    Id = src.Company != null ? src.Company.Id : 0,
                    Name = src.Company != null ? src.Company.Name : null
                }))
                .ForMember(dest => dest.JobVacancies, opt => opt.MapFrom(src => src.JobVacancies));

            // Мапінг JobVacancy -> JobVacancyDto
            CreateMap<JobVacancy, JobVacancyDto>();

            // Мапінг CreateRecruiterDto -> Recruiter
            CreateMap<CreateRecruiterDto, Recruiter>()
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => new Company { Name = src.CompanyName }));
        }
    }
}