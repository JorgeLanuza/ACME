namespace ACME.BL.Mapping.Profile
{
    using ACME.DataAccess.Entities;
    using ACME.Dtos;
    using AutoMapper;
    public class BussinesProfile : Profile
    {
        public BussinesProfile()
        {
            CreateMap<VisitEntity, VisitDto>()
                .ForMember(dest => dest.Employee, opt => opt.MapFrom(src => src.Employee != null ? src.Employee : null))
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<VisitDto, VisitEntity>()
                .ForMember(dest => dest.Employee, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }

    }
}
