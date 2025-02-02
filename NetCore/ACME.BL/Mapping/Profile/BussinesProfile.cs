namespace ACME.BL.Mapping.Profile
{
    using ACME.DataAccess.Entities;
    using ACME.Dtos;
    using AutoMapper;
    public class BussinesProfile : Profile
    {
        public BussinesProfile()
        {
            CreateMap<VisitEntity, VisitDto>().ReverseMap().ForAllMembers(delegate (IMemberConfigurationExpression<VisitDto, VisitEntity, object> opts)
            {
                opts.Condition((VisitDto src, VisitEntity dest, object srcMember) => srcMember != null);
            });
        }

    }
}
