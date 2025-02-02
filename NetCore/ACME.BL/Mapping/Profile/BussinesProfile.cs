namespace ACME.BL.Mapping.Profile
{
    using ACME.DataAccess.Entities;
    using ACME.Dtos;
    using ACME.Dtos.Base_Dto.Authentication;
    using AutoMapper;
    public class BussinesProfile : Profile
    {
        public BussinesProfile()
        {
            CreateMap<VisitEntity, VisitDto>().ReverseMap().ForAllMembers(delegate (IMemberConfigurationExpression<VisitDto, VisitEntity, object> opts)
            {
                opts.Condition((VisitDto src, VisitEntity dest, object srcMember) => srcMember != null);
            });
            CreateMap<EmployeeEntity, EmployeeDto>().ReverseMap().ForAllMembers(delegate (IMemberConfigurationExpression<EmployeeDto, EmployeeEntity, object> opts)
            {
                opts.Condition((EmployeeDto src, EmployeeEntity dest, object srcMember) => srcMember != null);
            });
            CreateMap<AuthenticationEntity, AuthenticationDto>().ReverseMap().ForAllMembers(delegate (IMemberConfigurationExpression<AuthenticationDto, AuthenticationEntity, object> opts)
            {
                opts.Condition((AuthenticationDto src, AuthenticationEntity dest, object srcMember) => srcMember != null);
            });
            CreateMap<UserProfileEntity, UserProfileDto>().ReverseMap().ForAllMembers(delegate (IMemberConfigurationExpression<UserProfileDto, UserProfileEntity, object> opts)
            {
                opts.Condition((UserProfileDto src, UserProfileEntity dest, object srcMember) => srcMember != null);
            });
        }

    }
}
