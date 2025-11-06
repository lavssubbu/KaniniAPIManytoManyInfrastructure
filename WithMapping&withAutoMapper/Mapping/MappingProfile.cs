using APIMMwithoutJunctionModel.DTOs;
using APIMMwithoutJunctionModel.Models;
using AutoMapper;


namespace APIMMwithoutJunctionModel.Mapping
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateDocDtoc, Doctor>();

            CreateMap<Doctor,DoctorDto>();

            CreateMap<Doctor, GetDocDto>()
           .ForMember(dest => dest.PatientName,
                      opt => opt.MapFrom(src => src.Patients != null
                          ? src.Patients.Select(p => p.PatName).ToList()
                          : new List<string>()));
        }      


    }
}
