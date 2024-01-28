using Autofac;
using Book6.ReadModel.Handler;
using CRG.ES;
using CRG.RavenConfig;
using MM.ES;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book6.ReadModel.Rebuild
{
    public class RegisterAutofac : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new RavenDbConfig(ConfigurationManager.ConnectionStrings["RavenDB"].ConnectionString));

            // Register all handlers within the following assembly

            builder.RegisterTypes(typeof(BookHandler)).AsClosedTypesOf(typeof(IHandleEvent<>));
            builder.RegisterTypes(typeof(UserHandler)).AsClosedTypesOf(typeof(IHandleEvent<>));
            builder.RegisterTypes(typeof(ReservationHandler)).AsClosedTypesOf(typeof(IHandleEvent<>));
            builder.RegisterTypes(typeof(WaitlistHandler)).AsClosedTypesOf(typeof(IHandleEvent<>));
           
            //builder.RegisterTypes(typeof(QAInspectionHandler)).AsClosedTypesOf(typeof(IHandleEvent<>));

            //builder.RegisterInstance(new ChecklistFileSetting(ConfigurationManager.AppSettings["DataDirectory"] + "InspectorChecklists.json")).As<IFileSetting<Checklist, ChecklistItem>>();

            //builder.RegisterType<RavenConfig.SetupIndexes>().As<IRequireStartup>();

            //InspectionCheckList.LoadChecklist(ConfigurationManager.AppSettings["DataDirectory"] + "Inspector Checklists.xlsx", InspectorCheckListLoader.Load);
            //Holiday.Load(ConfigurationManager.AppSettings["DataDirectory"] + "Holidays.xlsx", HolidayLoader.Load);
        }
    }
}
