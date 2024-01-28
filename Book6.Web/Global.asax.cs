using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Reflection;
using CRG.ES;
using CRG.ES.CommandProcessor;
using CRG.RavenConfig;
using CRG.ES.ClientAPI;
using NLog;
using MM.ES;
using MM.FileStore;
using System.Configuration;
using System.Data.SqlClient;
using Raven.Client;
using Autofac;
using Autofac.Configuration;
using Autofac.Integration.Mvc;
using ConfigurationBuilder = Microsoft.Extensions.Configuration.ConfigurationBuilder;
using Book = Book6.Domain.Book;
using User = Book6.Domain.User;
using Reservation = Book6.Domain.Reservation;
using Waitlist = Book6.Domain.Waitlist;
using Microsoft.Extensions.Configuration;
using ConfigurationManager = System.Configuration.ConfigurationManager;


namespace Book6.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        //public static IContainer container;
        //public static SqlFileStore FileStore;
        public new IDocumentSession Session { get; set; }

        protected void Application_Start()
        {
            logger.Info("Application Started....");
            //Bootstrap.RegisterSerialNumber();
            var builder = new ContainerBuilder();

            var autofacConfig = new ConfigurationBuilder();
            autofacConfig.AddJsonFile("autofac.json");
            var module = new ConfigurationModule(autofacConfig.Build());
            builder.RegisterModule(module);

            builder.RegisterControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();
            builder.RegisterModule(new AutofacWebTypesModule());

            builder.RegisterModule(new ConfigCommandProcessing<Book>("Book6.Web",
                new[] { typeof(Book).Assembly },
                new[] { typeof(CommandHandler.BookHandler).Assembly }));
            builder.RegisterModule(new ConfigCommandProcessing<User>("Book6.Web",
               new[] { typeof(User).Assembly },
               new[] { typeof(CommandHandler.UserHandler).Assembly }));
            builder.RegisterModule(new ConfigCommandProcessing<Reservation>("Book6.Web",
               new[] { typeof(Reservation).Assembly },
               new[] { typeof(CommandHandler.ReservationHandler).Assembly }));
            builder.RegisterModule(new ConfigCommandProcessing<Waitlist>("Book6.Web",
               new[] { typeof(Waitlist).Assembly },
               new[] { typeof(CommandHandler.WaitlistHandler).Assembly }));



            var ravenConnectionString = ConfigurationManager.ConnectionStrings["RavenDB"].ConnectionString;
            builder.RegisterModule(new RavenDbConfig(ravenConnectionString));

            builder.RegisterTypes(typeof(Book6.ReadModel.Handler.BookHandler)).AsClosedTypesOf(typeof(IHandleEvent<>));
            builder.RegisterTypes(typeof(Book6.ReadModel.Handler.UserHandler)).AsClosedTypesOf(typeof(IHandleEvent<>));
            builder.RegisterTypes(typeof(Book6.ReadModel.Handler.ReservationHandler)).AsClosedTypesOf(typeof(IHandleEvent<>));
            builder.RegisterTypes(typeof(Book6.ReadModel.Handler.WaitlistHandler)).AsClosedTypesOf(typeof(IHandleEvent<>));

            builder.RegisterType<Book6.ReadModel.RavenConfig.SetUpIndex>().As<IRequireStartup>();


            var container = builder.Build();
            var store = container.Resolve<IDocumentStore>();
            new Book6.ReadModel.RavenConfig.BookEvent_SetupIndex().Execute(store);
            new Book6.ReadModel.RavenConfig.UserEvent_SetupIndex().Execute(store);
            new Book6.ReadModel.RavenConfig.ReservationEvent_SetupIndex().Execute(store);
            new Book6.ReadModel.RavenConfig.WaitListEvent_SetupIndex().Execute(store);

            container.Resolve<IEnumerable<IRequireStartup>>().ToList().ForEach(x => x.Start());

            var httpRequestResolver = new AutofacDependencyResolver(container);

            DependencyResolver.SetResolver(httpRequestResolver);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }


    }
}
