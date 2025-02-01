namespace ACME.IoC
{
    using ACME.BL;
    using ACME.BL.Services;
    using ACME.BL.Validations;
    using ACME.DataAccess.Context;
    using ACME.DataAccess.Entities;
    using ACME.DataAccess.Repositories;
    using ACME.DependencyInjection.Container;
    using ACME.Dtos;
    using Autofac;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using System.Reflection;

    public static class DependencyInjection
    {
        public static void RegisterDependencyInjector(ACMEContainerBuilder builder)
        {
            MapperConfiguration config = new(cfg => cfg.AddMaps(Assembly.GetExecutingAssembly()));
            IMapper mapper = config.CreateMapper();
            builder.RegisterInstance(mapper);
            builder.RegisterType<VisitRepository>();
            builder.RegisterType<VisitService>().As<IVisitService>().WithParameter("logger", new LoggerFactory().CreateLogger<VisitService>());
            builder.RegisterType<VisitRepository>();
        }
    }
}
