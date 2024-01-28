using Autofac.Configuration;
using Autofac;
using CRG.ES.CommandProcessor;
using CRG.ES;
using CRG.RavenConfig;
using Microsoft.Extensions.Configuration;
using MM.ES;
using MM.FileStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Book6.ReadModel.RavenConfig;
using CRG.ES.Messages;
using System.Data.SqlClient;

namespace Book6.PullFromEmployee2
{
    public class RegisterAutofac : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(RegisterAutofac).Assembly).AsClosedTypesOf(typeof(IHandleEvent<>));
            var autofacConfig = new Microsoft.Extensions.Configuration.ConfigurationBuilder();
            autofacConfig.AddJsonFile("autofac.json");
            var module = new ConfigurationModule(autofacConfig.Build());
            builder.RegisterModule(module);
            builder.RegisterModule(new RavenDbConfig(System.Configuration.ConfigurationManager.ConnectionStrings["RavenDB"].ConnectionString));
            builder.RegisterModule(new ReadModel.Handler.RegisterAutofac());
            //builder.RegisterTypes(typeof(ReadModel.Handler.EmployeeHandler)).AsClosedTypesOf(typeof(IHandleEvent<>));
            builder.RegisterModule(new ConfigCommandProcessing<Book6.Domain.Employee>("PullFromEmployee2",
                new[] { typeof(Book6.Domain.Employee).Assembly },
                new[] { typeof(Book6.CommandHandler.EmployeeHandler).Assembly }, TypeOfEventProcessing.BulkEventHandling
                ));
            builder.Register(x => new SqlFileStore(new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["FileStore"].ConnectionString)))
                .As<IStoreFiles>().SingleInstance();
            builder.RegisterInstance(new ProcessDescriptor("PullFromEmployee2"));
            //builder.RegisterType<SetUpIndex>().As<IRequireStartup>();
        }
    }
}
