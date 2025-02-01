namespace ACME.DependencyInjection.Container
{
    using Autofac;
    using Autofac.Builder;

    public class ACMEContainerBuilder(ContainerBuilder builder)
    {
        private readonly ContainerBuilder _builder = builder;
        public ContainerBuilder GetBuilder()
        {
            return _builder;
        }
        public void RegisterInstance<T>(T instance) where T : class
        {
            _builder.RegisterInstance(instance);
        }
        public IRegistrationBuilder<T, ConcreteReflectionActivatorData, SingleRegistrationStyle> RegisterType<T>() where T : class
        {
            return _builder.RegisterType<T>();
        }
        public IRegistrationBuilder<T, SimpleActivatorData, SingleRegistrationStyle> Register<T>(Func<IComponentContext, T> delegateFunc) where T : class
        {
            return _builder.Register(delegateFunc).AsSelf();
        }
    }
}
