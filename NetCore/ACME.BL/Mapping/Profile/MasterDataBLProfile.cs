namespace ACME.BL.Mapping.Profile
{
    using ACME.DataAccess.Entities;
    using ACME.Dtos;
    using AutoMapper;
    public class MasterDataBLProfile : Profile
    {
        public MasterDataBLProfile()
        {
            CreateMap<VisitEntity, VisitDto>().ReverseMap();
        }

    }
}
