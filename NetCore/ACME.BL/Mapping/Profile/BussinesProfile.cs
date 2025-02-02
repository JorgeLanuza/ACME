namespace ACME.BL.Mapping.Profile
{
    using ACME.DataAccess.Entities;
    using ACME.Dtos;
    using AutoMapper;
    public class BussinesProfile : Profile
    {
        public BussinesProfile()
        {
            CreateMap<VisitDto, VisitEntity>().ReverseMap().ForAllMembers(delegate (IMemberConfigurationExpression<VisitEntity, VisitDto, object> opts)
            {
                opts.Condition((VisitEntity src, VisitDto dest, object srcMember) => srcMember != null);
            });
        }
    }
}
