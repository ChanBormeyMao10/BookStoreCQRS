using CRG.ES;
using MM.ES;
using NLog;
using Autofac;



namespace Book6.ReadModel.Handler
{
    public class RegisterAutofac : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(new[] { typeof(RegisterAutofac).Assembly }).AsClosedTypesOf(typeof(IHandleEvent<>));
            builder.RegisterType<ReadModel.RavenConfig.SetUpIndex>().As<IRequireStartup>();
        }
    }
}
