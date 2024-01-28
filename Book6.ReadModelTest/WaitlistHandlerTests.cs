using Book6.ReadModel;
using Book6.ReadModel.Handler;
using MM.ES;
using NUnit.Framework;
using Raven.Client.Linq.Indexing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book6.ReadModelTest
{
    [TestFixture]
    public class WaitlistHandlerTests
    {
        private IReadModelSession session;
        private WaitlistHandler handler;
        private Guid rId;
        private Waitlist reserve;

        [TearDown]
        public void Teardown()
        { }
        [SetUp]
        public void Setup()
        {
            session = Global.ReadModelSession;
            handler = new WaitlistHandler(session);
            rId = Guid.NewGuid();
            reserve = new Waitlist
            {
                Id = rId,
                BookId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
            };
            session.Store(reserve);

        }
        [Test]
        public void NewWaitlist()
        {
            //Arrange
            var e = new NewWaitlist
            {
                Id = Guid.NewGuid(),
                BookId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
            };
            //Action
            handler.Handle(e);
            var exp = session.Load<Waitlist>(e.Id);

            //Assert
            Assert.AreEqual(e.Id, exp.Id);
            Assert.AreEqual(e.BookId, exp.BookId);
            Assert.AreEqual(e.UserId, exp.UserId);

        }
        [Test]
        public void RemoveWaitlist()
        {
            //Arrange
            session.Store(reserve);
            var e = new RemoveWaitlist()
            {
                Id = rId,
            };

            //Action
            handler.Handle(e);
            var exp = session.Load<Waitlist>(e.Id);

            //Assert
            Assert.IsNull(exp);

        }
    }
}
