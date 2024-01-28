using System;
using System.Linq;
using CRG.ES;
using Book6.ReadModel.Handler;
using Book6.ReadModel.RavenConfig;
using Book6.Common;
using NLog;
using Raven.Abstractions.Indexing;
using Raven.Client;
using Raven.Client.Indexes;

namespace Book6.ReadModel.RavenConfig
{
    public class SetUpIndex : IRequireStartup

    {
        private readonly IDocumentStore store;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public SetUpIndex(IDocumentStore store)
        {
            this.store = store;
        }
        public void Start()
        {
            logger.Info("BookEventId Startup called..");
            new BookEvent_SetupIndex().Execute(store);
            new UserEvent_SetupIndex().Execute(store);
            new ReservationEvent_SetupIndex().Execute(store);
            new WaitListEvent_SetupIndex().Execute(store);
        }
    }
}
