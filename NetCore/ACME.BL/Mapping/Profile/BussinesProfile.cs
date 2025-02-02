namespace ACME.BL.Mapping.Profile
{
    using ACME.DataAccess.Entities;
    using ACME.Dtos;
    using AutoMapper;
    public class BussinesProfile : Profile
    {
        public BussinesProfile()
        {
            CreateMap<VisitEntity, VisitDto>().ReverseMap();
        }
    }
}
