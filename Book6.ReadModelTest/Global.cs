using MM.ES;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client;
using Raven.Client.Embedded;
using Book6.ReadModel.RavenConfig;
using System.IO;

namespace Book6.ReadModelTest
{
    [SetUpFixture]
    public class Global
    {
        public static IReadModelSession ReadModelSession;
        private static EmbeddableDocumentStore raven;
        [OneTimeSetUp]
        public void SetUp()
        {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
            Directory.SetCurrentDirectory(TestContext.CurrentContext.TestDirectory);
            raven = new EmbeddableDocumentStore { RunInMemory = true };

            raven.Configuration.Storage.Voron.AllowOn32Bits = true;
            raven.Initialize();
            new SetUpIndex(raven).Start();
            var session = raven.OpenSession();
            ReadModelSession = new RavenDocumentStore(raven);
        }
        internal class RavenDocumentStore : IReadModelSession
        {
            private readonly IDocumentSession session;

            public RavenDocumentStore(IDocumentStore raven)
            {
                session = raven.OpenSession();
            }

            public T Load<T>(Guid id) where T : class
            {
                return session.Load<T>(id);
            }

            public T Load<T>(string id) where T : class
            {
                return session.Load<T>(id);
            }

            public void Store(object obj)
            {
                session.Store(obj);
            }

            public void Store(object obj, string id)
            {
                session.Store(obj, id);
            }

            public void Delete(object obj)
            {
                session.Delete(obj);
            }

            public void Delete(string id)
            {
                session.Delete(id);
            }
        }
    }
}
